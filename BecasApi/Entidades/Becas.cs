using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BecasApi.Entidades
{
    public class Becas
    {
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        public List<BecasAlumnos> BecasAlumnos { get; set; }
    }
}
