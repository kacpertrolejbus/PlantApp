using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlantAppApi.Models
{
    [Table("USERS")]
    public class User
    {
        [Key]
        [Column("idUsers")]
        public int IdUsers { get; set; }

        [Column("email")]
        public string Email { get; set; } = null!;

        [Column("password")]
        public string Password { get; set; } = null!;

        [Column("createdAt")]
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow; 

        [Column("experience_points")]
        public double? ExperiencePoints { get; set; } = 0; 

        public virtual ICollection<UserPlant> UserPlants { get; set; } = new List<UserPlant>();
        public virtual ICollection<UsersHasAchievement> UsersHasAchievements { get; set; } = new List<UsersHasAchievement>();
    }
}