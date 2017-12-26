using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vano_report
{
    public class EmailSettings
    {
        public string AdminEmail { get; set; }
        public string AdminPassword { get; set; }
        public string SmtpPort { get; set; }
        public string SmtpServer { get; set; }
    }
}
