using Microsoft.EntityFrameworkCore;
using PlantAppApi.Models;

namespace PlantAppApi.Services
{
    public class AchievementService(PlantAppDbContext context)
    {
        public async Task CheckAchievements(int userId)
        {
            var user = await context.Users
                .Include(u => u.UserPlants)
                .FirstOrDefaultAsync(u => u.IdUsers == userId);

            if (user == null) return;

            
            int plantsOwned = user.UserPlants.Count(p => p.IsAlive == true);
            int plantsKilled = user.UserPlants.Count(p => p.IsAlive == false);

            
            var allAchievements = await context.Achievements.ToListAsync();
            var myAchievementsIds = await context.UsersHasAchievements
                .Where(ua => ua.USERSIdUsers == userId)
                .Select(ua => ua.ACHIEVEMENTSIdAchievements)
                .ToListAsync();

            foreach (var ach in allAchievements.Where(a => !myAchievementsIds.Contains(a.IdAchievements)))
            {
                bool unlocked = false;
                
                switch (ach.ConditionType)
                {
                    case "PLANTS_OWNED":
                        if (plantsOwned >= 1 && ach.Name.Contains("Początkujący")) unlocked = true;
                        if (plantsOwned >= 5 && ach.Name.Contains("Kolekcjoner")) unlocked = true;
                        break;
                    case "PLANTS_KILLED":
                        if (plantsKilled >= 3) unlocked = true;
                        break;
                }

                if (unlocked)
                {
                    context.UsersHasAchievements.Add(new UsersHasAchievement
                    {
                        USERSIdUsers = userId,
                        ACHIEVEMENTSIdAchievements = ach.IdAchievements,
                        DateAwarded = DateTime.Now
                    });
                }
            }
            if (context.ChangeTracker.HasChanges()) await context.SaveChangesAsync();
        }
    }
}