using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicoRest_AplicandoSerti_BT.Models
{
    public class LocalizacionesModel
    {
        public int Id { get; set; }
        public int Estante { get; set; }
        public string Sala { get; set; }
        public int Librero { get; set; }
        public string Posicion { get; set; }
    }
}