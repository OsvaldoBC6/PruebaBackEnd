using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using BecasApi.Model.Utils;

namespace BecasApi.Entidades
{
    public class Alumno
    {
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength:50)]
        public string Nombre { get; set; }
        [Required]
        public bool Genero { get; set; }
        [Required]
        public int Edad { get; set; }
        public List<BecasAlumnos> BecasAlumnos { get; set; }
    }
}
