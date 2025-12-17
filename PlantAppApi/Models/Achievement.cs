using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlantAppApi.Models
{
    [Table("ACHIEVEMENTS")]
    public class Achievement
    {
        [Key]
        [Column("idAchievements")]
        public int IdAchievements { get; set; }

        [Column("name")]
        public string Name { get; set; } = null!;

        [Column("description")]
        public string? Description { get; set; }

        [Column("conditionType")]
        public string? ConditionType { get; set; }

        [Column("iconURL")]
        public string? IconURL { get; set; }

        public virtual ICollection<UsersHasAchievement> UsersHasAchievements { get; set; } = new List<UsersHasAchievement>();
    }
}