using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using PawCare.Models;
using System.Data; 

namespace PawCare.Controllers
{
    public class MascotasController : Controller
    {
        private readonly IConfiguration _configuration;

        public MascotasController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // LISTAR
        public IActionResult Index()
        {
            var mascotas = new List<Mascota>();

            string conexion = _configuration.GetConnectionString("ConexionPawCare");

            using (SqlConnection connection = new SqlConnection(conexion))
            {
                using (SqlCommand command = new SqlCommand("spListarMascotas", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            mascotas.Add(new Mascota
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                NombreMascota = reader["NombreMascota"].ToString(),
                                NombreDueno = reader["NombreDueno"].ToString(),
                                Tipo = reader["Tipo"].ToString(),
                                Edad = Convert.ToInt32(reader["Edad"]),
                                Telefono = reader["Telefono"].ToString(),
                                Observaciones = reader["Observaciones"] == DBNull.Value
                                    ? null
                                    : reader["Observaciones"].ToString()
                            });
                        }
                    }
                }
            }

            return View(mascotas);
        }

        // INSERTAR
        [HttpPost]
        public IActionResult Index(Mascota mascota)
        {
            if (!ModelState.IsValid)
            {
                return View(mascota);
            }

            string conexion = _configuration.GetConnectionString("ConexionPawCare");

            using (SqlConnection connection = new SqlConnection(conexion))
            {
                using (SqlCommand command = new SqlCommand("spInsertarMascota", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter("@NombreMascota", mascota.NombreMascota));
                    command.Parameters.Add(new SqlParameter("@NombreDueno", mascota.NombreDueno));
                    command.Parameters.Add(new SqlParameter("@Tipo", mascota.Tipo));
                    command.Parameters.Add(new SqlParameter("@Edad", mascota.Edad));
                    command.Parameters.Add(new SqlParameter("@Telefono", mascota.Telefono));
                    command.Parameters.Add(new SqlParameter(
                        "@Observaciones",
                        (object?)mascota.Observaciones ?? DBNull.Value
                    ));

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }

            return RedirectToAction("Index");
        }
    }
}