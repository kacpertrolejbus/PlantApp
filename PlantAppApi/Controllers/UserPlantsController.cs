using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlantAppApi.Models;
using PlantAppApi.Services;

namespace PlantAppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserPlantsController(PlantAppDbContext context, AchievementService achievementService) : ControllerBase
    {
        
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetMyPlants(int userId)
        {
            var plants = await context.UserPlants
                .Include(p => p.PLANTSPECIESIdPlantSpeciesNavigation)
                .Where(p => p.USERSIdUsers == userId && p.IsAlive == true)
                .ToListAsync();
            return Ok(plants);
        }

        
        [HttpPost]
        public async Task<IActionResult> AddPlant(int userId, int speciesId, string nickname)
        {
            
            var plant = new UserPlant
            {
                USERSIdUsers = userId,
                PLANTSPECIESIdPlantSpecies = speciesId,
                Nickname = nickname
            };
            context.UserPlants.Add(plant);
            await context.SaveChangesAsync();

            
            var species = await context.PlantSpecies.FindAsync(speciesId);
            int days = species?.WaterFrequency ?? 7;

            
            context.Schedules.Add(new Schedule
            {
                USERPLANTIdUserPlant = plant.IdUserPlant,
                NextDueDate = DateTime.Now.AddDays(days),
                IntervalsDays = days
                
            });
            await context.SaveChangesAsync();

            
            await achievementService.CheckAchievements(userId);

            return Ok(new { msg = "Dodano", id = plant.IdUserPlant });
        }

       
        [HttpPut("{plantId}/status")]
        public async Task<IActionResult> SetStatus(int plantId, bool isAlive)
        {
            var plant = await context.UserPlants.FindAsync(plantId);
            if (plant == null) return NotFound();

            plant.IsAlive = isAlive;
            if (!isAlive) plant.DeathDate = DateTime.Now;

            await context.SaveChangesAsync();
            await achievementService.CheckAchievements(plant.USERSIdUsers); 

            return Ok("Zaktualizowano");
        }
    }
}