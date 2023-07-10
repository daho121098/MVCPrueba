using ServiciosRestPrueba.Models;
using ServiciosRestPrueba.App_Code;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace ServiciosRestPrueba.Controllers
{
    public class InscripcionesController : ApiController
    {
        // GET: api/Inscripciones
        [HttpGet]
        [Route("api/Catalogos")]
        public Catalogos Catalogos()
        {
            Constantes cadena = new Constantes();
            string cadenaConexion = cadena.cadenaConexion;
            Catalogos catalogos = new Catalogos();

            Genero objGenero = new Genero();
            List<Genero> listaGenero = new List<Genero>();

            try
            {
                Conexion conexion = new Conexion();


                SqlConnection con = new SqlConnection(cadenaConexion);
                using (SqlCommand cm = new SqlCommand("SP_ObtenerGenero", con))
                {
                    cm.CommandType = CommandType.StoredProcedure;

                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cm);
                    dt = conexion.Procedimiento("SP_ObtenerGenero", cm);
                    if (dt.Rows.Count == 0)
                    {
                        objGenero = null;
                    }
                    else
                    {
                        int contador = 0;
                        foreach (DataRow row in dt.Rows)
                        {
                            objGenero = new Genero();
                            objGenero.idGenero = row["IdGenero"].ToString();
                                objGenero.Descripcion = row["Descripcion"].ToString();
                                listaGenero.Add(objGenero);
                        }
                    }

                }

                catalogos.listaGenero = listaGenero;

                GeneroPoesia objGeneroPoesia = new GeneroPoesia();
                List<GeneroPoesia> listaGeneroPoesia = new List<GeneroPoesia>();
                con = new SqlConnection(cadenaConexion);
                using (SqlCommand cm = new SqlCommand("SP_ObtenerGeneroPoesia", con))
                {
                    cm.CommandType = CommandType.StoredProcedure;

                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cm);
                    dt = conexion.Procedimiento("SP_ObtenerGeneroPoesia", cm);
                    if (dt.Rows.Count == 0)
                    {
                        objGeneroPoesia = null;
                    }
                    else
                    {
                        int contador = 0;
                        foreach (DataRow row in dt.Rows)
                        {
                            objGeneroPoesia = new GeneroPoesia();
                            objGeneroPoesia.idGenero = Convert.ToInt32(row["IdGenero"].ToString());
                            objGeneroPoesia.Descripcion = row["Descripcion"].ToString();
                            listaGeneroPoesia.Add(objGeneroPoesia);
                        }
                    }

                }

                catalogos.listaGeneroPoesia = listaGeneroPoesia;

                Carrera objCarrera = new Carrera();
                List<Carrera> listaCarrera = new List<Carrera>();
                con = new SqlConnection(cadenaConexion);
                using (SqlCommand cm = new SqlCommand("SP_ObtenerCarrera", con))
                {
                    cm.CommandType = CommandType.StoredProcedure;

                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cm);
                    dt = conexion.Procedimiento("SP_ObtenerCarrera", cm);
                    if (dt.Rows.Count == 0)
                    {
                        objCarrera = null;
                    }
                    else
                    {
                        int contador = 0;
                        foreach (DataRow row in dt.Rows)
                        {
                            objCarrera = new Carrera();
                            objCarrera.idCarrera = Convert.ToInt32(row["IdCarrera"].ToString());
                            objCarrera.nombre = row["NombreCarrera"].ToString();
                            listaCarrera.Add(objCarrera);
                        }
                    }

                }

                catalogos.listaCarrera = listaCarrera;

            }
            catch (Exception e)
            {

            }

            return catalogos;
        }

        [HttpPost]
        [Route("api/Inscribir")]
        public bool Inscribir(Inscripcion inscripcion)
        {
            Constantes cadena = new Constantes();
            string cadenaConexion = cadena.cadenaConexion;
            bool respuesta = false;
            try
            {
                Conexion conexion = new Conexion();

                SqlConnection con = new SqlConnection(cadenaConexion);
                using (SqlCommand cm = new SqlCommand("SP_Inscribir", con))
                {
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.Parameters.Add(new SqlParameter("@Carnet", inscripcion.carnet));
                    cm.Parameters.Add(new SqlParameter("@Nombre", inscripcion.nombre));
                    cm.Parameters.Add(new SqlParameter("@IdGenero", inscripcion.idGenero));
                    cm.Parameters.Add(new SqlParameter("@Telefono", inscripcion.telefono));
                    cm.Parameters.Add(new SqlParameter("@FechaNacimiento", inscripcion.fechaNacimiento));
                    cm.Parameters.Add(new SqlParameter("@IdCarrera", inscripcion.idCarrera));
                    cm.Parameters.Add(new SqlParameter("@IdGeneroPoesia", inscripcion.idGeneroPoesia));



                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cm);
                    dt = conexion.Procedimiento("SP_Inscribir", cm);
                    if (dt.Rows.Count == 0)
                    {
                        respuesta = false;
                    }
                    else
                    {
                        int resultado = 0;
                        foreach (DataRow row in dt.Rows)
                        {
                            
                            resultado = Convert.ToInt32(row["Respuesta"].ToString());
                            
                        }

                        respuesta = (resultado == 1)?false:true;
                    }

                }
            }
            catch (Exception e) {
                respuesta = false;
            }


            return respuesta;
        }

        [HttpGet]
        [Route("api/Reporte")]
        public List<Inscripcion> Reporte()
        {
            Constantes cadena = new Constantes();
            string cadenaConexion = cadena.cadenaConexion;
            Inscripcion objInscricpcion = new Inscripcion();
            List<Inscripcion> listaInscripcion = new List<Inscripcion>();
            try
            {
                Conexion conexion = new Conexion();

                SqlConnection con = new SqlConnection(cadenaConexion);
                using (SqlCommand cm = new SqlCommand("SP_ConsultaReporte", con))
                {
                    cm.CommandType = CommandType.StoredProcedure;

                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cm);
                    dt = conexion.Procedimiento("SP_ConsultaReporte", cm);
                    if (dt.Rows.Count == 0)
                    {
                        objInscricpcion = null;
                    }
                    else
                    {

                        foreach (DataRow row in dt.Rows)
                        {
                            objInscricpcion = new Inscripcion();
                            objInscricpcion.carnet = row["Carnet"].ToString();
                            objInscricpcion.nombre = row["Nombre"].ToString();
                            objInscricpcion.idGenero = row["IdGenero"].ToString();
                            objInscricpcion.telefono = row["Telefono"].ToString();
                            objInscricpcion.fechaNacimiento = Convert.ToDateTime(row["FechaNacimiento"].ToString());
                            objInscricpcion.idCarrera = Convert.ToInt32(row["IdCarrera"].ToString());
                            objInscricpcion.idGeneroPoesia = Convert.ToInt32(row["IdGeneroPoesia"].ToString());
                            objInscricpcion.fechaInscripcion = Convert.ToDateTime(row["FechaInscripción"].ToString());
                            listaInscripcion.Add(objInscricpcion);
                        }
                    }

                }
            }
            catch (Exception e)
            {

            }


            return listaInscripcion;
        }
    }
}
