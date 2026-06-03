using System.ComponentModel.DataAnnotations;

namespace ControleEstoque.Models
{
    public class Endereco
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string? Nome { get; set; }
        [StringLength(50)]
        public string? Cep { get; set; }
        [StringLength(50)]
        public string? Rua { get; set; }
        [StringLength(50)]
        public Cidade? Cidade { get; set; }
        public string? Complemento { get; set; }
     }
}
