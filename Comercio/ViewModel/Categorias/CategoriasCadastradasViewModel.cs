using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Comercio.ViewModel.Categorias
{
    public class CategoriasCadastradasViewModel
    {
        public string Descricao { get; set; }

        public PaginacaoViewModel Paginacao { get; set; }

        public CategoriasCadastradasViewModel()
        {
            Paginacao = new PaginacaoViewModel();
        }
    }
}