using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tech.Models;

[Table("questionario")]
    public class Questionario
    {
        [Key]
        public int Id { get; set; }

        [StringLength(300)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Por favor, informe a Banca")]
        public int BancaId { get; set; }

        [ForeignKey(nameof(BancaId))]
        public Banca Banca { get; set; }

    }
