using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniWebShop.Shared.Models.ViewModel.AccountModels
{
    public class ApplicationUserViewModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public AddressViewModel Address { get; set; }
    }
}
