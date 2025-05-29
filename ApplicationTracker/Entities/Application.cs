using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationTracker.Models
{
    public class Application
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [Display(Name="Company name"), Column(TypeName = "varchar"), MaxLength(50)]
        public string CompanyName { get; set; } = String.Empty;

        [Display(Name="Position"), Column(TypeName = "varchar"), MaxLength(50)]
        public string Position { get; set; } = String.Empty;

        [Display(Name="Date applied")]
        public DateOnly DateApplied { get; set; }

        public ApplicationStatus Status { get; set; } = 0;
    }

    public enum ApplicationStatus
    {
        Interview = 1,
        Offer = 2,
        Rejected = 3
    }

    public class Status
    {
        [Key]
        public int Id { get; set; }
        
        [Column(TypeName = "varchar"), MaxLength(50)]
        public string Name { get; set; } = String.Empty;
    }
}