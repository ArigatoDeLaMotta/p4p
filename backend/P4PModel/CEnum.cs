using System;
using System.Collections.Generic;
using System.Text;

namespace P4PModel
{

    public class CITem
    {
        public string Id { get; set; }
        public string Value { get; set; }
    }
    class CEnum
    {

        public List<CITem> getListCuando() {

            List<CITem> li = new List<CITem>();

            li.Add(new CITem { Id =     "1", Value = "Lo Antes Posible" });
            li.Add(new CITem { Id =    "10", Value = "De Lunes a Viernes" });
            li.Add(new CITem { Id =   "100", Value = "Sábados y Domingo" });
            li.Add(new CITem { Id =  "1000", Value = "FullTime" });
            li.Add(new CITem { Id = "10000", Value = "PartTIme" });
            return li;

        }
    }
}
