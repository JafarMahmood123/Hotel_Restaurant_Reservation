using Hotel_Restaurant_Reservation.Application.Abstractions.Payment;
using Hotel_Restaurant_Reservation.Infrastructure.Algorithms;
using Microsoft.Extensions.Configuration;
using PayPalCheckoutSdk.Core;
using PayPalCheckoutSdk.Orders;
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
        _httpClientFactory = httpClientFactory;
        var clientId = configuration["PayPal:ClientId"];
        var clientSecret = configuration["PayPal:ClientSecret"];
        var mode = configuration["PayPal:Mode"];
        _webhookId = configuration["PayPal:WebhookId"];

        if (string.IsNullOrEmpty(clientId) || string.IsNullOrEmpty(clientSecret) || string.IsNullOrEmpty(mode))
        {
            throw new ArgumentNullException("PayPal configuration is missing from appsettings.json.");
        }

        PayPalEnvironment environment = mode.ToLower() == "sandbox"
            ? new SandboxEnvironment(clientId, clientSecret)
            : new LiveEnvironment(clientId, clientSecret);

        _client = new PayPalHttpClient(environment);
    }

    public async Task<Order> CreateOrder(string totalAmount, string currencyCode)
    {
        var orderRequest = new OrderRequest()
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
        };

        var request = new OrdersCreateRequest();
        request.Prefer("return=representation");
        request.RequestBody(orderRequest);

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
        // Use the custom Crc32 class to compute the hash.
        byte[] bodyBytes = Encoding.UTF8.GetBytes(requestBody);
        uint crc32 = Crc32.Compute(bodyBytes);

        var message = $"{headers.TransmissionId}|{headers.Timestamp}|{_webhookId}|{crc32}";

        var httpClient = _httpClientFactory.CreateClient();
        var certBytes = await httpClient.GetByteArrayAsync(headers.CertUrl);
        var certificate = new X509Certificate2(certBytes);

        var rsa = certificate.GetRSAPublicKey();
        if (rsa == null)
        {
            return false;
        }

        var data = Encoding.UTF8.GetBytes(message);
        var signatureBytes = Convert.FromBase64String(headers.Signature);

        return rsa.VerifyData(data, signatureBytes, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
    }
}