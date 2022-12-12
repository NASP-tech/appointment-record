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
    public class PacienteDAL
    {
        string conString = ConfigurationManager.ConnectionStrings["adoConnectionstring"].ToString();

        //Get All Pacientes
        public List<Paciente> GetAllPacientes()
        {
            List<Paciente> pacienteList = new List<Paciente>();

            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_GetAllPacientes";
                SqlDataAdapter sqlDA = new SqlDataAdapter(command);
                DataTable dtPacientes = new DataTable();

                connection.Open();
                sqlDA.Fill(dtPacientes);
                connection.Close();

                foreach ( DataRow dr in dtPacientes.Rows)
                {
                    pacienteList.Add(new Paciente
                    {
                        idPaciente = Convert.ToInt32(dr["idPaciente"]),
                        Nombre = dr["Nombre"].ToString(),
                        Direccion = dr["Direccion"].ToString(),
                        Telefono = dr["Telefono"].ToString(),
                        EMail = dr["EMail"].ToString()
                    });
                }
            }

            return pacienteList;
        }

        //Insert Paciente

        public bool InsertPaciente(Paciente paciente)
        {
            int id = 0;
            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = new SqlCommand("sp_InsertPacientes", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Nombre", paciente.Nombre);
                command.Parameters.AddWithValue("@Direccion", paciente.Direccion);
                command.Parameters.AddWithValue("@Telefono", paciente.Telefono);
                command.Parameters.AddWithValue("@EMail", paciente.EMail);

                connection.Open();
                id = command.ExecuteNonQuery();
                connection.Close();
            }

            if(id > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //Get Pacientes by Paciente ID
        public List<Paciente> GetPacientesByID(int idPaciente)
        {
            List<Paciente> pacienteList = new List<Paciente>();

            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_GetPacienteByID";
                command.Parameters.AddWithValue("@idPaciente", idPaciente);
                SqlDataAdapter sqlDA = new SqlDataAdapter(command);
                DataTable dtPacientes = new DataTable();

                connection.Open();
                sqlDA.Fill(dtPacientes);
                connection.Close();

                foreach (DataRow dr in dtPacientes.Rows)
                {
                    pacienteList.Add(new Paciente
                    {
                        idPaciente = Convert.ToInt32(dr["idPaciente"]),
                        Nombre = dr["Nombre"].ToString(),
                        Direccion = dr["Direccion"].ToString(),
                        Telefono = dr["Telefono"].ToString(),
                        EMail = dr["EMail"].ToString()
                    });
                }
            }

            return pacienteList;
        }


        //Insert Paciente

        public bool UpdatePaciente(Paciente paciente)
        {
            int i = 0;
            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = new SqlCommand("sp_UpdatePacientes", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@idPaciente", paciente.idPaciente);
                command.Parameters.AddWithValue("@Nombre", paciente.Nombre);
                command.Parameters.AddWithValue("@Direccion", paciente.Direccion);
                command.Parameters.AddWithValue("@Telefono", paciente.Telefono);
                command.Parameters.AddWithValue("@EMail", paciente.EMail);

                connection.Open();
                i = command.ExecuteNonQuery();
                connection.Close();
            }

            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //Delete Pacient
        public string DeletePaciente(int idPaciente)
        {
            string result = "";

            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = new SqlCommand("sp_deletepaciente", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@idPaciente", idPaciente);
                command.Parameters.Add("@OutputMessage", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;

                connection.Open();
                command.ExecuteNonQuery();
                result = command.Parameters["@OutputMessage"].Value.ToString();
                connection.Close();
            }

            return result;
        }
    }
}