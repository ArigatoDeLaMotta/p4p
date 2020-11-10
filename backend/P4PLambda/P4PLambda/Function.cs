using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Amazon.Lambda.Serialization.Json;
using Amazon.Lambda.Core;
using P4PModel;
// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace P4PLambda
{
    public class Function
    {

        /// <summary>
        /// A simple function that takes a string and does a ToUpper
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        [LambdaSerializer(typeof(JsonSerializer))]
        public JObject FunctionHandler(JObject input, ILambdaContext context)
        {
            //input.
            Console.WriteLine("JObject:" + Regex.Replace(input.ToString(), @"\t|\n|\r", ""));

            CResponse response = new CResponse();
            string action = input["action"].ToString();
            LambdaLogger.Log("action: " + action);
            System.Console.Out.WriteLine("Starting calculation");
            try
            {
                switch (action)
                {
                    case "getProfesiones":

                        // Retorna Listado De objetos
                        response.Exito = true;
                        response.Response["getProfesiones"] = JToken.FromObject(CProfesion.List());

                        break;

                    case "getUser":
                        {
                            CUser user = new CUser();


                            string key = input["key"].ToString();
                            string mail = input["mail"].ToString();
                            string clave = input["clave"].ToString();
                            int id = int.Parse(input["id"].ToString());
                            Console.WriteLine("Inside getUser ");

                            user = CUser.Load(id, mail, clave, key);
                            // Retorna Un Objeto
                            response.Exito = true;
                            response.Response = JObject.FromObject(user);

                        }

                        break;
                    case "getSearch":

                        COfertaSearch of = new COfertaSearch();

                        of.IDProfesion = int.Parse(input["IDProfesion"].ToString());

                        List<COfertaResult> lr = COferta.getOfertas(of);

                        response.Exito = true;
                        response.Response["getSearch"] = JToken.FromObject(lr);

                        break;

                    case "editarPerfil":
                        {
                            CUser user = new CUser();
                            bool bolExito = false;

                            string mail = input["mail"].ToString();
                            string clave = input["clave"].ToString();
                            int id = int.Parse(input["id"].ToString());
                            string nombre = input["nombre"].ToString();
                            string apaterno = input["apaterno"].ToString();
                            string amaterno = input["amaterno"].ToString();
                            int rating = int.Parse(input["rating"].ToString());
                            string link_foto = input["link_foto"].ToString();
                            string link_cv = input["link_cv"].ToString();
                            string descripcion = input["descripcion"].ToString();

                            Console.WriteLine("Inside getUser ");

                            user.Mail = mail;
                            user.Clave = clave;
                            user.Id = id;
                            user.Nombre = nombre;
                            user.APaterno = apaterno;
                            user.AMaterno = amaterno;
                            user.Rating = rating;
                            user.Avatar = link_foto;
                            user.CV = link_cv;
                            user.Descripcion = descripcion;

                            bolExito = user.Update();

                            // Retorna Un Objeto
                            response.Exito = bolExito;


                        }
                        break;
                    case "insProfesion":
                        {
                            int idP = 0;
                            CProfesion prof = new CProfesion();
                            string nombre = input["nombre"].ToString();
                            prof.Nombre = nombre;
                            idP = prof.Create();
                            if (idP > 0)
                            {
                                response.Exito = true;
                            }

                        }
                        break;

                    default:
                        {
                            CUser user = new CUser();
                            user.Nombre = action;

                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                LambdaLogger.Log(ex.ToString());
                response.Exito = true;

            }

            Console.WriteLine("Response From Function:" + response.ToString());

            return JObject.FromObject(response);
        }
    }
}
