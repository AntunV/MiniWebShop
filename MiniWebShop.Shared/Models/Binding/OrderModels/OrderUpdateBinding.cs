using MiniWebShop.Shared.Models.Base.OrderModels;
using MiniWebShop.Shared.Models.Binding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniWebShop.Shared.Models.Binding.OrderModels
{
    public class OrderUpdateBinding : OrderBase
    {
        public int Id { get; set; }
        public AddressUpdateBinding? OrderAddress { get; set; }
        public List<OrderItemUpdateBiding>? OrderItemIds { get; set; }
    }
}
