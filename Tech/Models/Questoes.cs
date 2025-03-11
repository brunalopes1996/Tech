
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tech.Models;

[Table("questoes")]
    public class Questoes
{
    [Key]
      public int Id { get; set; }  

      public string Questao { get; set; }

        [StringLength(1000)]

      public string Alternativa_A { get; set; }
        [StringLength(1000)]

      public string Alternativa_B { get; set; }
        [StringLength(1000)]


      public string Alternativa_C { get; set; }
      
         [StringLength(1000)]

      public string Alternativa_D { get; set; }
        [StringLength(1000)]

      public string Alternativa_E { get; set; }
      
       [StringLength(1000)]

      public string Alternativa_Correta { get; set; }
       [StringLength(1000)]

      public string Imagem { get; set; }
      
    [Required(ErrorMessage = "Questão incorreta")]
     public int QuestionarioId { get; set; }
    

    [ForeignKey(nameof(QuestionarioId))]
    public Questionario Questionario { get; set; }


        
}

