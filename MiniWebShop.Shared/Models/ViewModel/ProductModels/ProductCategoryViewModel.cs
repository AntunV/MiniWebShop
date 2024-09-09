using MiniWebShop.Shared.Models.Base.ProductModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniWebShop.Shared.Models.ViewModel.ProductModels
{
    public class ProductCategoryViewModel:ProductCategoryBase
    {
        public int Id { get; set; } 
        public List<ProductItemViewModel> ProductItems { get; set; }
    }
}
