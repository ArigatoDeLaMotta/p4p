using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Dynamic;
using System.Text;

namespace P4PModel
{
   public class CProfesion
    {
        public int IDProfesion { get; set; }
        public string Nombre { get; set; }

        public static List<CProfesion> List()
        {
            List<CProfesion> lp = new List<CProfesion>();
            SqlConnection cnn = CDB.GetConnection();
            SqlDataReader dr = null;

            try
            {

                SqlCommand cmd = new SqlCommand("select  [id_p],[create_date],[nombre] ,[estado]  from tbl_profesion");
                cmd.Connection = cnn;

                //cmd.Parameters.AddWithValue("@id_car", idcar);

                cmd.CommandType = System.Data.CommandType.Text;


                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    int idp = int.Parse(dr["id_p"].ToString());
                    string nombre = dr["nombre"].ToString();


                    lp.Add(new CProfesion { IDProfesion = idp, Nombre = nombre });
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
            return lp;


        }

        public int Create()
        {
            int IdProfesion = 0;

            SqlConnection cnn = CDB.GetConnection();

            try
            {

                SqlCommand cmd = new SqlCommand("sp_profesion_ins");
                cmd.Connection = cnn;

                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@nombre", Nombre);
                cmd.Parameters.AddWithValue("@estado", DateTime.Now);
               


                SqlParameter p = new SqlParameter("@id_p", System.Data.SqlDbType.Int);
                p.Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters.Add(p);

                cmd.ExecuteNonQuery();
                IdProfesion = (int)p.Value;

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


            return IdProfesion;


        }

    }
}
