using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pb_projekt.Models
{
    public class Cargo
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string SerialNumber { get; set; }

        [Required]
        [MaxLength(50)]
        public string SecurityLevel { get; set; }

        [Required]
        public double Weight { get; set; }

        [Required]
        [MaxLength(100)]
        public string CargoType { get; set; }

        [Required]
        [MaxLength(100)]
        public string DestinationPort { get; set; }

        public int? ShipId { get; set; }
        public Ship Ship { get; set; }

        public int? HangarId { get; set; }
        public Hangar Hangar { get; set; }

        public int? LandShipmentId { get; set; }
        public LandShipment LandShipment { get; set; }


    }
}
