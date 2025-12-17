using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlantAppApi.Models
{
    [Table("PLANT_SPECIES")]
    public class PlantSpecies
    {
        [Key]
        [Column("idPlantSpecies")]
        public int IdPlantSpecies { get; set; }

        [Column("name")]
        public string Name { get; set; } = null!;

        [Column("lightPreference")]
        public string? LightPreference { get; set; }

        [Column("waterFrequency")]
        public int? WaterFrequency { get; set; }

        [Column("soilType")]
        public string? SoilType { get; set; }

        [Column("hydroponicReady")]
        public bool? HydroponicReady { get; set; }

        // Relacje
        public virtual ICollection<UserPlant> UserPlants { get; set; } = new List<UserPlant>();
        public virtual ICollection<PlantSpeciesHasDiseasesPest> PlantSpeciesHasDiseasesPests { get; set; } = new List<PlantSpeciesHasDiseasesPest>();
    }
}