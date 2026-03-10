using System.ComponentModel.DataAnnotations;

namespace AllTrails.Models
{
    public class UserProfile
    {
        [Key]
        public string IdentityId { get; set; } = string.Empty; // PK - the Identity Id

        [Display(Name="Display Name")]
        public string Name { get; set; } = string.Empty;
    }
}
