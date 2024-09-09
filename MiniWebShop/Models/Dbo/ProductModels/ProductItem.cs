using MiniWebShop.Shared.Models.Base.ProductModels;
using System.ComponentModel.DataAnnotations;

namespace MiniWebShop.Models.Dbo.ProductModels
{
    public class ProductItem:ProductItemBase
    {
        [Key]
        public int Id { get; set; }

        public ProductCategory? ProductCategory { get; set; }

        public int? ProductCategoryId { get; set; }



    }
}
