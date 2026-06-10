using System.ComponentModel.DataAnnotations;

namespace ControleEstoque.Models
{
    public class Fornecedor
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome/razão social é obrigatório.")]
        [StringLength(200)]
        [Display(Name = "Nome/Razão Social")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O CNPJ é obrigatório.")]
        [StringLength(18)]
        public string Cnpj { get; set; }

        [StringLength(20)]
        public string Telefone { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [StringLength(200)]
        public string Endereco { get; set; }


        public ICollection<Produto>? Produtos { get; set; }
    }
}
