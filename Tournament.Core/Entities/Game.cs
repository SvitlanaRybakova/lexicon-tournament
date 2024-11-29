using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tournament.Core.Entities
{
    public class Game
    {
        public int Id { get; set; }
        [Required]
        [StringLength(30, ErrorMessage = "The title cannot exceed 30 characters.")]
        public string Title { get; set; }
        public DateTime Time { get; set; }
        public string Description { get; set; }

        // FK
       public int TournamentModelId { get; set; }
    }
}
