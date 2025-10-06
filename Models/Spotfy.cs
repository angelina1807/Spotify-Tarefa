using System.ComponentModel.DataAnnotations;

namespace TarefaSpotfyApi.Models
{
    public class Spotfy
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome da música é obrigatório.")]
        public string Musica { get; set; }

        [Required(ErrorMessage = "O nome do artista é obrigatório.")]
        public string Artista { get; set; }

        public string Album { get; set; }

        [Required(ErrorMessage = "O ano é obrigatório.")]
        public int Ano { get; set; }
    }
}
