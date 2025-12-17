using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlantAppApi.Models;
using PlantAppApi.Services;

namespace PlantAppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActionsController(PlantAppDbContext context, AchievementService achievementService) : ControllerBase
    {
        public class ActionReq { public int PlantId { get; set; } public string Type { get; set; } public string Note { get; set; } }

        [HttpPost("confirm")]
        public async Task<IActionResult> Confirm([FromBody] ActionReq req)
        {
            using var trans = context.Database.BeginTransaction();
            try
            {
              
                var schedule = await context.Schedules
                    .Include(s => s.USERPLANTIdUserPlantNavigation)
                    .FirstOrDefaultAsync(s => s.USERPLANTIdUserPlant == req.PlantId && s.ActionType == req.Type);

                if (schedule == null) return NotFound("Brak harmonogramu");

                
                context.CareLogs.Add(new CareLog
                {
                    USERPLANTIdUserPlant = req.PlantId,
                    ActionType = req.Type,
                    Notes = req.Note,
                    ActionDate = DateTime.Now
                });

                
                schedule.LastDueDate = DateTime.Now;
                schedule.NextDueDate = DateTime.Now.AddDays(schedule.IntervalsDays ?? 7);

                
                var user = await context.Users.FindAsync(schedule.USERPLANTIdUserPlantNavigation.USERSIdUsers);
                if (user != null) user.ExperiencePoints += 10;

                await context.SaveChangesAsync();
                await trans.CommitAsync();

                
                await achievementService.CheckAchievements(user.IdUsers);

                return Ok("Wykonano!");
            }
            catch { await trans.RollbackAsync(); return StatusCode(500); }
        }
    }
}