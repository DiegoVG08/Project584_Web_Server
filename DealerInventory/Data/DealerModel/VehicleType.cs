using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DealerInventory.Data.DealerModel;

[Table("VehicleType")]
[Index(nameof(Make))]
[Index(nameof(Model))]
[Index(nameof(Year))]
[Index(nameof(DealershipID))]


public class VehicleType
{

    [Key]
    [Required]

    public int VehicleTypeID { get; set; }

    [StringLength(50)]
    [Column(TypeName = "VARCHAR(50)")]
    public string Make { get; set; } = null!;

    [StringLength(50)]
    [Column(TypeName = "VARCHAR(50)")]
    public string Model { get; set; } = null!;


    public int Year { get; set; }

    public int DealershipID { get; set; }

    public CarDealership? carDealership { get; set; }
}

