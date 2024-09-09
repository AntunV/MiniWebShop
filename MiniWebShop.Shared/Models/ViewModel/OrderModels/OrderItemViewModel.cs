using MiniWebShop.Shared.Models.Base.OrderModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniWebShop.Shared.Models.ViewModel.OrderModels
{
    public class OrderItemViewModel : OrderItemBase
    {
        public int Id { get; set; }
        public int? ProductItemId { get; set; }
    }
}
