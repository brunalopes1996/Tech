using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tech.Models;

[Table("banca")]
    public class Banca
    {
        [Key]
        public int Id { get; set; }   

        [Required(ErrorMessage = "Por favor, informe o nome")]
        [StringLength(30, ErrorMessage =  "O nome deve possuir no máximo 30 caracteres")]
        public string Nome { get; set; }

        [Required]
        [StringLength(30)]
        public string Tipo { get; set; }

        public virtual ICollection<Questionario> Questionarios { get; set; }
    }
