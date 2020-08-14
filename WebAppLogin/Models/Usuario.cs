using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAppLogin.Models
{
    public class Usuario
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
        public string Senha { get; set; }
        [Required]
        public bool Status { get; set; }
        [Required]
        public bool Admin { get; set; }

        public UsuarioEdit GetUserEdit()
        {
            return new UsuarioEdit 
            {
                Admin = Admin,
                Login = Login,
                Status = Status,
                Email = Email,
                IdUsuario = IdUsuario
            };
        }
    }
}