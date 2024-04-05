using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfCRUD.Models
{
    public class UserModel
    {
        [Column("ID")]
        private int _id;
        [Column("nombre")]
        private string _nombre;
        [Column("apellidos")]
        private string _apellidos;
        [Column("email")]
        private string _email;
        [Column("contrasena")]
        private string _password;

        public int Id
        {
            get => _id;
            set
            {
                if (_id != value)
                {
                    _id = value;
                }
            }
        }

        public string Nombre
        {

            get => _nombre;
            set
            {
                if (_nombre != value)
                {

                    _nombre = value;
                }
            }
        }
        public string Apellidos
        {

            get => _apellidos;
            set
            {
                if (_apellidos != value)
                {

                    _apellidos = value;
                }
            }
        }
        public string Email
        {

            get => _email;
            set
            {
                if (_email != value)
                {
                    _email = value;
                }
            }
        }
        public string Password
        {

            get => _password;
            set
            {
                if (_password != value)
                {
                    _password = value;
                }
            }
        }
    }
        
}
