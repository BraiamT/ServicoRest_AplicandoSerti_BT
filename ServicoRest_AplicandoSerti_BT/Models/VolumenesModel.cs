using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicoRest_AplicandoSerti_BT.Models
{
    public class VolumenesModel
    {
        public int Id { get; set; }
        public int No_Volumen { get; set; }
        public string Titulo { get; set; }
        public int IsActive { get; set; }
        public short Id_Localizacion { get; set; }
        public string Ubicado_En { get; set; }
    }
}