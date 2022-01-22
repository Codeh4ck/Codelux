using System.Collections.Generic;

namespace Codelux.Utilities.Mailing
{
    public class Email
    {
        public Email()
        {
            To = new();
            ReplyTo = new();
        }

        public string From { get; set; }
        public string FromName { get; set; }
        public List<string> To { get; set; }
        public List<string> ReplyTo { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool IsHtml { get; set; }
    }
}
