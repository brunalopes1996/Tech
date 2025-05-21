using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tech.Models;

[Table("historico")]
public class Historico
{
    [Key]
    public int Id { get; set; }
    
    public bool Acertou { get; set; }
  
    public string Erro { get; set; }
    public string Acerto { get; set; }

 
    public DateTime DataResposta { get; set; } = DateTime.Now;

    [Required]
    public string UsuarioId { get; set; }
    [ForeignKey(nameof(UsuarioId))]
    public Usuario Usuario { get; set; }

    [Required(ErrorMessage = "Questão incorreta")]
    public int QuestionarioId { get; set; }
    [ForeignKey(nameof(QuestionarioId))]
    public Questionario Questionario { get; set; }

    [Required]
    public int QuestaoId { get; set; }
    [ForeignKey(nameof(QuestaoId))]
    public Questoes Questao { get; set; }


    [Required]
    [StringLength(1)]
    public string AlternativaSelecionada { get; set; }
}
