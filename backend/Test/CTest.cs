using P4PModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Test
{
    class CTest
    {
        public void CreateUser()
        {
            CUser user = new CUser();
            user.AMaterno = "Amaterno";
            user.APaterno = "Apaterno";
            user.Avatar = "avatar";
            user.CV = "CV";
            user.Descripcion = "Descripcion";
            user.Mail = "mail";
            user.Nombre = "Nombre";
            user.Valido = true;
            user.Clave = "Clave";

            user.Create();

        }

        public void CreateOferta()
        {
            Random rnd = new Random();

            COferta oferta = new COferta();

            oferta.Aconvenir = true;
            oferta.Ciudad = rnd.Next(1, 100);
            oferta.Comuna = rnd.Next(1, 100);
            oferta.Dia = rnd.Next(1, 100);
            oferta.Direccion = "Direccion "+ rnd.Next(1, 100); ;
            oferta.Disponibilidad = rnd.Next(1, 100);
            oferta.Hora = rnd.Next(1, 100);
            oferta.IDProfesion = rnd.Next(1, 100);
            oferta.IDUsuario = 1;
            oferta.Lat = rnd.NextDouble()*3 ;
            oferta.Lng = rnd.NextDouble() * 3;
            oferta.Mes = rnd.Next(1, 100);
            oferta.Region = rnd.Next(1, 100);
            oferta.Remoto = true;
            oferta.Servicio = rnd.Next(1, 100);
            oferta.Create();





        }

        public void CUser_Load()
        {

            CUser user = CUser.Load(0, "mail", "clave", "");

            Console.WriteLine(user.ToString());
        }

        public void OfertaSearch()
        {
            COfertaSearch os = new COfertaSearch();
            os.IDProfesion = 10;
             List<COfertaResult> lr =  COferta.getOfertas(os);

            foreach(COfertaResult r in lr)
            {
                Console.WriteLine(r.Oferta);
            
            }
        }
    }
}
