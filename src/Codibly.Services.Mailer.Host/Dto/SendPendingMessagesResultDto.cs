using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Codibly.Services.Mailer.Host.Dto
{
    public class SendPendingMessagesResultDto
    {
        public int SuccessCount { get; }
        public string[] Errors { get; }

        public SendPendingMessagesResultDto(int successCount, ICollection<string> errors)
        {
            SuccessCount = successCount;
            Errors = errors.ToArray();
        }
    }
}