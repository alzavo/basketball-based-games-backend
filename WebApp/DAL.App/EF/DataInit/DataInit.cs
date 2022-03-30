using Domain.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.App.EF.DataInit
{
    public static class DataInit
    {
        public static void SeedGamesData(AppDbContext ctx) 
        {
            var game33 = new Game
            {
                Name = "33",
                Description = "Simple game with basketball.",
                Language = "en",
            };

            ctx.Games.Add(game33);
            ctx.SaveChanges();
        }
    }
}
