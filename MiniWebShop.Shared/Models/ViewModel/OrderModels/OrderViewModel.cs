using MiniWebShop.Shared.Models.Base.OrderModels;
using MiniWebShop.Shared.Models.ViewModel.AccountModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniWebShop.Shared.Models.ViewModel.OrderModels
{
    public class OrderViewModel : OrderBase
    {
       
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public ApplicationUserViewModel? Buyer { get; set; }
        public AddressViewModel? OrderAddress { get; set; }
        public List<OrderItemViewModel> OrderItems { get; set; }
        [Required(ErrorMessage = "Total price is required.")]
        [Column(TypeName = "decimal(7, 2)")]
        public decimal Total { get; set; }
       

    }
}
