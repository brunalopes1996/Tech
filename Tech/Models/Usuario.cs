using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.ConstrainedExecution;
using Microsoft.AspNetCore.Identity;

namespace Tech.Models;

[Table("usuario")]
    public class Usuario : IdentityUser

    {

        [Key]
        public int Id_Usuario { get; set; }

        [Required(ErrorMessage = "Por favor, informe o Nome")]
        [StringLength(100, ErrorMessage = "O nome deve possuir no máximo 60 caracteres")]
        public string Nome { get; set; }

        [Required]
        [StringLength(150)]
        [EmailAddress]
        public string Email { get; set; }

        [StringLength(15, ErrorMessage = "O telefone deve possuir 11 caracteres")]
        public int Telefone { get; set; }

        [StringLength(300)]
        public string Foto { get; set; }

        public virtual ICollection<Historico> Historicos { get; set; }
    }
