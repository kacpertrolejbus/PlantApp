using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlantAppApi.Models
{
    [Table("USER_PLANT")]
    public class UserPlant
    {
        [Key]
        [Column("idUserPlant")]
        public int IdUserPlant { get; set; }

        [Column("nickname")]
        public string? Nickname { get; set; }

        [Column("photoURL")]
        public string? PhotoUrl { get; set; } = "default.png"; 

        [Column("acquisitionDate")]
        public DateTime? AcquisitionDate { get; set; } = DateTime.UtcNow; 

        [Column("isAlive")]
        public bool? IsAlive { get; set; } = true; 

        [Column("deathDate")]
        public DateTime? DeathDate { get; set; } 
        [Column("USERS_idUsers")]
        public int USERSIdUsers { get; set; }

        [Column("PLANT_SPECIES_idPlantSpecies")]
        public int PLANTSPECIESIdPlantSpecies { get; set; }

        [ForeignKey("USERSIdUsers")]
        public virtual User? USERSIdUsersNavigation { get; set; }

        [ForeignKey("PLANTSPECIESIdPlantSpecies")]
        public virtual PlantSpecies? PLANTSPECIESIdPlantSpeciesNavigation { get; set; }

        public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();
    }
}