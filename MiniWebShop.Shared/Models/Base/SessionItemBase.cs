using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniWebShop.Shared.Models.Base
{
    public abstract class SessionItemBase
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public DateTime Expires { get; set; }
    }
}
