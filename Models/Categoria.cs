using System.ComponentModel.DataAnnotations; //para as validações
using System.ComponentModel.DataAnnotations.Schema; // para os schemas do banco

namespace Shop.Models
{

    // [Table("CATEGORIAS")] Caso queria usar um nome expecífico para a tabela
    public class Categoria
    {
        [Key]
        //[Column("CATEGORIA_ID")] Caso queria um nome expecífico para a coluna da tabela
        public int Id { get; set; }

        [Required(ErrorMessage = "Esse campo é obrigatório")]
        [MaxLength(60, ErrorMessage = "Esse campo deve conter de 3 a 60 caracteres")]
        [MinLength(3, ErrorMessage = "Esse campo deve conter de 3 a 60 caracteres")]
        //[DataType("nvarchar")] para setar o tipo de dado da coluna
        public string Titulo { get; set; }
        
    }
    
}