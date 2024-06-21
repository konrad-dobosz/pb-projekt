using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace pb_projekt.Models
{
    public class Hangar
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int AvailableSpace { get; set; }

        [Required]
        public int LoadedCrates { get; set; }

        public ICollection<Cargo> Cargoes { get; set; } = new List<Cargo>();

        public ICollection<LandShipment> LandShipments { get; set; } = new List<LandShipment>();
    }
}