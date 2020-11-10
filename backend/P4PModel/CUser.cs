using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace P4PModel
{
    public class CUser
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string APaterno { get; set; }
        public string AMaterno { get; set; }
        public string Mail { get; set; }
        public string Avatar { get; set; }
        public string CV { get; set; }
        public string Descripcion { get; set; }

        public bool Valido { get; set; }

        public string Clave { get; set; }

        public string Key { get; set; }

        public int Rating { get; set; }

        static public CUser Load(int id, string mail, string clave, string key)

        {
            CUser user = new CUser();

            user.Valido = false;


            SqlConnection cnn = CDB.GetConnection();
            SqlDataReader dr = null;

            try
            {

                SqlCommand cmd = new SqlCommand("sp_user_get");
                cmd.Connection = cnn;

                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@mail", mail);
                cmd.Parameters.AddWithValue("@clave", clave);
                cmd.Parameters.AddWithValue("@key", key);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;


                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    int id_usuario = int.Parse(dr["id_usuario"].ToString());

                    string nombre = dr["nombre"].ToString();
                    string apaterno = dr["apaterno"].ToString();
                    string amaterno = dr["amaterno"].ToString();
                    mail = dr["mail"].ToString();
                    clave = dr["clave"].ToString();
                    string link_foto = dr["link_foto"].ToString();
                    string link_cv = dr["link_cv"].ToString();
                    string descripcion = dr["descripcion"].ToString();
                    string sessionkey = dr["sessionkey"].ToString();

                    int rating = int.Parse(dr["rating"].ToString());

                    user.Id = id_usuario;
                    user.Mail = mail;
                    user.AMaterno = amaterno;
                    user.APaterno = apaterno;
                    user.CV = link_cv;
                    user.Avatar = link_foto;
                    user.Descripcion = descripcion;
                    user.Key = sessionkey;
                    user.Valido = true;
                    user.Nombre = nombre;
                    user.Rating = rating;


                }

                dr.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("addcarDay exception:" + ex.ToString());

            }
            finally
            {
                if (dr != null && !dr.IsClosed)
                {
                    dr.Close();
                }
                if (cnn != null)
                {
                    cnn.Close();
                }
            }

            return user;
        }

        public int Create()
        {
            int IdUser = 0;

            SqlConnection cnn = CDB.GetConnection();

            try
            {

                SqlCommand cmd = new SqlCommand("sp_user_ins");
                cmd.Connection = cnn;

                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@nombre", Nombre);

                cmd.Parameters.AddWithValue("@apaterno", APaterno);
                cmd.Parameters.AddWithValue("@amaterno", AMaterno);
                cmd.Parameters.AddWithValue("@mail", Mail);
                cmd.Parameters.AddWithValue("@clave", Clave);


                SqlParameter p = new SqlParameter("@idu", System.Data.SqlDbType.Int);
                p.Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters.Add(p);

                cmd.ExecuteNonQuery();
                IdUser = (int)p.Value;

            }
            catch (Exception ex)
            {
                Console.WriteLine(" exception:" + ex.ToString());

            }
            finally
            {
                if (cnn != null)
                {
                    cnn.Close();
                }
            }


            return IdUser;


        }

        public bool Update()
        {
            bool bolExito = true;

            SqlConnection cnn = CDB.GetConnection();

            try
            {

                SqlCommand cmd = new SqlCommand("sp_user_upd");
                cmd.Connection = cnn;

                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@idu", Id);
                cmd.Parameters.AddWithValue("@nombre", Nombre);

                cmd.Parameters.AddWithValue("@apaterno", APaterno);
                cmd.Parameters.AddWithValue("@amaterno", AMaterno);
                cmd.Parameters.AddWithValue("@mail", Mail);
                cmd.Parameters.AddWithValue("@clave", Clave);
                cmd.Parameters.AddWithValue("@rating", Rating);
                cmd.Parameters.AddWithValue("@link_foto", Avatar);
                cmd.Parameters.AddWithValue("@link_cv", CV);
                cmd.Parameters.AddWithValue("@descripcion", Descripcion);

                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Console.WriteLine(" exception:" + ex.ToString());
                bolExito = false;

            }
            finally
            {
                if (cnn != null)
                {
                    cnn.Close();
                }
            }


            return bolExito;


        }

    }
}
