using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;


    class CDB
    {
    public static SqlConnection GetConnection()
    {
        SqlConnection cnn = new SqlConnection();
        string cnnstring = "Server=3.223.180.135;Database=p4p;User Id=p4papp;Password =p4p100; ";
        cnn.ConnectionString = cnnstring;
        cnn.Open();

        return cnn;
    }
}

