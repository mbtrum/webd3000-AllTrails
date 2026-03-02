using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace AllTrails.Models
{
    public class TripReport
    {
        public int Id { get; set; } // PK

        [Required]
        [Range(1, 5)]
        
        public int Rating { get; set; } // out of 5 stars

        [Required]
        [MaxLength(1000)]
        [Display(Name = "Report")]
        public string Description { get; set; } = string.Empty;

        [Display(Name = "Date Created")]
        [ScaffoldColumn(false)]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow; // initialize UTC time

        [Display(Name ="Trail Name")]
        public int TrailId { get; set; } //FK

        [ValidateNever]
        public Trail Trail { get; set; } = null!; // nav property
    }
}
