using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.AdoModel
{
    public class DelayModel
    {
        /// <summary>
        /// Dakika Cinsinden
        /// </summary>
        public int smtp_delay { get; set; }
        /// <summary>
        /// Gün Cinsinden
        /// </summary>
        public int workdef_delay { get; set; }
    }
}
