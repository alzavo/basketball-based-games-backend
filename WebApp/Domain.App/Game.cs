using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.App
{
    public class Game
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; } = null!;
        [MaxLength]
        public string Description { get; set; } = null!;
        [StringLength(30)]
        public string Language { get; set; } = null!;
    }
}
