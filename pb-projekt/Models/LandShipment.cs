using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace pb_projekt.Models
{
    public class LandShipment
    {
        [Key]
        public int Id { get; set; }
        
        public ICollection<Cargo> Cargoes { get; set; } = new List<Cargo>(); 

        public int HangarId { get; set; }
        public Hangar Hangar { get; set; }

        public ICollection<UnloadingEquipment> UnloadingEquipments { get; set; } = new List<UnloadingEquipment>();
    }
}
