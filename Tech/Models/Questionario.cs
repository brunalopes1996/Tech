using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tech.Models;

[Table("questionario")]
    public class Questionario
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(300000)]
        public string Nome { get; set; }

        [Required]
        public int BancaId { get; set; }

        [ForeignKey(nameof(BancaId))]
        public Banca Banca { get; set; }

        public virtual ICollection<Questoes> Questoes { get; set; }

    }
