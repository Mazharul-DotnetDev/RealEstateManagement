using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RealEstateManagement.Models
{
    public class Asset
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AssetId { get; set; }

        [Required(ErrorMessage = "Property Name is required.")]
        public string PropertyName { get; set; }

        [Display(Name = "Property Address")]
        public string P_Address { get; set; }


        [Required(ErrorMessage = "Number of Units is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Number of Units must be greater than 0.")]
        public int NumberOfUnits { get; set; }

        [Required(ErrorMessage = "Rent Amount is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Rent Amount must be greater than or equal to 0.")]
        public decimal RentAmount { get; set; }


        public List<Owner> OwnerList { get; set; } = new List<Owner>();
    }
}
