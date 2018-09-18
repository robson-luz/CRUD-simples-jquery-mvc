using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Comercio.ViewModel
{
    public class PaginacaoViewModel
    {
        public int Inicio { get; set; }

        public int Limite { get; set; }

        public int TotalRegistros { get; set; }
    }
}