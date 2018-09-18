using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Comercio.ViewModel.Categorias
{
    public class AtualizarCategoriaViewModel
    {
        [Range(1,Int32.MaxValue, ErrorMessage="Categoria é obrigatória.")]
        public int IdCategoria { get; set; }

        [Required(ErrorMessage ="Campo {0} é obrigatório.")]
        public string Descricao { get; set; }
    }
}