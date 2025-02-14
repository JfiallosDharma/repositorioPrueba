using Microsoft.AspNetCore.Mvc;
using APInet8.Services;
using APInet8.Models;

namespace APInet8.Controllers
{
    [ApiController]
    [Route("api/[controller]")]//la ruta por la que voy acceder a este controlador y sus verbos
    public class PersonasController : ControllerBase
    {
        private readonly PersonaServices _services;//variable de tipo PersonaServices, accedo a los metodos creados en PersonaServices

        //constructor
        public PersonasController(PersonaServices services)
        {
            this._services = services;
        }
        [HttpGet]
        public async Task<IEnumerable<Personas>> GetEmpleados()
        {
            return await _services.GetEmpleados();
        }
        [HttpGet("{IDEmpleado}")]
        public async Task<IEnumerable<Personas>> GetEmpleadoEspecifico(int IDEmpleado)
        {
            return await _services.GetEmpleadoEspecifico(IDEmpleado);
        }
        [HttpPost]
        [Route("InsertEmpleado")]
        public async Task<bool> InsertEmpleado(Personas empleado)
        {
            var res = await _services.InsertEmpleado(empleado);
            return res;
        } 
       [HttpPost]
        [Route("UpdateEmpleado")]
        public async Task<bool> UpdateEmpleado(Personas empleado)
        {
            var res = await _services.ActualizarEmpleado(empleado);
            return res;
        }
        [HttpPost]
        [Route("DeleteEmpleado")]
        public async Task DeleteEmpleado(Personas empleado)
        {
                await _services.EliminarEmpleado(empleado);           
        }


    }
}
