namespace Hotel_Restaurant_Reservation.Application.Abstractions.Payment;

public class WebhookHeaders
{
    public string TransmissionId { get; set; }
    public string Timestamp { get; set; }
    public string Signature { get; set; }
    public string CertUrl { get; set; }
}