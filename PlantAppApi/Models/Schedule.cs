using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlantAppApi.Models
{
    [Table("SCHEDULES")]
    public class Schedule
    {
        [Key]
        [Column("idSchedules")]
        public int IdSchedules { get; set; }

        [Column("actionType")]
        public string? ActionType { get; set; } = "Podlewanie";

        [Column("intervalsDays")]
        public int? IntervalsDays { get; set; }

        [Column("lastDueDate")]
        public DateTime? LastDueDate { get; set; } = DateTime.UtcNow; 

        [Column("nextDueDate")]
        public DateTime? NextDueDate { get; set; }

        [Column("isActive")]
        public bool? IsActive { get; set; } = true;

        [Column("USER_PLANT_idUserPlant")]
        public int USERPLANTIdUserPlant { get; set; }

        [ForeignKey("USERPLANTIdUserPlant")]
        public virtual UserPlant? USERPLANTIdUserPlantNavigation { get; set; }
    }
}