using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlantAppApi.Models
{
    [Table("PLANT_SPECIES_has_DISEAS_PESTS")]
    public class PlantSpeciesHasDiseasesPest
    {
        [Column("PLANT_SPECIES_idPlantSpecies")]
        public int PLANTSPECIESIdPlantSpecies { get; set; }

        [Column("DISEASES_PESTS_idDiseasesPests")]
        public int DISEASESPESTSIdDiseasesPests { get; set; }

        [ForeignKey("PLANTSPECIESIdPlantSpecies")]
        public virtual PlantSpecies PLANTSPECIESIdPlantSpeciesNavigation { get; set; } = null!;

        [ForeignKey("DISEASESPESTSIdDiseasesPests")]
        public virtual DiseasesPest DISEASESPESTSIdDiseasesPestsNavigation { get; set; } = null!;
    }
}