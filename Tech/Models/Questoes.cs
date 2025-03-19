
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tech.Models;

[Table("questoes")]
    public class Questoes
{
    [Key]
      public int Id { get; set; } 

    [Required]
    [StringLength(500)]
      public string Questao { get; set; }

    [Required]
    [StringLength(1000)]
    public string Alternativa_A { get; set; }

    [Required]
    [StringLength(1000)]
      public string Alternativa_B { get; set; }
        
        [Required]
        [StringLength(1000)]
      public string Alternativa_C { get; set; }
      
         [Required]
         [StringLength(1000)]
      public string Alternativa_D { get; set; }
        
        [Required]
        [StringLength(1000)]
      public string Alternativa_E { get; set; }
      
      [Required]
       [StringLength(1000)]
      public string Alternativa_Correta { get; set; }
       [StringLength(1000)]

      public string Imagem { get; set; }

     [ForeignKey(nameof(QuestionarioId))] 
    [Required(ErrorMessage = "Questão incorreta")]
     public int QuestionarioId { get; set; }
        
    public virtual Questionario Questionario { get; set; }
}
        

