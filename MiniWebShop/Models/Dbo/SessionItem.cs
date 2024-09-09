using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using MiniWebShop.Models.Dbo.UserModel;
using MiniWebShop.Shared.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace MiniWebShop.Models.Dbo
{
    public class SessionItem : SessionItemBase 
    {
        [Key]
        public int Id { get; set; }
        public ApplicationUser? User { get; set; }
        public string? UserId { get; set; }

    }
}
