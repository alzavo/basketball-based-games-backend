using System.ComponentModel.DataAnnotations;

namespace Domain.App
{
    public class PlayedGame 
    {
        [Key]
        [Required]
        public int Id { get; set; }

        public int Place { get; set; }
        public int Points { get; set; }

        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public int GameId { get; set; }
        public Game Game { get; set; } = null!;
    }
}
