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

        private string _connectionString;// = @"Data Source=L2901MICRO16;Initial Catalog=Teste;Integrated Security=True";

        public SqlServer(string connectionString)
        {
            this._connectionString = connectionString;
        }
        //create table tb_teste(
        //id identity primary key,
        //Usuario varchar,
        //Visitas int)
        
        public UserProfile BuscarPerfilId(string id)
        {                           
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        con.Open();
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "select Id,Usuario,Visitas from tb_teste where Usuario=@Usuario";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@Usuario", id);
                       SqlDataReader reader = cmd.ExecuteReader();
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

        public void Inserir(Message message)
        {
            throw new NotImplementedException();
        }

        public void InserirPerfil(string id, UserProfile profile)
        {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand())
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

        public void AtualizarPerfil(UserProfile profile)
        {            
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        con.Open();
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "UPDATE tb_teste set Visitas=@Visitas WHERE Usuario=@Usuario";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@Usuario",profile.Id);
                        cmd.Parameters.AddWithValue("@Visitas", profile.Visitas);
                        SqlDataReader reader = cmd.ExecuteReader();
                    }
                }           
           
        }
    }
}