using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlantAppApi.Models
{
    [Table("CARE_LOG")]
    public class CareLog
    {
        [Key]
        [Column("idCareLog")]
        public int IdCareLog { get; set; }

        [Column("actionType")]
        public string? ActionType { get; set; }

        [Column("actionDate")]
        public DateTime? ActionDate { get; set; }

        [Column("notes")]
        public string? Notes { get; set; }

        [Column("USER_PLANT_idUserPlant")]
        public int USERPLANTIdUserPlant { get; set; }

        [ForeignKey("USERPLANTIdUserPlant")]
        public virtual UserPlant? USERPLANTIdUserPlantNavigation { get; set; }
    }
}