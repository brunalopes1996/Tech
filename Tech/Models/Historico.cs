using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tech.Models;

[Table("historico")]
    public class Historico
    {
        [Key]
        public int Id { get; set; }

        [StringLength(300)]
        public string Erro { get; set; }
        
        [StringLength(300)]
        public string Acerto { get; set; }

        [Required(ErrorMessage = "Por favor, informe a Banca")]
        public int UsuarioId { get; set; }
        [ForeignKey(nameof(UsuarioId))]
        public Usuario Usuario { get; set; }

         [Required(ErrorMessage = "Questão incorreta")]
         public int QuestionarioId { get; set; }
    

          [ForeignKey(nameof(QuestionarioId))]
         public Questionario Questionario { get; set; }


    }        
