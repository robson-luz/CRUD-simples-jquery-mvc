using Comercio.Database;
using Comercio.Models.Categorias;
using Comercio.ViewModel.Categorias;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Comercio.Controllers
{
    public class CategoriasController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Cadastrar(CadastrarCategoriaViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = 422;
                return Json(ModelState.Values.SelectMany(x => x.Errors));
            }

            using (DbComercio db = new DbComercio())
            {
                Categoria categoriaBanco = db
                    .Categorias
                    .ComDescricao(viewModel.Descricao)
                    .SingleOrDefault();

                if (categoriaBanco != null)
                {
                    Response.StatusCode = 422;
                    return Json(new { ErrorMessage = "Já existe uma categoria com essa descrição." } );
                }

                Categoria categoria = new Categoria()
                {
                    Descricao = viewModel.Descricao
                };

                db.RegistrarNovo(categoria);

                db.Salvar();

                return Json(new { message = "Categoria cadastrada com sucesso." });
            }
        }

        [HttpPost]
        public ActionResult Atualizar(AtualizarCategoriaViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = 422;
                return Json(ModelState.Values.SelectMany(x => x.Errors));
            }

            using (DbComercio db = new DbComercio())
            {
                Categoria categoria = db
                    .Categorias
                    .ComId(viewModel.IdCategoria)
                    .SingleOrDefault();

                if (categoria == null)
                {
                    Response.StatusCode = 422;
                    return Json(new { ErrorMessage = "Categoria não encontrada." });
                }

                Categoria categoriaBanco = db
                    .Categorias
                    .ComDescricao(categoria.Descricao)
                    .Where(c => c.IdCategoria != categoria.IdCategoria)
                    .SingleOrDefault();

                if (categoriaBanco != null)
                {
                    Response.StatusCode = 422;
                    return Json(new { ErrorMessage = "Já existe uma categoria com essa descrição." });
                }

                categoria.Descricao = viewModel.Descricao;

                //db.Categorias.Attach(categoria);
                //db.Entry(categoria).State = EntityState.Modified;
                db.RegistrarAlterado(categoria);

                db.SaveChanges();

                return Json(new { message = "Categoria atualizada com sucesso." });
            }
        }

        [HttpPost]
        public ActionResult Cadastradas(CategoriasCadastradasViewModel viewModel)
        {
            using (DbComercio db = new DbComercio())
            {
                IQueryable<Categoria> categoriasQuery = db
                    .Categorias
                    .OndeDescricaoContem(viewModel.Descricao)
                    .OrderBy(c => c.IdCategoria);

                ICollection<Categoria> categorias = categoriasQuery
                    .Skip(viewModel.Paginacao.Inicio * viewModel.Paginacao.Limite)
                    .Take(viewModel.Paginacao.Limite)
                    .ToList();

                viewModel.Paginacao.TotalRegistros = categoriasQuery.ToList().Count;

                List<dynamic> categoriasJson = new List<dynamic>();

                foreach (Categoria categoria in categorias)
                {
                    categoriasJson.Add(new
                    {
                        idCategoria = categoria.IdCategoria,
                        descricao = categoria.Descricao
                    });
                }

                return Json(new
                {
                    categorias = categoriasJson,
                    paginacao = viewModel.Paginacao
                });
            }
        }

        [HttpPost]
        public ActionResult Remover(IdCategoriaViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return Json(ModelState.Values.SelectMany(x => x.Errors));
            }

            using (DbComercio db = new DbComercio())
            {
                Categoria categoria = db
                    .Categorias
                    .ComId(viewModel.IdCategoria)
                    .SingleOrDefault();

                if (categoria == null)
                {
                    Response.StatusCode = 422;
                    return Json(new { ErrorMessage = "Categoria não encontrada." });
                }

                db.RegistrarRemovido(categoria);

                db.SaveChanges();

                return Json(new { message = "Categoria removida com sucesso." });
            }
        }
    }
}