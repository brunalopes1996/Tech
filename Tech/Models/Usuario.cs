using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.ConstrainedExecution;
using Microsoft.AspNetCore.Identity;

namespace Tech.Models;

[Table("usuario")]
    public class Usuario : IdentityUser
    {
        [Required(ErrorMessage = "Por favor, informe o Nome")]
        [StringLength(60, ErrorMessage = "O nome deve possuir no máximo 60 caracteres")]
        public string Nome { get; set; }

        [StringLength(11, ErrorMessage = "O telefone deve possuir 11 caracteres")]
        public int Telefone { get; set; }

        [StringLength(300)]
        public string Foto { get; set; }
    }
