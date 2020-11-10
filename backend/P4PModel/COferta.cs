using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace P4PModel
{

    public class COfertaResult
    { 
       public COferta Oferta { get; set; }
        public CUser User { get; set; }

        public COfertaResult()
        {
            Oferta = new COferta();
            User = new CUser();
        }

    }
    public class COfertaSearch : COferta 
    { 
    
    }

    public class COferta
    {
        public int IDOferta { get; set; }
        public DateTime CreaterDate { get; set; }
        public int IDUsuario { get; set; }
        public int IDProfesion { get; set; }
        public int Disponibilidad { get; set; }
        public bool Remoto { get; set; }
        public bool Aconvenir { get; set; }
        public int Hora { get; set; }
        public int Dia { get; set; }
        public int Mes { get; set; }
        public int Servicio { get; set; }
        public int Region { get; set; }
        public int Ciudad { get; set; }
        public int Comuna { get; set; }
        public string Direccion { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }

        public int Create()

        {
            int IdOferta = 0;

            SqlConnection cnn = CDB.GetConnection();

            try
            {

                SqlCommand cmd = new SqlCommand("sp_oferta_ins");
                cmd.Connection = cnn;

                cmd.CommandType = System.Data.CommandType.StoredProcedure;



                cmd.Parameters.AddWithValue("@id_usuario", IDUsuario);
                cmd.Parameters.AddWithValue("@id_profesion", IDProfesion);
                cmd.Parameters.AddWithValue("@disponibilidad", Disponibilidad);
                cmd.Parameters.AddWithValue("@remoto", Remoto);
                cmd.Parameters.AddWithValue("@aconvenir", Aconvenir);
                cmd.Parameters.AddWithValue("@hora", Hora);
                cmd.Parameters.AddWithValue("@dia", Dia);
                cmd.Parameters.AddWithValue("@mes", Mes);
                cmd.Parameters.AddWithValue("@servicio", Servicio);
                cmd.Parameters.AddWithValue("@region", Region);
                cmd.Parameters.AddWithValue("@ciudad", Ciudad);
                cmd.Parameters.AddWithValue("@comuna", Comuna);
                cmd.Parameters.AddWithValue("@direccion", Direccion);
                cmd.Parameters.AddWithValue("@lat", Lat);
                cmd.Parameters.AddWithValue("@lng", Lng);

                SqlParameter p = new SqlParameter("@idoferta", System.Data.SqlDbType.Int);
                p.Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters.Add(p);

                cmd.ExecuteNonQuery();
                IdOferta = (int)p.Value;

            }
            catch (Exception ex)
            {
                Console.WriteLine("addcarDay exception:" + ex.ToString());

            }
            finally
            {
                if (cnn != null)
                {
                    cnn.Close();
                }
            }

            return IdOferta;
        }

        public static  List<COfertaResult> getOfertas(COfertaSearch par)
        {
            List<COfertaResult> ls = new List<COfertaResult>();
            SqlDataReader dr = null;
            SqlConnection cnn = CDB.GetConnection();

            try
            {

                SqlCommand cmd = new SqlCommand("sp_oferta_search");
                cmd.Connection = cnn;

                cmd.CommandType = System.Data.CommandType.StoredProcedure;



                cmd.Parameters.AddWithValue("@id_usuario", par.IDUsuario);
                cmd.Parameters.AddWithValue("@id_profesion", par.IDProfesion);
                cmd.Parameters.AddWithValue("@disponibilidad", par.Disponibilidad);
                cmd.Parameters.AddWithValue("@remoto", par.Remoto);
                cmd.Parameters.AddWithValue("@aconvenir", par.Aconvenir);
                cmd.Parameters.AddWithValue("@hora", par.Hora);
                cmd.Parameters.AddWithValue("@dia", par.Dia);
                cmd.Parameters.AddWithValue("@mes", par.Mes);
                cmd.Parameters.AddWithValue("@servicio", par.Servicio);
                cmd.Parameters.AddWithValue("@region", par.Region);
                cmd.Parameters.AddWithValue("@ciudad", par.Ciudad);
                cmd.Parameters.AddWithValue("@comuna", par.Comuna);
                cmd.Parameters.AddWithValue("@direccion", par.Direccion);
                cmd.Parameters.AddWithValue("@lat", par.Lat);
                cmd.Parameters.AddWithValue("@lng", par.Lng);

              
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {

                    COfertaResult r = new COfertaResult();
                    

                   r.Oferta.Aconvenir = bool.Parse(dr["aconvenir"].ToString());
                   r.Oferta.Ciudad = int.Parse(dr["ciudad"].ToString());
                   r.Oferta.Comuna = int.Parse(dr["comuna"].ToString());
                   r.Oferta.CreaterDate = DateTime.Parse(dr["create_date"].ToString());
                   r.Oferta.Dia = int.Parse(dr["dia"].ToString());
                   r.Oferta.Direccion = dr["direccion"].ToString();
                   r.Oferta.Disponibilidad = int.Parse(dr["disponibilidad"].ToString());
                   r.Oferta.Hora = int.Parse(dr["hora"].ToString());
                   r.Oferta.IDOferta = int.Parse(dr["id_oferta"].ToString());
                   r.Oferta.IDProfesion = int.Parse(dr["id_profesion"].ToString());
                   r.Oferta.IDUsuario = int.Parse(dr["id_usuario"].ToString());
                   r.Oferta.Lat = double.Parse(dr["lat"].ToString());
                   r.Oferta.Lng = double.Parse(dr["lng"].ToString());
                   r.Oferta.Mes = int.Parse(dr["mes"].ToString());
                   r.Oferta.Region = int.Parse(dr["region"].ToString());
                   r.Oferta.Remoto = bool.Parse(dr["remoto"].ToString());
                   r.Oferta.Servicio = int.Parse(dr["servicio"].ToString());

                    r.User.Id = int.Parse(dr["id_usuario"].ToString());
                    r.User.Nombre = dr["nombre"].ToString();
                    r.User.Mail = dr["mail"].ToString();
                    r.User.Rating = int.Parse(dr["rating"].ToString());
                    r.User.Avatar = dr["link_foto"].ToString();

                    ls.Add(r);




                }
                dr.Close();
               

            }
            catch (Exception ex)
            {
                Console.WriteLine("addcarDay exception:" + ex.ToString());

            }
            finally
            {
                if (cnn != null)
                {
                    cnn.Close();
                }
            }


            return ls;

        }
    }
}
