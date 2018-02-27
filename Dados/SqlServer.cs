using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace SimpleBot.Dados
{
    public class SqlServer : IDados
    {
        private SqlConnection con;
        private SqlCommand cmd;
        private SqlDataReader reader;
        private static string _connectionString = @"Data Source=L2901MICRO16;Initial Catalog=Teste;Integrated Security=True";

        //create table tb_teste(
        //id identity primary key,
        //Usuario varchar,
        //Visitas int)
        
        public UserProfile BuscarPerfilId(string id)
        {
            try
            {                
                using (con = new SqlConnection(_connectionString))
                {
                    using (cmd = new SqlCommand())
                    {
                        con.Open();
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "select Id,Usuario,Visitas from tb_teste where Usuario=@Usuario";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@Usuario", id);
                        reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            return new UserProfile()
                            {
                                Id = reader["Usuario"].ToString(),
                                Visitas = (int)reader["Visitas"]
                            };
                        }
                    }
                }
                return default(UserProfile);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void Inserir(Message message)
        {
            throw new NotImplementedException();
        }

        public void InserirPerfil(string id, UserProfile profile)
        {

            try
            {
                using (con = new SqlConnection(_connectionString))
                {
                    using (cmd = new SqlCommand())
                    {
                        con.Open();
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "INSERT INTO tb_teste(Usuario,Visitas)VALUES(@id,@Visitas)";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.Parameters.AddWithValue("@Visitas", profile.Visitas);
                        cmd.ExecuteNonQuery();                        
                    }
                }                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AtualizarPerfil(UserProfile profile)
        {
            try
            {
                using (con = new SqlConnection(_connectionString))
                {
                    using (cmd = new SqlCommand())
                    {
                        con.Open();
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "UPDATE tb_teste set Visitas=@Visitas WHERE Usuario=@Usuario";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@Usuario",profile.Id);
                        cmd.Parameters.AddWithValue("@Visitas", profile.Visitas);
                        reader = cmd.ExecuteReader();
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}