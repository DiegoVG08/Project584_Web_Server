using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

using Microsoft.EntityFrameworkCore;

namespace DealerInventory.Data.DealerModel;

[Table("CarDealership")]
[Index(nameof(Name))]
[Index(nameof(Location))]
[Index(nameof(ContactInfo))]
public class CarDealership
{

    [Key]
    [Required]
    //change to format in sql
    public int DealershipID { get; set; }

    [StringLength(50)]
    [Column(TypeName = "VARCHAR(50)")]
    public string Name { get; set; } = null!;

    [StringLength(100)]
    [Column(TypeName = "VARCHAR(100)")]
    public string Location { get; set; } = null!;

    [StringLength(100)]
    [Column(TypeName = "VARCHAR(100)")]
    public string ContactInfo { get; set; } = null!;



    public ICollection<VehicleType>? VehicleType { get; set; } = null!;

    /*public partial class CarDealership
    { }*/
}
