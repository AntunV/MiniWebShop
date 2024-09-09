using MiniWebShop.Shared.Models.Base.ProductModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniWebShop.Shared.Models.ViewModel.ProductModels
{
    public class ProductItemViewModel:ProductItemBase
    {
        public int Id { get; set; }
        public int? ProductCategoryId { get; set; }
    }
}
