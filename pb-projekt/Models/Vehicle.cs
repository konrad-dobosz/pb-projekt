using System;
using System.ComponentModel.DataAnnotations;

namespace pb_projekt.Models
{
    public class Vehicle
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string DestinationPort { get; set; }

        [Required]
        public DateTime ArrivalDate { get; set; }

        public int LandShipmentId { get; set; }
        public LandShipment LandShipment { get; set; }
    }
}
