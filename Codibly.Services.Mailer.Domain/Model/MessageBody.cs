namespace Codibly.Services.Mailer.Domain.Model
{
    public class MessageBody
    {
        public string Body { get; }
        public bool IsHtml { get; }

        public MessageBody(string body, bool isHtml = false)
        {
            this.Body = body;
            this.IsHtml = isHtml;
        }

        public static MessageBody CreateHtmlBody(string htmlBody) => new MessageBody(htmlBody, true);
        public static MessageBody CreateTextBody(string body) => new MessageBody(body);
    }
}