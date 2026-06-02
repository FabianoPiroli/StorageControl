using System.ComponentModel.DataAnnotations;

namespace ControleEstoque.Models
{
    public class Categoria
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome da categoria é obrigatório.")]
        [StringLength(100)]
        [Display(Name = "Nome da Categoria")]
        public string Nome { get; set; }

        public string Descricao { get; set; }

        public ICollection<Produto>? Produtos { get; set; }
    }
}
