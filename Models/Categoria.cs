using System.ComponentModel.DataAnnotations;

namespace Shop.Models
{

    public class Categoria
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Esse campo é obrigatório")]
        [MaxLength(60, ErrorMessage = "Esse campo deve conter de 3 a 60 caracteres")]
        [MinLength(60, ErrorMessage = "Esse campo deve conter de 3 a 60 caracteres")]
        public string Titulo { get; set; }
        
    }
    
}