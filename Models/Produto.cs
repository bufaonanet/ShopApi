using System.ComponentModel.DataAnnotations; //para as validações

namespace Shop.Models
{
    public class Produto
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Esse campo é obrigatório")]
        [MaxLength(60, ErrorMessage = "Esse campo deve conter de 3 a 60 caracteres")]
        [MinLength(3, ErrorMessage = "Esse campo deve conter de 3 a 60 caracteres")]
        public string Titulo { get; set; }

        [MaxLength(1024, ErrorMessage = "Esse campo deve conter um máximo de 2014 caracteres")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Esse campo é obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage = "Preço deve ser maior que zero")]
        public decimal Preco { get; set; }

        [Required(ErrorMessage = "Esse campo é obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage = "Categoria Inválida")]
        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }


    }

}