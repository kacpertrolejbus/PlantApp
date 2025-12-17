using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlantAppApi.Models
{
    [Table("DISEASES_PESTS")]
    public class DiseasesPest
    {
        [Key]
        [Column("idDiseasesPests")]
        public int IdDiseasesPests { get; set; }

        [Column("name")]
        public string Name { get; set; } = null!;

        [Column("treatmentTips")]
        public string? TreatmentTips { get; set; }

        [Column("type")]
        public string? Type { get; set; }

        public virtual ICollection<PlantSpeciesHasDiseasesPest> PlantSpeciesHasDiseasesPests { get; set; } = new List<PlantSpeciesHasDiseasesPest>();
    }
}