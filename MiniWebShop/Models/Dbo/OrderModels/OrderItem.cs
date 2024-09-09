using MiniWebShop.Models.Dbo.ProductModels;
using MiniWebShop.Shared.Models.Base.OrderModels;
using System.ComponentModel.DataAnnotations;

namespace MiniWebShop.Models.Dbo.OrderModels
{
    public class OrderItem:OrderItemBase
    {
        [Key]
        public int Id { get; set; }
        public ProductItem? ProductItem { get; set; }
        public int? ProductItemId { get; set; }

        public decimal CalculateTotal()
        {
            return Price * Quantity;
        }
    }
}
