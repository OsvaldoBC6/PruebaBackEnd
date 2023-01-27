using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BecasApi.Entidades
{
    public class BecasAlumnos
    {
        public int BecaId { get; set; }
        public int AlumnoId { get; set; }
        public int BecasId { get; set; }
        public Alumno Alumno { get; set; }
        public Becas Becas { get; set; }
    }
}
