using AutoMapper;
using BecasApi.Entidades;
using BecasApi.Entidades.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BecasApi.Model.Utils
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<BecasDTO, Becas>().ReverseMap();
            CreateMap<AlumnoDTO, Alumno>()
                .ForMember(x => x.BecasAlumnos, opciones => opciones.MapFrom(MapearBecasAlumnos));

            CreateMap<Alumno, AlumnoBecaDTO>()
                .ForMember(x => x.Becas, options => options.MapFrom(MapearBecasAlumnos));
            CreateMap<AlumnoDTO, AlumnoBecaDTO>().ReverseMap();
        }
        private List<BecasDTO> MapearBecasAlumnos(Alumno alumno,AlumnoBecaDTO alumnoBecaDTO)
        {
            var resultado = new List<BecasDTO>();

            if (alumno.BecasAlumnos != null)
            {
                foreach(var beca in alumno.BecasAlumnos)
                {
                    resultado.Add(new BecasDTO() { Id = beca.BecaId, Nombre = beca.Becas.Nombre });
                }
            }
            return resultado;
        }
        private List<BecasAlumnos> MapearBecasAlumnos(AlumnoDTO alumnoDTO,Alumno alumno)
        {
            var resultado = new List<BecasAlumnos>();
            if (alumnoDTO.BecasIds == null)
            {
                return resultado;
            }

            foreach( var id in alumnoDTO.BecasIds)
            {
                resultado.Add(new BecasAlumnos() {BecaId=id ,BecasId=id});
            }
            return resultado;
        }
    }

}
