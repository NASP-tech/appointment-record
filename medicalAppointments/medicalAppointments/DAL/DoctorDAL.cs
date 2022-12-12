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
    public class DoctorDAL
    {
        string conString = ConfigurationManager.ConnectionStrings["adoConnectionstring"].ToString();

        //Get all doctors
        public List<Doctores> GetAllDoctores()
        {
            List<Doctores> doctoresList = new List<Doctores>();

            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_GetAllDoctores";
                SqlDataAdapter sqlA = new SqlDataAdapter(command);
                DataTable dtDoctores = new DataTable();

                connection.Open();
                sqlA.Fill(dtDoctores);
                connection.Close();

                foreach (DataRow dr in dtDoctores.Rows)
                {
                    doctoresList.Add(new Doctores
                    {
                        idDoctor = Convert.ToInt32(dr["idDoctor"]),
                        Nombre = dr["Nombre"].ToString(),
                        Direccion = dr["Direccion"].ToString(),
                        Telefono = dr["Telefono"].ToString(),
                        EMail = dr["EMail"].ToString(),
                        Especialidad = dr["Especialidad"].ToString()
                    });
                }
            }
            return doctoresList;
        }

        //Insert Doctores

        public bool InsertDoctor(Doctores doctores)
        {
            int id = 0;
            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = new SqlCommand("sp_InsertDoctores", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Nombre", doctores.Nombre);
                command.Parameters.AddWithValue("@Direccion", doctores.Direccion);
                command.Parameters.AddWithValue("@Telefono", doctores.Telefono);
                command.Parameters.AddWithValue("@EMail", doctores.EMail);
                command.Parameters.AddWithValue("@Especialidad", doctores.Especialidad);

                connection.Open();
                id = command.ExecuteNonQuery();
                connection.Close();
            }

            if (id > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}