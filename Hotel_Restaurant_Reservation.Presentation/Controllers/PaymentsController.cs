using Hotel_Restaurant_Reservation.Application.Abstractions.Payment;
using Hotel_Restaurant_Reservation.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace Hotel_Restaurant_Reservation.Presentation.Controllers
{
    public class PaymentsController : ApiController
    {
        private readonly IPayPalService _payPalService;

        public PaymentsController(ISender sender, IPayPalService payPalService) : base(sender)
        {
            _payPalService = payPalService;
        }

        [HttpPost("create-order")]
        public async Task<IActionResult> CreateOrder(string totalAmount, string currencyCode)
        {
            var order = await _payPalService.CreateOrder(totalAmount, currencyCode);
            return Ok(order);
        }

        [HttpPost("capture-order")]
        public async Task<IActionResult> CaptureOrder(string orderId)
        {
            var order = await _payPalService.CaptureOrder(orderId);
            return Ok(order);
        }

        [HttpPost("webhook")]
        public async Task<IActionResult> PayPalWebhook()
        {
            var requestBody = await new StreamReader(Request.Body).ReadToEndAsync();

            var headers = new WebhookHeaders
            {
                TransmissionId = Request.Headers["paypal-transmission-id"],
                Timestamp = Request.Headers["paypal-transmission-time"],
                Signature = Request.Headers["paypal-transmission-sig"],
                CertUrl = Request.Headers["paypal-cert-url"]
            };

            var isSignatureValid = await _payPalService.VerifyWebhookSignature(requestBody, headers);
            if (!isSignatureValid)
            {
                return BadRequest();
            }

            var webhookEvent = JObject.Parse(requestBody);
            var eventType = webhookEvent["event_type"]?.ToString();

            switch (eventType)
            {
                case "PAYMENT.CAPTURE.COMPLETED":
                    var resource = webhookEvent["resource"];
                    var customId = resource["custom_id"]?.ToString();
                    // Update the booking/reservation status in your database using the customId
                    break;
                case "PAYMENT.CAPTURE.DENIED":
                    // Handle the denied payment
                    break;
                default:
                    // Handle other event types
                    break;
            }

            return Ok();
        }
    }
}