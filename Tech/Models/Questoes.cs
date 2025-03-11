
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tech.Models;

[Table("questoes")]
    public class Questoes
{
    [Key]
      public int Id { get; set; }  

      public string Questao { get; set; }

      public string Alternativa_A { get; set; }

      public string Alternativa_B { get; set; }

      public string Alternativa_C { get; set; }

      public string Alternativa_D { get; set; }

      public string Alternativa_E { get; set; }

      public string Alternativa_Correta { get; set; }

      public string Imagem { get; set; }
   
}

