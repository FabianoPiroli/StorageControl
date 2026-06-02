using System.ComponentModel.DataAnnotations;

namespace ControleEstoque.Models
{
    public class Produto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do produto é obrigatório.")]
        [StringLength(150)]
        public string Nome { get; set; }

        [StringLength(50)]
        public string Sku { get; set; }

        [Display(Name = "Preço de Custo")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = true)]
        public decimal PrecoCusto { get; set; }

        [Display(Name = "Preço de Venda")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = true)]
        public decimal PrecoVenda { get; set; }

        [Display(Name = "Quantidade Atual")]
        public int QuantidadeAtual { get; set; }

        [Display(Name = "Estoque Mínimo")]
        public int EstoqueMinimo { get; set; }

        [Display(Name = "Categoria")]
        public int CategoriaId { get; set; }
        public Categoria? Categoria { get; set; }

        [Display(Name = "Fornecedor")]
        public int FornecedorId { get; set; }
        public Fornecedor? Fornecedor { get; set; }

        public ICollection<MovimentacaoEstoque>? Movimentacoes { get; set; }
    }
}
