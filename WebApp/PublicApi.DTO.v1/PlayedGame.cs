using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicApi.DTO.v1
{
    public class PlayedGame
    {
        public int Id { get; set; }

        public int Place { get; set; }
        public int Points { get; set; }

        public int UserId { get; set; }
        public string UserName { get; set; } = null!;

        public int GameId { get; set; }
        public string GameName { get; set; } = null!;
    }

    public class PlayedGameAllUsers
    {
        public ICollection<PlayedGame> PlayedGames { get; set; } = null!;
    }
}
