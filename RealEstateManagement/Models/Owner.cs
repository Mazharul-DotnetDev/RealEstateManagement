using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RealEstateManagement.Models
{
    public class Owner
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OwnerId { get; set; }

        [Display(Name = "Owner Name")]
        public string OwnerName { get; set; }

        [Display(Name = "Owner Contact Information")]
        public string Own_ContactInformation { get; set; }


        [Display(Name = "Owner Salary")]
        public decimal Salary { get; set; }

        public decimal TotalYearlyIncome
        {
            get => Salary * 12;
        }


        public int? TenantId { get; set; }
        public int? AssetId { get; set; }


        public virtual Asset? GetAsset { get; set; }

        public virtual Tenant? GetTenant { get; set; }



    }
}
