using System.ComponentModel.DataAnnotations;

namespace ControleEstoque.Models
{
    public enum TipoMovimentacao
    {
        Entrada = 1,
        Saida = 2
    }

    public class MovimentacaoEstoque
    {
        public int Id { get; set; }

        [Display(Name = "Produto")]
        public int ProdutoId { get; set; }
        public Produto? Produto { get; set; }

        public TipoMovimentacao Tipo { get; set; }

        [Required(ErrorMessage = "A quantidade é obrigatória.")]
        [Range(1, int.MaxValue, ErrorMessage = "A quantidade deve ser maior que zero.")]
        public int Quantidade { get; set; }

        [Display(Name = "Data da Movimentação")]
        public DateTime DataMovimentacao { get; set; } = DateTime.Now;

        [StringLength(255)]
        [Display(Name = "Observação")]
        public string Observacao { get; set; }
    }
}
