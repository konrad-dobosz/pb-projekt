using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace pb_projekt.Models
{
    public class Ship
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public double CargoCapacity { get; set; }


        public ICollection<Cargo> Cargoes { get; set; } = new List<Cargo>();

        public ICollection<UnloadingEquipment> UnloadingEquipments { get; set; } = new List<UnloadingEquipment>();

        [Required]
        [MaxLength(100)]
        public string DockingSpace { get; set; }
    }
}
