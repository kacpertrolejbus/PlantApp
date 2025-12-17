using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlantAppApi.Models
{
    [Table("USERS_has_ACHIEVEMENTS")]
    public class UsersHasAchievement
    {
        [Column("USERS_idUsers")]
        public int USERSIdUsers { get; set; }

        [Column("ACHIEVEMENTS_idAchievements")]
        public int ACHIEVEMENTSIdAchievements { get; set; }

        [Column("dateAwarded")]
        public DateTime? DateAwarded { get; set; }

        [ForeignKey("USERSIdUsers")]
        public virtual User USERSIdUsersNavigation { get; set; } = null!;

        [ForeignKey("ACHIEVEMENTSIdAchievements")]
        public virtual Achievement ACHIEVEMENTSIdAchievementsNavigation { get; set; } = null!;
    }
}