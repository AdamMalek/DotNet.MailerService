namespace Codibly.Services.Mailer.Host.Dto
{
    public class CreateMessageDto
    {
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool? IsHtml { get; set; }
        public string Sender { get; set; }
        public string[] Recipients { get; set; }
    }
}