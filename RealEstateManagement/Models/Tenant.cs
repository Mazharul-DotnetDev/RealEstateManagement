using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RealEstateManagement.Models
{
    public class Tenant
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TenantId { get; set; }

        [Required(ErrorMessage = "Tenant Name is required.")]
        [Display(Name = "Tenant Name")]
        public string TenantName { get; set; }

        [Display(Name = "Contact Information")]
        public string T_ContactInformation { get; set; }

        [Required(ErrorMessage = "Lease Start Date is required.")]
        [DataType(DataType.Date)]
        [Display(Name = "Lease Starting Date")]
        public DateTime LeaseStartDate { get; set; } = DateTime.Now;


        [NotMapped]
        public IFormFile? ImageUpload { get; set; }

        public string? TenantImage { get; set; }


        public IList<Asset> AssetList { get; set; } = new List<Asset>();

    }
}
