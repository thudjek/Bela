using System;
using System.Collections.Generic;
using System.Text;

namespace Bela.Application.Utility
{
    public class EmailConfigModel
    {
        public string SmtpServer { get; set; }
        public int SmtpPort { get; set; }
        public string SmtpUsername { get; set; }
        public string SmtpPassword { get; set; }
    }
}
