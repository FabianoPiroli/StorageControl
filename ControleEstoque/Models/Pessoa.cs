using System.ComponentModel.DataAnnotations;

namespace ControleEstoque.Models
{
    public class Pessoa
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome da pessoa é obrigatório.")]
        [StringLength(150)]
        public string? Nome { get; set; }
        [StringLength(50)]
        public string? Doc { get; set; }


    }
}
