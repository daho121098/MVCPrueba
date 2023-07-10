using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ServiciosRestPrueba.App_Code
{
    public class Conexion
    {
        SqlConnection ConexionBDD;
        SqlCommand Comando;
        SqlDataReader dataResultado;

        public void ConexionOpen()
        {
            Constantes cadena = new Constantes();

            ConexionBDD = new SqlConnection(cadena.cadenaConexion);
            ConexionBDD.Open();
        }

        public void ConexionClose()
        {
            ConexionBDD.Close();
        }

        public DataTable Procedimiento(String nombreSP, SqlCommand cm)
        {
            DataTable comando = new DataTable();

            using (SqlCommand cmi = new SqlCommand(nombreSP, ConexionBDD))
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cm);
                comando = dt;
                ConexionOpen();
                da.Fill(dt);
                ConexionClose();
            }


            return comando;

        }
    }
}