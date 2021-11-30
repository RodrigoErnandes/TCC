using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IdentitySample.Identity;
using IdentitySample.Models;
using IdentitySample.Models.Relatorio;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Reporting.WebForms;

namespace IdentitySample.Controllers
{
    [Authorize(Roles = "Atendente,Admin")]
    public class MultasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ActionResult Reports(string ReportType)
        {
            LocalReport localreports = new LocalReport();
            localreports.ReportPath = Server.MapPath("~/Reports/MultasRelatorio.rdlc");

            ReportDataSource reportDataSource = new ReportDataSource();
            reportDataSource.Name = "DataSet1";
            var lista = db.Multas.Include(c => c.Livro).Select(c => new MultasComEmprestimo
            {
                LivroId = c.LivroId,
                Titulo = c.Livro.Titulo,                
                Valor = c.Valor,
                MotivoMulta = c.MotivoMulta,
                StatusPagamento= c.StatusPagamento,
                Leitor = c.Leitor,
            }).ToList();

            reportDataSource.Value = lista;
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
            Response.AddHeader("content-disposition", "attachment; filename = Multas_Relatorio." + fileNameExtension);
            return File(renderedByte, fileNameExtension);

        }
        // GET: Multas
        public ActionResult Index()
        {
            var multas = db.Multas.Include(m => m.Livro);
            return View(multas.ToList());
        }

        // GET: Multas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Multa multa = db.Multas
                .Include(c => c.Livro)
                .First(c => c.Id == id);
            if (multa == null)
            {
                return HttpNotFound();
            }
            return View(multa);
        }

        [HttpPost, ActionName("Pagar")]
        [ValidateAntiForgeryToken]
        public ActionResult Pagar(int id)
        {
            Multa multa = db.Multas.Find(id);

            multa.StatusPagamento = "Pago";
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: Multas/Create
        public ActionResult Create()
        {
            ViewBag.Leitor = new SelectList(UserManager.Users, "UserName", "UserName");
            ViewBag.LivroId = new SelectList(db.Livros, "Id", "Titulo");
            ViewBag.MotivoMulta = new SelectList(new List<String>() {
                { "Atraso" },
                { "Extravio" },
                { "Destruição" }

            });
            ViewBag.StatusPagamento = new SelectList(new List<String>() {
                { "Pendente" },
                { "Pago" },
                { "Abonado" }
            });
            return View();
        }

        // POST: Multas/Create
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Valor,StatusPagamento,MotivoMulta,LivroId,Observacao,Leitor")] Multa multa)
        {
            if (ModelState.IsValid)
            {
                db.Multas.Add(multa);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Leitor = new SelectList(UserManager.Users, "UserName", "UserName");
            ViewBag.LivroId = new SelectList(db.Livros, "Id", "Titulo", multa.LivroId);
            ViewBag.MotivoMulta = new SelectList(new List<String>() {
                { "Atraso" },
                { "Extravio" },
                { "Destruição" }

            });
            ViewBag.StatusPagamento = new SelectList(new List<String>() {
                { "Pendente" },
                { "Pago" },
                { "Abonado" }
            });
            return View(multa);
        }

        // GET: Multas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Multa multa = db.Multas.Find(id);
            if (multa == null)
            {
                return HttpNotFound();
            }
            ViewBag.Leitor = new SelectList(UserManager.Users, "UserName", "UserName");
            ViewBag.LivroId = new SelectList(db.Livros, "Id", "Titulo", multa.LivroId);
            ViewBag.MotivoMulta = new SelectList(new List<String>() {
                { "Atraso" },
                { "Extravio" },
                { "Destruição" }

            });
            ViewBag.StatusPagamento = new SelectList(new List<String>() {
                { "Pendente" },
                { "Pago" },
                { "Abonado" }
            });
            return View(multa);
        }

        // POST: Multas/Edit/5
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Valor,StatusPagamento,MotivoMulta,LivroId,Observacao")] Multa multa)
        {
            if (ModelState.IsValid)
            {
                db.Entry(multa).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Leitor = new SelectList(UserManager.Users, "UserName", "UserName");
            ViewBag.LivroId = new SelectList(db.Livros, "Id", "Titulo", multa.LivroId);
            ViewBag.MotivoMulta = new SelectList(new List<String>() {
                { "Atraso" },
                { "Extravio" },
                { "Destruição" }

            });
            ViewBag.StatusPagamento = new SelectList(new List<String>() {
                { "Pendente" },
                { "Pago" },
                { "Abonado" }
            });
            return View(multa);
        }

        // GET: Multas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Multa multa = db.Multas
                .Include(c => c.Livro)
                .First(c => c.Id == id);
            if (multa == null)
            {
                return HttpNotFound();
            }
            return View(multa);
        }

        // POST: Multas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Multa multa = db.Multas.Find(id);
            db.Multas.Remove(multa);
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
