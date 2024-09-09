using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniWebShop.Shared.Models.Binding.AccountModels
{
    public class ApplicationUserUpdateBinding
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public AddressUpdateBinding? Address { get; set; }


    }
}
