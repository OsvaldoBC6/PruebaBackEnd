using BecasApi.Model.Utils;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BecasApi.Entidades.DTOS
{
    public class AlumnoDTO
    {
        public int Id { get; set; }
        [StringLength(maximumLength: 50)]
        public string Nombre { get; set; }
        [Required]
        public bool Genero { get; set; }
        [Required]
        public int Edad { get; set; }
        [ModelBinder(BinderType = typeof(TypeBinder<List<int>>))]
        public List<int> BecasIds { get; set; }

    }
}
