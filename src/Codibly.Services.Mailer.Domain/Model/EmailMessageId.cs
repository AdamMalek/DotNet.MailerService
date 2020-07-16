namespace Codibly.Services.Mailer.Domain.Model
{
    public class EmailMessageId
    {
        public string Value { get; }

        public EmailMessageId(string value)
        {
            Value = value;
        }

        public override string ToString() => this.Value.ToString();
    }
}