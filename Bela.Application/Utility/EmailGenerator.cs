using Bela.Application.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bela.Application.Utility
{
    public class EmailGenerator
    {
        public static EmailMessage GenerateConfirmEmailMessage(string link, string userEmail)
        {
            EmailMessage message = new EmailMessage()
            {
                From = "noreply@belot.hr",
                To = userEmail,
                Subject = "Belot.hr Aktivacijski link",
                Body = $"<h2>Dobrodošli na Belot.hr</h2> <br \\> <h4>Dovršite registraciju klikom na <a href=\"{link}\">link</a></h4>"
            };

            return message;
        }

        public static EmailMessage GeneratePasswordResetMessage(string link, string userEmail)
        {
            EmailMessage message = new EmailMessage()
            {
                From = "noreply@belot.hr",
                To = userEmail,
                Subject = "Belot.hr Promjena lozinke",
                Body = $"<h4>Kliknite na <a href=\"{link}\">link</a> za promjenu lozinke</h4>"
            };

            return message;
        }

    }
}
