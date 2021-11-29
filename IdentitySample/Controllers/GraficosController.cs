using IdentitySample.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;


namespace IdentitySample.Controllers
{
    public class GraficosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult RelatorioEmprestimos()
        {
            

            ViewBag.ListaQtdeReservasPorMes = db.ReservaLivros.GroupBy(c => new { ano = c.Reserva.DataReserva.Year, mes = c.Reserva.DataReserva.Month })
                    .Select(c => new GraficoViewModel
                    {
                        descricao = c.Key.mes + "/" + c.Key.ano,
                        qtde = c.Count()
                    }).ToList();

            ViewBag.ListaQtdeBaixasPorMes = db.Baixas.GroupBy(c => new { ano = c.Databaixa.Year, mes = c.Databaixa.Month })
                    .Select(c => new GraficoViewModel
                    {
                        descricao = c.Key.mes + "/" + c.Key.ano,
                        qtde = c.Count()
                    }).ToList();


            var emprestimos = db.Emprestimos.Include(e => e.Livro);

            ViewBag.ListaGeneroEmprestados = db.Acervos
                .Include(c => c.Livro)
                .Include(c => c.Livro.Emprestimos)
                .GroupBy(c => new { genero = c.Genero })             
                 .Select(c => new GraficoViewModel
                 {
                     descricao = c.Key.genero,
                     qtde = c.Sum(d => d.Livro.Emprestimos.Count())
                 }).Where(c=> c.qtde>0)
                 .ToList();

            ViewBag.ListaEstadoConservacao = db.Acervos
                .Include(c => c.Livro)
                .Include(c => c.Estado)
                .GroupBy(c => new { estado = c.Estado })
                 .Select(c => new GraficoViewModel
                 {
                     descricao = c.Key.estado,
                     qtde = c.Count()
                 }).Where(c => c.qtde > 0)
                 .ToList();

            ViewBag.ListaQtdeLivrosEmprestados = db.Emprestimos
                .Include(c => c.Livro)           
                .GroupBy(c => new {c.Livro.Titulo } )
                 .Select(c => new GraficoViewModel
                 {
                     descricao = c.Key.Titulo,
                     qtde = c.Count()
                 }).Where(c => c.qtde > 0)
                 .ToList();

            return View();
        }
    }
}