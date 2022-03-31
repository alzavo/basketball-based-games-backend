namespace PublicApi.DTO.v1.Mapper
{
    public class GameMapper
    {
        public static Game Map(Domain.App.Game game)
        {
            return new Game() 
            {
                Id = game.Id,
                Name = game.Name,
                Description = game.Description,
                Language = game.Language,
            };
        }

        public static Domain.App.Game Map(GameCreate game)
        {
            return new Domain.App.Game()
            {
                Name = game.Name,
                Description = game.Description,
                Language = game.Language,
            };
        }

        public static Domain.App.Game Map(GameUpdate game)
        {
            return new Domain.App.Game()
            {
                Id = game.Id,
                Name = game.Name,
                Description = game.Description,
                Language = game.Language,
            };
        }
    }
}
