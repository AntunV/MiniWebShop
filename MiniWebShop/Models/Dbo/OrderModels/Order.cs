using MiniWebShop.Models.Dbo.UserModel;
using MiniWebShop.Shared.Models.Base.OrderModels;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MiniWebShop.Models.Dbo.OrderModels
{
    public class Order : OrderBase
    {
        [Key]
        public int Id { get; set; }
     
        [Required(ErrorMessage = "Total price is required.")]
        [Column(TypeName = "decimal(7, 2)")]
        public decimal Total { get; set; }
        public ApplicationUser? Buyer { get; set; }
        public string? BuyerId { get; set; }
        public Address? OrderAddress { get; set; }
        public int? OrderAddressId { get; set; }

        public ICollection<OrderItem>? OrderItems { get; set; }

        public void CalcualteTotal()
        {
            if (OrderItems == null)
            {
                return;
            }

            Total = OrderItems.Select(y => y.CalculateTotal()).Sum();
        }

    }
}
