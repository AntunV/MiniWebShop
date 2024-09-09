using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniWebShop.Shared.Models.Dto
{
    public class AppSettings
    {
        public int PaginationOffset { get; set; }
        public int ExpireSessionInHours { get; set; }
    }
}
