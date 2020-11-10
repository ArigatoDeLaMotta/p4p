using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace P4PModel
{
    class CHelper
    {


        static List<KeyValuePair<string, string>> getRegiones()
        {
            List<KeyValuePair<string, string>> lkv = new List<KeyValuePair<string, string>>();
            try
            {
                SqlCommand cmd = new SqlCommand("select * from tbl_region");
                cmd.Connection = CDB.GetConnection();
                cmd.CommandType = System.Data.CommandType.Text;

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    string key = dr["id_region"].ToString();

                    string value = dr["nombre_region"].ToString();
                    lkv.Add(new KeyValuePair<string, string>(key, value));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR INSIDE checkSessionKey:" + ex.ToString());
            }

            return lkv;

        }

        static List<KeyValuePair<string, string>> getProvincias(int idr)
        {
            List<KeyValuePair<string, string>> lkv = new List<KeyValuePair<string, string>>();
            try
            {
                SqlCommand cmd = new SqlCommand("select * from tbl_provincia where id_region=@idr");
                cmd.Connection = CDB.GetConnection();
                cmd.CommandType = System.Data.CommandType.Text;

                cmd.Parameters.AddWithValue("@idr", idr);

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    string key = dr["id_provincia"].ToString();

                    string value = dr["nombre_provincia"].ToString();
                    lkv.Add(new KeyValuePair<string, string>(key, value));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR INSIDE checkSessionKey:" + ex.ToString());
            }

            return lkv;

        }

        static List<KeyValuePair<string, string>> getComunas(int idp)
        {
            List<KeyValuePair<string, string>> lkv = new List<KeyValuePair<string, string>>();
            try
            {
                SqlCommand cmd = new SqlCommand("select * from tbl_comuna where id_provincia =@idp ");
                cmd.Connection = CDB.GetConnection();
                cmd.CommandType = System.Data.CommandType.Text;

                cmd.Parameters.AddWithValue("@idp", idp);

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    string key = dr["id_comuna"].ToString();

                    string value = dr["nombre_comuna"].ToString();
                    lkv.Add(new KeyValuePair<string, string>(key, value));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR INSIDE checkSessionKey:" + ex.ToString());
            }

            return lkv;

        }
    }
}
