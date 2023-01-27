using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BecasApi.Entidades.DTOS
{
    public class CredencialesDTO
    {
        [EmailAddress]
        [Required]
        public string correo { get; set; }
        [Required]
        public string contrasena { get; set; }

    }
}
