using System;
using System.Collections.Generic;
using System.Text;

namespace Email
{
    public class EmailConfiguration
    {

        public string From { get; set; }

        public string SmtpServer { get; set; }

        public int port { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

    }

}
