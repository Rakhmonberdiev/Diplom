using Diplom.Entities;
using Microsoft.AspNetCore.Identity;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace Diplom.Data.Seed
{
    public class SeedData
    {
        public static async Task SeedDistricts(AppDbContext dbContext, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            if(dbContext.Districts.Any())
            {
                return;
            }
            var districtsData = await File.ReadAllTextAsync("Data/Seed/districts.json", Encoding.UTF8);
            var opt = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
            };
            var districts = JsonSerializer.Deserialize<List<DistrictsEn>>(districtsData,opt);
           

            foreach(var district in districts)
            {
                await dbContext.Districts.AddAsync(district);
                await dbContext.SaveChangesAsync();
            }
            var scheduleData = await File.ReadAllTextAsync("Data/Seed/schedule.json", Encoding.UTF8);
            var schedules = JsonSerializer.Deserialize<List<Schedule>>(scheduleData, opt);
            foreach(var schedule in schedules)
            {
                await dbContext.Schedules.AddAsync(schedule);
                await dbContext.SaveChangesAsync();
            }

            var roles = new List<IdentityRole>
            {
                new IdentityRole{Name="User"},
                new IdentityRole{Name="Admin"}
            };
            foreach(var role in roles)
            {
                await roleManager.CreateAsync(role);
            }
            var admin = new AppUser
            {
                UserName = "admin",
            };
            await userManager.CreateAsync(admin, "Admin123*");
            await userManager.AddToRolesAsync(admin, new[] { "Admin" });

        }
    }
}
