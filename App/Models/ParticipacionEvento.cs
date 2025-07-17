using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIGRAUM2025.Models
{
    public class ParticipacionEvento
    {

        [Key]
        public int IdParticipacionEvento { get; set; }

        [Required]
        public int IdAtleta { get; set; }

        [Required]
        public int IdEvento { get; set; }

        [MaxLength(50)]
        public string? Resultado { get; set; }

        [NotMapped]
        public string? NombreEvento { get; set; }

    }
}
