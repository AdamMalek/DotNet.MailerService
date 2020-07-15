using System;
using Codibly.Services.Mailer.Domain.Exceptions;

namespace Codibly.Services.Mailer.Domain.Model
{
    public class MessageBody
    {
        public string Body { get; }
        public bool IsHtml { get; }

        internal MessageBody(string body, bool isHtml = false)
        {
            this.Body = body;
            this.IsHtml = isHtml;
        }

        public static MessageBody CreateHtmlBody(string body)
        {
            ValidateBody(body);
            return new MessageBody(body, true);
        }

        public static MessageBody CreateTextBody(string body)
        {
            ValidateBody(body);
            return new MessageBody(body);
        }

        private static void ValidateBody(string body)
        {
            if (string.IsNullOrWhiteSpace(body))
            {
                throw new EmptyMessageBodyException();
            }
        }
    }
}