using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicoRest_AplicandoSerti_BT.Models.WS
{
    public class Response
    {
        public int resultado { get; set; }
        public object data { get; set; }
        public string mensaje { get; set; }
    }
}