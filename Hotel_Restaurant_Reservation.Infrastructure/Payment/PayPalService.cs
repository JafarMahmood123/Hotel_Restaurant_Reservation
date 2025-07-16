using Hotel_Restaurant_Reservation.Application.Abstractions.Payment;
using Microsoft.Extensions.Configuration;
using PayPalCheckoutSdk.Core;
using PayPalCheckoutSdk.Orders;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Hotel_Restaurant_Reservation.Infrastructure.Payment;

public class PayPalService : IPayPalService
{
    private readonly PayPalHttpClient _client;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly string _webhookId;

    public PayPalService(IConfiguration configuration, IHttpClientFactory httpClientFactory)
    {
        var clientId = configuration["PayPal:ClientId"];
        var clientSecret = configuration["PayPal:ClientSecret"];
        var mode = configuration["PayPal:Mode"];
        _webhookId = configuration["PayPal:WebhookId"];
        _httpClientFactory = httpClientFactory;


        if (mode == "sandbox")
        {
            var environment = new SandboxEnvironment(clientId, clientSecret);
            _client = new PayPalHttpClient(environment);
        }
        else
        {
            var environment = new LiveEnvironment(clientId, clientSecret);
            _client = new PayPalHttpClient(environment);
        }
    }

    public async Task<Order> CreateOrder(string totalAmount, string currencyCode)
    {
        var request = new OrdersCreateRequest();
        request.Prefer("return=representation");
        request.RequestBody(new OrderRequest()
        {
            CheckoutPaymentIntent = "CAPTURE",
            PurchaseUnits = new List<PurchaseUnitRequest>()
            {
                new PurchaseUnitRequest()
                {
                    AmountWithBreakdown = new AmountWithBreakdown()
                    {
                        CurrencyCode = currencyCode,
                        Value = totalAmount
                    }
                }
            }
        });

        var response = await _client.Execute(request);
        return response.Result<Order>();
    }

    public async Task<Order> CaptureOrder(string orderId)
    {
        var request = new OrdersCaptureRequest(orderId);
        request.RequestBody(new OrderActionRequest());

        var response = await _client.Execute(request);
        return response.Result<Order>();
    }

    public async Task<bool> VerifyWebhookSignature(string requestBody, WebhookHeaders headers)
    {
        var crc32 = Crc32.Compute(Encoding.UTF8.GetBytes(requestBody));

        var message = $"{headers.TransmissionId}|{headers.Timestamp}|{_webhookId}|{crc32}";

        var httpClient = _httpClientFactory.CreateClient();
        var certBytes = await httpClient.GetByteArrayAsync(headers.CertUrl);
        var certificate = new X509Certificate2(certBytes);

        var rsa = certificate.GetRSAPublicKey();
        var data = Encoding.UTF8.GetBytes(message);
        var signatureBytes = Convert.FromBase64String(headers.Signature);

        return rsa.VerifyData(data, signatureBytes, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
    }
}