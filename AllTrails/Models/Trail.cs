using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AllTrails.Models
{
    public class Trail
    {
        public int Id { get; set; } // PK

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        public string? ImageUrl { get; set; } // Image not required (nullable)

        [Required]
        [Range(0, 24000)] // 0 allows sub-1km trails; max = Trans Canada Trail
        [Display(Name = "Length (km)")]        
        public int LengthKm { get; set; } // 7km

        [Required]
        [Range(0, 999)] // max < 1km
        [Display(Name = "Length (m)")]
        public int LengthMetres { get; set; } // 350m

        [Required]
        [Range(0, 8849)] // max = Mt. Everest
        [Display(Name = "Elevation Gain (m)")]        
        public int ElevationGainMetres { get; set; } // 450m

        [Required]
        [Display(Name = "Estimated Duration")]
        public TimeSpan EstimatedDuration { get; set; } // 3hrs 30min

        [Display(Name = "Date Created")]
        [ScaffoldColumn(false)] // not editable in Create/Edit
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow; // initialize UTC time

        [ValidateNever]
        public ICollection<TripReport> TripReports { get; set; } = new List<TripReport>();
    }
}
