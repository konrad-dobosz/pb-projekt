using System;
using System.ComponentModel.DataAnnotations;

namespace pb_projekt.Models
{
    public class UnloadingEquipment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public bool IsAssignedToShip { get; set; }

        [Required]
        [MaxLength(100)]
        public string SerialNumber { get; set; }

        [Required]
        [MaxLength(100)]
        public string EquipmentType { get; set; }

        [Required]
        public DateTime LastInspectionDate { get; set; }

        public int? ShipId { get; set; }
        public Ship Ship { get; set; }

        public int? LandShipmentId { get; set; }
        public LandShipment LandShipment { get; set; }
    }
}