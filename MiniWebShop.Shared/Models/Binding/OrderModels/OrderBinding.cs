using MiniWebShop.Shared.Models.Base.OrderModels;
using MiniWebShop.Shared.Models.Binding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniWebShop.Shared.Models.Binding.OrderModels
{
    public class OrderBinding : OrderBase
    {
        public AddressBinding? OrderAddress { get; set; }
        public List<OrderItemBinding>? OrderItems { get; set; }
    }
}
