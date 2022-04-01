using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicApi.DTO.v1.Mapper
{
    public class PlayedGameMapper
    {
        public static PlayedGame Map(Domain.App.PlayedGame playedGame)
        {
            return new PlayedGame()
            {
                Id = playedGame.Id,
                Points = playedGame.Points,
                Place = playedGame.Place,
                UserId = playedGame.UserId,
                UserName = playedGame.User.UserName,
                GameId = playedGame.GameId,
                GameName = playedGame.Game.Name,
            };
        }

        public static Domain.App.PlayedGame Map(PlayedGameUpdate playedGame)
        {
            return new Domain.App.PlayedGame()
            {
                Id = playedGame.Id,
                Points = playedGame.Points,
                Place = playedGame.Place,
                UserId = playedGame.UserId,
                GameId = playedGame.GameId,
            };
        }

        public static Domain.App.PlayedGame Map(PlayedGameCreate playedGame)
        {
            return new Domain.App.PlayedGame()
            {
                Points = playedGame.Points,
                Place = playedGame.Place,
                UserId = playedGame.UserId,
                GameId = playedGame.GameId,
            };
        }
    }
}
