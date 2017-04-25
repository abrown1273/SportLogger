using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportLogger.Services
{
    public class AuthMessageSenderOptions
    {
        // https://docs.microsoft.com/en-us/aspnet/core/security/authentication/accconfirm
        //https://sendgrid.com/free/

        public string SendGridUser { get; set; }
        public string SendGridKey { get; set; }
    }
}
