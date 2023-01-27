using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BecasApi.Entidades.DTOS
{
    public class AlumnoPostGetDTO
    {
        public AlumnoDTO Alumno { get; set; }
        public List<BecasDTO> BecasSeleccionadas { get; set; }
        public List<BecasDTO> BecasNoSeleccionadas { get; set; }
    }
}
