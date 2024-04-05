using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WpfCRUD.Models;


namespace WpfCRUD.BD
{
    internal class Bd
    {
        private readonly string _conexion;

        public string Conexion => _conexion;

        public Bd()
        {
            string servidor = "localhost"; //Nombre o ip del servidor de MySQL
            string bd = "bd_users"; //Nombre de la base de datos
            string usuario = "root"; //Usuario de acceso a MySQL
            string password = ""; //Contraseña de usuario de acceso a MySQL

            //Crearemos la cadena de conexión concatenando las variables
           _conexion = "Database=" + bd + "; Data Source=" + servidor + "; User Id=" + usuario + "; Password=" + password + "";
            
        }

        internal ObservableCollection<UserModel> Get()
        {
            ObservableCollection<UserModel> lstResult = new ObservableCollection<UserModel>();
            string query = "Select ID, nombre, apellidos, email, contrasena From usuarios";
            using (MySqlConnection cn = new MySqlConnection(Conexion) )
            {
                cn.Open();
                MySqlCommand cmd = new MySqlCommand(query, cn);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lstResult.Add(new UserModel()
                    {
                        Id = (int)Convert.ToInt64(reader["ID"]),
                        Nombre = (string)reader["nombre"],
                        Apellidos = (string)reader["apellidos"],
                        Email = (string)reader["email"],
                        Password = (string)reader["contrasena"]

                    });
                }
                reader.Close(); 
                cn.Close();
            }
            return lstResult;
        }

        internal void Add(UserModel user)
        {
            string query = "Insert into usuarios (nombre, apellidos, email, contrasena) values (@nombre, @apellidos, @email, @contrasena)";

            using (MySqlConnection cn =  new MySqlConnection (Conexion) )
            {
                cn.Open();
                MySqlCommand cmd = new MySqlCommand(query, cn);
                cmd.Parameters.AddWithValue("@nombre", user.Nombre);
                cmd.Parameters.AddWithValue("@apellidos", user.Apellidos);
                cmd.Parameters.AddWithValue("@email", user.Email);
                cmd.Parameters.AddWithValue("@contrasena", user.Password);
                cmd.ExecuteNonQuery();
                cn.Close();

            }
        }

        internal void Delete(UserModel user) {

            string query = "Delete From usuarios Where ID = @id";

            using (MySqlConnection cn = new MySqlConnection(Conexion))
            {
                cn.Open();
                MySqlCommand cmd = new MySqlCommand(query, cn);
                cmd.Parameters.AddWithValue("@id", user.Id);
                cmd.ExecuteNonQuery();
                cn.Close();

            }
        }

        internal void Edit(UserModel user)
        {
            string query = "Update usuarios set nombre=@nombre, apellidos=@apellidos, email=@email, contrasena=@contrasena where ID=@id";

            using (MySqlConnection cn = new MySqlConnection(Conexion))
            {
                cn.Open();
                MySqlCommand cmd = new MySqlCommand(query, cn);
                cmd.Parameters.AddWithValue("@id", user.Id);
                cmd.Parameters.AddWithValue("@nombre", user.Nombre);
                cmd.Parameters.AddWithValue("@apellidos", user.Apellidos);
                cmd.Parameters.AddWithValue("@email", user.Email);
                cmd.Parameters.AddWithValue("@contrasena", user.Password);
                cmd.ExecuteNonQuery();
                cn.Close();

            }
        }


        internal UserModel Login(UserModel user)
        {
            string query = "SELECT email, contrasena FROM usuarios WHERE email = @email";
            UserModel loggedInUser = null;

            using (MySqlConnection cn = new MySqlConnection(Conexion))
            {
                cn.Open();
                MySqlCommand cmd = new MySqlCommand(query, cn);
                cmd.Parameters.AddWithValue("@email", user.Email);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        // Crear un nuevo objeto UserModel con los datos obtenidos de la base de datos
                        loggedInUser = new UserModel
                        {
                            Email = reader["email"].ToString(),
                            Password = reader["contrasena"].ToString()
                        };
                    }
                }
            }

            return loggedInUser;
        }


    }
}
