using System.ComponentModel.DataAnnotations; //para as validações

namespace Shop.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Esse campo é obrigatório")]
        [MaxLength(20, ErrorMessage = "Esse campo deve conter de 3 a 20 caracteres")]
        [MinLength(3, ErrorMessage = "Esse campo deve conter de 3 a 20 caracteres")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Esse campo é obrigatório")]
        [MaxLength(20, ErrorMessage = "Esse campo deve conter de 3 a 20 caracteres")]
        [MinLength(3, ErrorMessage = "Esse campo deve conter de 3 a 20 caracteres")]
        public string Senha { get; set; }

        public string NivelAcesso { get; set; }
    }

}