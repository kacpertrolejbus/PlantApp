using Microsoft.EntityFrameworkCore;

namespace PlantAppApi.Models 
{
    public class PlantAppDbContext : DbContext
    {
        public PlantAppDbContext(DbContextOptions<PlantAppDbContext> options) : base(options) { }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<PlantSpecies> PlantSpecies { get; set; }
        public virtual DbSet<UserPlant> UserPlants { get; set; }
        public virtual DbSet<Schedule> Schedules { get; set; }
        public virtual DbSet<CareLog> CareLogs { get; set; }
        public virtual DbSet<Achievement> Achievements { get; set; }
        public virtual DbSet<UsersHasAchievement> UsersHasAchievements { get; set; }
        public virtual DbSet<DiseasesPest> DiseasesPests { get; set; }
        public virtual DbSet<PlantSpeciesHasDiseasesPest> PlantSpeciesHasDiseasPests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UsersHasAchievement>()
                .HasKey(ua => new { ua.USERSIdUsers, ua.ACHIEVEMENTSIdAchievements });
            modelBuilder.Entity<PlantSpeciesHasDiseasesPest>()
                .HasKey(pd => new { pd.PLANTSPECIESIdPlantSpecies, pd.DISEASESPESTSIdDiseasesPests });
        }
    }
}