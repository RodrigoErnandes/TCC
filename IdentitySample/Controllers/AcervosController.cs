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
    public class AcervosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Reports(string ReportType)
        {
            LocalReport localreports = new LocalReport();
            localreports.ReportPath = Server.MapPath("~/Reports/AcervosRelatorio.rdlc");

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
            Response.AddHeader("content-disposition", "attachment; filename = Acervos_Relatorio." + fileNameExtension);
            return File(renderedByte, fileNameExtension);

        }

        // GET: Acervos
        public ActionResult Index()
        {
            var acervos = db.Acervos.Include(a => a.Livro);
            return View(acervos.ToList());
        }

        // GET: Acervos/Details/5
        public ActionResult Details(int? id)
        {
            ViewBag.LivroId = new SelectList(db.Livros, "Id", "Titulo");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Acervo acervo = db.Acervos
                .Include(c => c.Livro)
                .First(c => c.Id == id);
            if (acervo == null)
            {
                return HttpNotFound();
            }
            return View(acervo);
        }


        // GET: Acervos/Create
        public ActionResult Create()
        {
            ViewBag.LivroId = new SelectList(db.Livros, "Id", "Titulo");
            ViewBag.Estado = new SelectList(new List<string>()
                {
                    {"Novo"},
                    {"Bom"},
                    {"Ruim"},
                    {"Péssimo - Baixar"}
              });
            ViewBag.Genero = new SelectList(new List<string>()
                {
                        {"Fantasia"},
                        {"Ficção científica"},
                        {"Distopia"},
                        {"Ação e aventura"},
                        {"Ficção Policial"},
                        {"Horror"},
                        {"Thriller e Suspense"},
                        {"Ficção histórica"},
                        {"Romance"},
                        {"Ficção Feminina"},
                        {"LGBTQ+"},
                        {"Ficção Contemporânea"},
                        {"Realismo mágico"},
                        {"Graphic Novel"},
                        {"Conto"},
                        {"Young adult – Jovem adulto"},
                        {"New adult – Novo Adulto "},
                        {"Infantil"},
                        {"Memórias e autobiografia"},
                        {"Biografia"},
                        {"Gastronomia"},
                        {"Arte e Fotografia"},
                        {"Autoajuda"},
                        {"História"},
                        {"Viajem"},
                        {"Crimes Reais"},
                        {"Humor"},
                        {"Ensaios"},
                        {"Guias & Como fazer "},
                        {"Religião e Espiritualidade"},
                        {"Humanidades e Ciências Sociais"},
                        {"Paternidade e família"},
                        {"Tecnologia e Ciência"},
                        {"Infantil"}
              });
            return View();
        }

        // POST: Acervos/Create
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,LivroId,Corredor,Prateleira,Estado,Genero")] Acervo acervo)
        {
            if (ModelState.IsValid)
            {
                db.Acervos.Add(acervo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LivroId = new SelectList(db.Livros, "Id", "Titulo", acervo.LivroId);
            ViewBag.Status = new SelectList(new List<string>()
                {
                    {"Emprestado"},
                    {"Devolvido"},
                    {"Reservado"}
              });

            ViewBag.Genero = new SelectList(new List<string>()
                {
                        {"Fantasia"},
                        {"Ficção científica"},
                        {"Distopia"},
                        {"Ação e aventura"},
                        {"Ficção Policial"},
                        {"Horror"},
                        {"Thriller e Suspense"},
                        {"Ficção histórica"},
                        {"Romance"},
                        {"Ficção Feminina"},
                        {"LGBTQ+"},
                        {"Ficção Contemporânea"},
                        {"Realismo mágico"},
                        {"Graphic Novel"},
                        {"Conto"},
                        {"Young adult – Jovem adulto"},
                        {"New adult – Novo Adulto "},
                        {"Infantil"},
                        {"Memórias e autobiografia"},
                        {"Biografia"},
                        {"Gastronomia"},
                        {"Arte e Fotografia"},
                        {"Autoajuda"},
                        {"História"},
                        {"Viajem"},
                        {"Crimes Reais"},
                        {"Humor"},
                        {"Ensaios"},
                        {"Guias & Como fazer "},
                        {"Religião e Espiritualidade"},
                        {"Humanidades e Ciências Sociais"},
                        {"Paternidade e família"},
                        {"Tecnologia e Ciência"},
                        {"Infantil"}
              });
            return View(acervo);
        }

        // GET: Acervos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Acervo acervo = db.Acervos.Find(id);

            if (acervo == null)
            {
                return HttpNotFound();
            }

            ViewBag.LivroId = new SelectList(db.Livros, "Id", "Titulo", acervo.LivroId);
            ViewBag.Estado = new SelectList(new List<string>()
                {
                    {"Novo"},
                    {"Bom"},
                    {"Ruim"},
                    {"Péssimo - Baixar"}
              }, acervo.Estado);
            ViewBag.Genero = new SelectList(new List<string>()
                {
                        {"Fantasia"},
                        {"Ficção científica"},
                        {"Distopia"},
                        {"Ação e aventura"},
                        {"Ficção Policial"},
                        {"Horror"},
                        {"Thriller e Suspense"},
                        {"Ficção histórica"},
                        {"Romance"},
                        {"Ficção Feminina"},
                        {"LGBTQ+"},
                        {"Ficção Contemporânea"},
                        {"Realismo mágico"},
                        {"Graphic Novel"},
                        {"Conto"},
                        {"Young adult – Jovem adulto"},
                        {"New adult – Novo Adulto "},
                        {"Infantil"},
                        {"Memórias e autobiografia"},
                        {"Biografia"},
                        {"Gastronomia"},
                        {"Arte e Fotografia"},
                        {"Autoajuda"},
                        {"História"},
                        {"Viajem"},
                        {"Crimes Reais"},
                        {"Humor"},
                        {"Ensaios"},
                        {"Guias & Como fazer "},
                        {"Religião e Espiritualidade"},
                        {"Humanidades e Ciências Sociais"},
                        {"Paternidade e família"},
                        {"Tecnologia e Ciência"},
                        {"Infantil"}
              });
            return View(acervo);
        }

        // POST: Acervos/Edit/5
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,LivroId,Corredor,Prateleira,Estado,Genero")] Acervo acervo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(acervo).State = EntityState.Modified;
                db.SaveChanges();

                if (acervo.Estado == "Péssimo - Baixar")
                {
                    var baixa = new Baixa
                    {
                        Databaixa = DateTime.Today,
                        Destino = "AGUARDANDO LOCAL",
                        LivroId = acervo.LivroId,
                        MotivoBaixa = "BAIXA AUTOMÁTICA"
                    };

                    db.Baixas.Add(baixa);
                    db.SaveChanges();

                    return RedirectToAction("Edit", "Baixas", new { id = baixa.Id });
                }


                return RedirectToAction("Index");
            }

            return Edit(acervo);
        }

        // GET: Acervos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Acervo acervo = db.Acervos
                .Include(c => c.Livro)
                .First(c => c.Id == id);
            if (acervo == null)
            {
                return HttpNotFound();
            }
            return View(acervo);
        }

        // POST: Acervos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Acervo acervo = db.Acervos.Find(id);
            db.Acervos.Remove(acervo);
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
