using AutoMapper;
using BecasApi.Entidades;
using BecasApi.Entidades.DTOS;
using BecasApi.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BecasApi.Controllers
{
    [Route("api/alumnos")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class AlumnosController : ControllerBase
    {
        private readonly ILogger<AlumnosController> logger;
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        public AlumnosController(ILogger<AlumnosController> logger, ApplicationDbContext context, IMapper mapper)
        {
            this.logger = logger;
            this.context = context;
            this.mapper = mapper;
        }
        [HttpGet("listaalumnos")]
        public async Task<ActionResult<List<Alumno>>> ObtenerAlumnos()
        {
            return await context.Alumnos.ToListAsync();
        }

        [HttpGet("alumnosFiltroBeca/{id:int}")]
        public async Task<ActionResult<List<Alumno>>> ObtenerAlumnosBeca(int id)
        {
            var alumnos = context.Alumnos.AsQueryable();
            alumnos = alumnos.Where(x => x.BecasAlumnos.Select(y => y.BecaId).Contains(id));
            return await alumnos.ToListAsync();
          }
        [HttpGet("alumnosBecados/{id:int}")]
        public async Task<ActionResult<List<Alumno>>> ObtenerAlumnosConBeca(int id)
        {
            var alumnos = context.Alumnos.AsQueryable();
            if (id == 1)
            {
                alumnos = alumnos.Where(x => x.BecasAlumnos.Select(y => y.AlumnoId).Contains(x.Id));
            }
            else if (id == 2)
            {

            }
            
            return await alumnos.ToListAsync();
        }
        [HttpGet("listaBecas")]
        public async Task<ActionResult<List<BecasDTO>>> ObtenerBecas()
        {
            var becas = await context.Becas.ToListAsync();
            return mapper.Map<List<BecasDTO>>(becas); ;
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Alumno>> ObtenerAlumno([BindRequired] int id)
        {
            var alumno = await context.Alumnos.FirstOrDefaultAsync(x => x.Id == id);
            if (alumno == null)
            {
                return NotFound();
            }
            return alumno;
        }
        [HttpGet("alumno/{id:int}")]
        public async Task<ActionResult<AlumnoPostGetDTO>> ObtenerAlumnoBeca([BindRequired] int id)
        {
            var alumno = await context.Alumnos.Include(x => x.BecasAlumnos).ThenInclude(x => x.Becas).FirstOrDefaultAsync(x => x.Id == id);
            if (alumno == null) { return NotFound(); }
            var alumnobase = mapper.Map<AlumnoBecaDTO>(alumno);
            var becasSeleccionadas = alumnobase.Becas.Select(x => x.Id).ToList();
            var becasNoSeleccionadas = await context.Becas.Where(x => !becasSeleccionadas.Contains(x.Id)).ToListAsync();

            var becasNoseleccionadasDTO = mapper.Map<List<BecasDTO>>(becasNoSeleccionadas);
            var respuesta = new AlumnoPostGetDTO();
            respuesta.Alumno = mapper.Map<AlumnoDTO>(alumnobase);
            respuesta.BecasSeleccionadas = alumnobase.Becas;
            respuesta.BecasNoSeleccionadas = becasNoseleccionadasDTO;
            return respuesta;
        }


        [HttpPost("agregar")]
        public async Task<ActionResult> AgregarAlumno([FromForm] AlumnoDTO alumnoDTO)
        {
            var alumno = mapper.Map<Alumno>(alumnoDTO);
            context.Add(alumno);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> ActualizarAlumno(int id, [FromForm] AlumnoDTO alumno)
        {
            var alumnobase = await context.Alumnos
                .Include(x=>x.BecasAlumnos)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (alumnobase == null)
            {
                return NotFound();
            }

            alumnobase = mapper.Map(alumno, alumnobase);
            await context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> EliminarAlumno(int id)
        {
            context.Remove(new Alumno() { Id = id });
            await context.SaveChangesAsync();
            return NoContent();
        }


    }
}
