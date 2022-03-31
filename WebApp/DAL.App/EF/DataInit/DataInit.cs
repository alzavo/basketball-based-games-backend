using Domain.App;

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

            var gameMinus5 = new Game
            {
                Name = "-5",
                Description = "Another game for chidlren.",
                Language = "en",
            };

            var game21 = new Game
            {
                Name = "21",
                Description = "Simple active game.",
                Language = "en",
            };

            ctx.Games.Add(game33);
            ctx.Games.Add(gameMinus5);
            ctx.Games.Add(game21);
            ctx.SaveChanges();
        }
    }
}
