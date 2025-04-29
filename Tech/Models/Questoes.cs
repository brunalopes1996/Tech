
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
  public string AlternativaA { get; set; }

  [Required]
  [StringLength(1000)]
  public string AlternativaB { get; set; }

  [Required]
  [StringLength(1000)]
  public string AlternativaC { get; set; }

  [Required]
  [StringLength(1000)]
  public string AlternativaD { get; set; }

  [Required]
  [StringLength(1000)]
  public string AlternativaE { get; set; }

  [Required]
  [StringLength(1000)]
  public string AlternativaCorreta { get; set; }
  [StringLength(1000)]

  public string Imagem { get; set; }

  [ForeignKey(nameof(QuestionarioId))]
  [Required(ErrorMessage = "Questão incorreta")]
  public int QuestionarioId { get; set; }

  public virtual Questionario Questionario { get; set; }
}


