using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using IdentitySample.Models;
using Microsoft.Reporting.WebForms;

namespace IdentitySample.Controllers
{
    [Authorize(Roles = "Atendente,Admin,Usuario")]
    public class ReservasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Reports(string ReportType)
        {
            LocalReport localreports = new LocalReport();
            localreports.ReportPath = Server.MapPath("~/Reports/ReservasRelatorio.rdlc");

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
            Response.AddHeader("content-disposition", "attachment; filename = Reservas_Relatorio." + fileNameExtension);
            return File(renderedByte, fileNameExtension);

        }

        // GET: Reservas
        public ActionResult Index()
        {
            var reservas = db.Reservas
                .Include(c => c.ReservaLivros);
            return View(reservas.ToList());
        }

        // GET: Reservas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reserva reserva = db.Reservas
                .Include(c => c.ReservaLivros)
                .Include("ReservaLivros.Livro")
                .First(c => c.Id == id);
            if (reserva == null)
            {
                return HttpNotFound();
            }
            return View(reserva);
        }

        // GET: Reservas/Create
        public ActionResult Create()
        {
            ViewBag.Livros = new SelectList(db.Livros.Where(c => c.Ativo).ToList(), "Id", "Titulo");
            return View();
        }

        // POST: Reservas/Create
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Reserva reserva)
        {
            if (ModelState.IsValid)
            {
                
                reserva.Leitor = User.Identity.Name;

                db.Reservas.Add(reserva);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LivroId = new SelectList(db.Livros.Where(c => c.Ativo).ToList(), "Id", "Titulo");
            return View(reserva);
        }

        // GET: Reservas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reserva reserva = db.Reservas
                .Include(c => c.ReservaLivros)
                .First(c => c.Id == id);
            if (reserva == null)
            {
                return HttpNotFound();
            }
            ViewBag.LivroId = new SelectList(db.Livros.Where(c => c.Ativo).ToList(), "Id", "Titulo");
            return View(reserva);
        }

        // POST: Reservas/Edit/5
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Reserva reserva)
        {
            if (ModelState.IsValid)
            {
                db.ReservaLivros.RemoveRange(db.ReservaLivros.Where(c => c.ReservaId == reserva.Id));

                foreach (var item in reserva.ReservaLivros)
                {
                    item.ReservaId = reserva.Id;
                }

                db.ReservaLivros.AddRange(reserva.ReservaLivros);

                reserva.Leitor = User.Identity.Name;
                db.Entry(reserva).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LivroId = new SelectList(db.Livros.Where(c => c.Ativo).ToList(), "Id", "Titulo");
            return View(reserva);
        }

        // GET: Reservas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reserva reserva = db.Reservas
                .Include(c => c.ReservaLivros)
                .Include("ReservaLivros.Livro")
                .First(c => c.Id == id);
            if (reserva == null)
            {
                return HttpNotFound();
            }
            return View(reserva);
        }

        // POST: Reservas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Reserva reserva = db.Reservas.Find(id);
            db.Reservas.Remove(reserva);
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
