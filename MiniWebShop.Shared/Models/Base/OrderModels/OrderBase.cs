using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniWebShop.Shared.Models.Base.OrderModels
{
    public abstract class OrderBase
    {
        public string? Message { get; set; }

    }
}
