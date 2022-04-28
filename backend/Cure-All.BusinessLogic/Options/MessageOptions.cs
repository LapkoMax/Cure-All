using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cure_All.BusinessLogic.Options
{
    public class MessageOptions
    {
        public List<MailboxAddress> To { get; set; }
        
        public string Subject { get; set; }

        public string Content { get; set; }

        public MessageOptions(IEnumerable<string> to, string subject, string content)
        {
            To = new List<MailboxAddress>();

            To.AddRange(to.Select(x => new MailboxAddress(x)));
            Subject = subject;
            Content = content;
        }
    }
}
