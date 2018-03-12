using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace SimpleBot.Dados
{
    public class SqlServerRepo : IUserRepo
    {

        private string _connectionString;// = @"Data Source=L2901MICRO16;Initial Catalog=Teste;Integrated Security=True";

        public SqlServerRepo(string connectionString)
        {
            this._connectionString = connectionString;
        }
        //create table tb_teste(
        //id int identity primary key,
        //Usuario varchar,
        //Visitas int)

        public UserProfile BuscarPerfilId(string id)
        {
            UserProfile profile = null;
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
            if (profile == null)
                profile = new UserProfile() { Id = id, Visitas = 0 };
            return profile;
        }

        //Create table tb_historico(
        //Id int identity primary key,
        //Usuario varchar(100),
        //Mensagem varchar(max))
        public void SalvarHistorico(Message message)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    con.Open();
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@Usuario", message.User);
                    cmd.Parameters.AddWithValue("@Mensagem", message.Text);
                    cmd.CommandText = "INSERT INTO tb_historico(Usuario,Mensagem)VALUES(@Usuario,@Mensagem)";
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void SalvarPerfil(UserProfile profile)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    con.Open();
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@Usuario", profile.Id);
                    cmd.Parameters.AddWithValue("@Visitas", profile.Visitas);
                    if (profile.Visitas == 1) //Então significa que vai ser a primeira vez que vai salvar
                    {
                        cmd.CommandText = "INSERT INTO tb_teste(Usuario,Visitas)VALUES(@Usuario,@Visitas)";
                    }
                    else
                    {
                        cmd.CommandText = "UPDATE tb_teste set Visitas=@Visitas WHERE Usuario=@Usuario";
                    }
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}