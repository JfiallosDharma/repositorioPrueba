using APInet8.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace APInet8.Services
{
    public class PersonaServices
    {
        //aqui van los metodos, la logica de programacion

        private readonly IConfiguration _connectionString;
        private readonly string cadena;

        //metodo para obtener la conexion a la base de datos
        public PersonaServices(IConfiguration configuration)
        {
            _connectionString = configuration;
            cadena = _connectionString.GetConnectionString("DefaultConnection");//llama a la cadena de conexion de appsettings.json
        }

        //metodo para obtener todos los empleados
        public async Task<IEnumerable<Personas>> GetEmpleados()
        {
            IEnumerable<Personas> empleados;
            var query = "SP_listaEmpleado";//el nombre del sp
            using (IDbConnection db = new SqlConnection(cadena))
            {
                empleados = await db.QueryAsync<Personas>(query, commandType: CommandType.StoredProcedure);//consulto de manera asincrona
            }
            return empleados;
        }

        //metodo para obtener un empleado por id
        public async Task<IEnumerable<Personas>> GetEmpleadoEspecifico(int IDEmpleado)
        {
            IEnumerable<Personas> empleados;
            var query = "SP_EmpleadoEspecifico";//el nombre del sp
            using (IDbConnection db = new SqlConnection(cadena))
            {
                var id = new { ID = IDEmpleado };
                //aqui envio el parametro al sp
                empleados = await db.QueryAsync<Personas>(query, id, commandType: CommandType.StoredProcedure);//consulto de manera asincrona
            }
            return empleados;
        }
        //metodo para insertar un empleado
        public async Task<bool> InsertEmpleado(Personas empleado)
        {
            var query = "SP_crearEmleado";//el nombre del sp
            using (IDbConnection db = new SqlConnection(cadena))
            {
                //aqui creo los parametros que se enviaran al sp en los campos de la bd
                var parameters = new DynamicParameters();
                parameters.Add("@Nombre", empleado.Nombre);
                parameters.Add("@Apellido", empleado.Apellido);//campos de la bd  parameters.Add("@valorenviado", empleado.campoBD);
                parameters.Add("@Edad", empleado.Edad);
                parameters.Add("@Correo", empleado.Correo);
                parameters.Add("@Estado", empleado.Estado);

                //aqui envio los parametros al sp
                await db.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);//consulto de manera asincrona
            }
            return true;
        }
        //metodo para actualizar un empleado
        public async Task<bool> ActualizarEmpleado(Personas empleado)
        {
            var query = "SP_ActualizarEmpleado";//el nombre del sp
            using (IDbConnection db = new SqlConnection(cadena))
            {
                //aqui creo los parametros que se enviaran al sp en los campos de la bd
                var parameters = new DynamicParameters();
                parameters.Add("@ID", empleado.ID);//en base a este campo se actualizara el registro
                parameters.Add("@Nombre", empleado.Nombre);
                parameters.Add("@Apellido", empleado.Apellido);//campos de la bd  parameters.Add("@valorenviado", empleado.campoBD);
                parameters.Add("@Edad", empleado.Edad);
                parameters.Add("@Correo", empleado.Correo);
                parameters.Add("@Estado", empleado.Estado);
                //aqui envio los parametros al sp
                await db.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);//consulto de manera asincrona
            }
            return true;
        }
        //borrado logico
        public async Task EliminarEmpleado(Personas empleado)
        {
            var query = "SP_BorradoLogicoEmpleado";//el nombre del sp
            using (IDbConnection db = new SqlConnection(cadena))
            {
                //aqui creo los parametros que se enviaran al sp en los campos de la bd
                var parameters = new DynamicParameters();
                parameters.Add("@ID", empleado.ID);//en base a este campo se actualizara el registro
                parameters.Add("@Estado", empleado.Estado);
                //aqui envio los parametros al sp
                await db.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);//consulto de manera asincrona
            }
        }
    }
}
