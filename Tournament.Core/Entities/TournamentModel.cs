using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tournament.Core.Entities
{
    public class TournamentModel
    {
       public int Id { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "The title cannot exceed 30 characters.")]
        public string Title { get; set; }
       public DateTime StartDate { get; set; }

        // Nav prop
       public ICollection<Game> Games { get; set; } = new List<Game>();

    }
}
