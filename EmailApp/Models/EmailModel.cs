using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmailApp.Models
{
    public class EmailModel
    {
        public string Sender { get; set; }
        public string Recipient { get; set; }
        public string subject { get; set; }
        public string body { get; set; }
        public string cc { get; set; }
        public string bcc { get; set; }
    }
}