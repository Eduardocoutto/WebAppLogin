using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebAppLogin.Models
{
    public class UsuarioEdit
    {
        [Key]
        public int IdUsuario { get; set; }
        [Required(ErrorMessage = "Preencha o campo Email")]
        [MaxLength(150, ErrorMessage = "Máximo de 150 caracteres")]
        [EmailAddress(ErrorMessage = "Preencha um e-mail válido")]
        public string Email { get; set; }
        [StringLength(40, MinimumLength = 3)]
        [Required]
        public string Login { get; set; }
        [Required]
        public bool Status { get; set; }
        [Required]
        public bool Admin { get; set; }
    }
}