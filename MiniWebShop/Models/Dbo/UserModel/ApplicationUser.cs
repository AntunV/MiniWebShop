using Microsoft.AspNetCore.Identity;

namespace MiniWebShop.Models.Dbo.UserModel
{
    public class ApplicationUser:IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Address? Address { get; set; }
        public DateTime? RegistrationDate { get; set; }


    }
}
