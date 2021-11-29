using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IdentitySample.Models;
using Microsoft.Reporting.WebForms;

namespace IdentitySample.Controllers
{
    [Authorize(Roles = "Atendente,Admin")]
    public class BaixasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Reports(string ReportType)
        {
            LocalReport localreports = new LocalReport();
            localreports.ReportPath = Server.MapPath("~/Reports/BaixasRelatorio.rdlc");

            ReportDataSource reportDataSource = new ReportDataSource();
            reportDataSource.Name = "DataSet1";
            reportDataSource.Value = db.Database.SqlQuery<EmprestimosComLivro>("select E.*,L.Titulo from Emprestimos E join Livros L ON E.LivroId=L.Id").ToList();
            localreports.DataSources.Add(reportDataSource);
            string reportType = ReportType;
            string mimeType;
            string encoding;
            string fileNameExtension;

            if (reportType == "Excel")
            {
                fileNameExtension = "xlsx";
            }
            else
            {
                fileNameExtension = "pdf";
            }

            string[] streams;
            Warning[] warnings;
            byte[] renderedByte;
            renderedByte = localreports.Render(reportType, "", out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
            Response.AddHeader("content-disposition", "attachment; filename = Baixas_Relatorio." + fileNameExtension);
            return File(renderedByte, fileNameExtension);

        }
        // GET: Baixas
        public ActionResult Index()
        {
            var baixas = db.Baixas.Include(b => b.Livro);
            return View(baixas.ToList());
        }

        // GET: Baixas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Baixa baixa = db.Baixas
                .Include(c => c.Livro)
                .First(c => c.Id == id);
            if (baixa == null)
            {
                return HttpNotFound();
            }
            return View(baixa);
        }

        // GET: Baixas/Create
        public ActionResult Create(int id)
        {
            ViewBag.ListaLivros = new SelectList(db.Livros, "Id", "Titulo", id);
            return View();
        }

        // POST: Baixas/Create
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,LivroId,MotivoBaixa,Destino,Databaixa")] Baixa baixa)
        {
            if (ModelState.IsValid)
            {
                db.Baixas.Add(baixa);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LivroId = new SelectList(db.Livros, "Id", "Titulo", baixa.LivroId);
            return View(baixa);
        }

        // GET: Baixas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Baixa baixa = db.Baixas.Find(id);
            if (baixa == null)
            {
                return HttpNotFound();
            }
            ViewBag.LivroId = new SelectList(db.Livros, "Id", "Titulo", baixa.LivroId);
            return View(baixa);
        }

        // POST: Baixas/Edit/5
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,LivroId,MotivoBaixa,Destino,Databaixa")] Baixa baixa)
        {
            if (ModelState.IsValid)
            {
                db.Entry(baixa).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LivroId = new SelectList(db.Livros, "Id", "Titulo", baixa.LivroId);
            return View(baixa);
        }

        // GET: Baixas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Baixa baixa = db.Baixas
                .Include(c => c.Livro)
                .First(c => c.Id == id);
            if (baixa == null)
            {
                return HttpNotFound();
            }
            return View(baixa);
        }

        // POST: Baixas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Baixa baixa = db.Baixas.Find(id);
            db.Baixas.Remove(baixa);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
