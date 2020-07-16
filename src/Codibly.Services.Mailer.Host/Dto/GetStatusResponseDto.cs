using Codibly.Services.Mailer.Domain.Model;

namespace Codibly.Services.Mailer.Host.Dto
{
    public class GetStatusResponseDto
    {
        public string MessageId { get; }
        public string Status { get; }

        public GetStatusResponseDto(string messageId, MessageStatus status)
        {
            MessageId = messageId;
            Status = status.ToString();
        }
    }
}