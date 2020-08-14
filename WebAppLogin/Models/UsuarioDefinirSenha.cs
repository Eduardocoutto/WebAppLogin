using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebAppLogin.Models
{
    public class UsuarioDefinirSenha
    {
        [Key]
        public int IdUsuario { get; set; }
        [Required]
        public string Senha { get; set; }
    }
}