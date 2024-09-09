using MiniWebShop.Shared.Models.Base.ProductModels;
using System.ComponentModel.DataAnnotations;

namespace MiniWebShop.Models.Dbo.ProductModels
{
    public class ProductCategory:ProductCategoryBase
    {
        [Key]
        public int Id { get; set; }
        
        public ICollection<ProductItem>?  ProductItems { get; set; }

    }
}
