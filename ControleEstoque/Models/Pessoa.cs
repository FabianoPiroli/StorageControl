using System.ComponentModel.DataAnnotations;

namespace ControleEstoque.Models
{
    public class Pessoa
    {
        public int Id { get; set; }

        [StringLength(150)]
        public string? Nome { get; set; }

        [StringLength(13)]
        public string? Cpf { get; set; }

        [StringLength(18)]
        public string? Cnpj { get; set; }


    }
}
