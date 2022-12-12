using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using medicalAppointments.Models;

namespace medicalAppointments.DAL
{
    public class CitasDAL
    {
        string conString = ConfigurationManager.ConnectionStrings["adoConnectionstring"].ToString();

        //Get all doctors
        public List<Citas> GetAllCitas()
        {
            List<Citas> citasList = new List<Citas>();

            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_GetAllCitas";
                SqlDataAdapter sqlA = new SqlDataAdapter(command);
                DataTable dtCitas = new DataTable();

                connection.Open();
                sqlA.Fill(dtCitas);
                connection.Close();

                foreach (DataRow dr in dtCitas.Rows)
                {
                    citasList.Add(new Citas
                    {
                        idCita = Convert.ToInt32(dr["idCita"]),
                        idPaciente = Convert.ToInt32(dr["idPaciente"]),
                        idDoctor = Convert.ToInt32(dr["idDoctor"]),
                        dia = dr["dia"].ToString(),
                        hora = dr["hora"].ToString(),
                        diagnostico = dr["diagnostico"].ToString(),
                        comentarios = dr["comentarios"].ToString()
                    });
                }
            }
            return citasList;
        }
    }
}