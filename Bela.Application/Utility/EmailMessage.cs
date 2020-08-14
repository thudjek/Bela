using System;
using System.Collections.Generic;
using System.Text;

namespace Bela.Application.Utility
{
    public class EmailMessage
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
