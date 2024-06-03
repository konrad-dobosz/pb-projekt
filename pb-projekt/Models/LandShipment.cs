using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace pb_projekt.Models
{
    public class LandShipment
    {
        [Key]
        public int Id { get; set; }

        public ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();

        public int HangarId { get; set; }
        public Hangar Hangar { get; set; }

        public ICollection<UnloadingEquipment> UnloadingEquipments { get; set; } = new List<UnloadingEquipment>();
    }
}
