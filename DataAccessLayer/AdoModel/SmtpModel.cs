using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.AdoModel
{
    public class SmtpModel
    {
        public string smtp_sender { get; set; }
        public string smtp_host { get; set; }
        public string smtp_password { get; set; }
        public int smtp_port { get; set; }
        public int version { get; set; }
    }
}
