using MiniWebShop.Shared.Models.Base.ProductModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniWebShop.Shared.Models.Binding.ProductModels
{
    public  class ProductCategoryUpdateBinding:ProductCategoryBase
    {
        public int Id { get; set; }
    }
}
