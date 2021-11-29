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
    
    public class LivrosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Reports(string ReportType)
        {
            LocalReport localreports = new LocalReport();
            localreports.ReportPath = Server.MapPath("~/Reports/LivrosRelatorio.rdlc");

            ReportDataSource reportDataSource = new ReportDataSource();
            reportDataSource.Name = "LivrosDataSet";
            reportDataSource.Value = db.Livros.ToList();
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
            Response.AddHeader("content-disposition", "attachment; filename = Livros_Relatorio." + fileNameExtension);
            return File(renderedByte, fileNameExtension);

        }

        [Authorize(Roles = "Admin,Atendente,Usuario")]
        // GET: Livros
        public ActionResult Index()
        {
            return View(db.Livros.ToList());
        }

        // GET: Livros/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Livro livro = db.Livros.Find(id);
            if (livro == null)
            {
                return HttpNotFound();
            }
            return View(livro);
        }

        [Authorize(Roles = "Admin,Atendente")]
        // GET: Livros/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Livros/Create
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Atendente")]
        public ActionResult Create([Bind(Include = "Id,Titulo,Autor,Editora,Paginas")] Livro livro)
        {
            if (ModelState.IsValid)
            {
                db.Livros.Add(livro);
                db.SaveChanges();
                return RedirectToAction("Create","Acervos");
            }

            return View(livro);
        }

        // GET: Livros/Edit/5
        [Authorize(Roles = "Admin,Atendente")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Livro livro = db.Livros.Find(id);
            if (livro == null)
            {
                return HttpNotFound();
            }
            return View(livro);
        }

        // POST: Livros/Edit/5
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Atendente")]
        public ActionResult Edit([Bind(Include = "Id,Titulo,Autor,Editora,Paginas")] Livro livro)
        {
            if (ModelState.IsValid)
            {
                db.Entry(livro).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(livro);
        }

        // GET: Livros/Delete/5
        [Authorize(Roles = "Admin,Atendente")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Livro livro = db.Livros.Find(id);
            if (livro == null)
            {
                return HttpNotFound();
            }
            return View(livro);
        }

        // POST: Livros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Atendente")]
        public ActionResult DeleteConfirmed(int id)
        {
            Livro livro = db.Livros.Find(id);
            db.Livros.Remove(livro);
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
