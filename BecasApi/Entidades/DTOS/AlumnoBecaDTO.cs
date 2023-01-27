using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BecasApi.Entidades.DTOS
{
    public class AlumnoBecaDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool Genero { get; set; }
        public int Edad { get; set; }
        public List<BecasDTO> Becas { get; set; }
    }
}
