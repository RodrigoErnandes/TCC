using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using IdentitySample.Identity;
using IdentitySample.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Reporting.WebForms;

namespace IdentitySample.Controllers
{
    [Authorize(Roles = "Atendente,Admin")]
    public class EmprestimosController : Controller
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
            localreports.ReportPath = Server.MapPath("~/Reports/EmprestimoRelatorio.rdlc");

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
            Response.AddHeader("content-disposition", "attachment; filename = Emprestimos_Relatorio." + fileNameExtension);
            return File(renderedByte, fileNameExtension);

        }

        // GET: Emprestimos
        public ActionResult Index()
        {

            var emprestimos = db.Emprestimos.Include(e => e.Livro);
            return View(emprestimos.ToList());
        }

        // GET: Emprestimos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Emprestimo emprestimo = db.Emprestimos.Find(id);
            Emprestimo emprestimo = db.Emprestimos
            .Include(c => c.Livro)
                .First(c => c.Id == id);
            if (emprestimo == null)
            {
                return HttpNotFound();
            }
            return View(emprestimo);
        }

        // GET: Emprestimos/Create
        public ActionResult Create()
        {
            ViewBag.Leitor = new SelectList(UserManager.Users, "UserName", "UserName");
            ViewBag.LivroId = new SelectList(db.Livros.Where(c => c.Ativo && !c.Emprestimos.Any(d => d.Status == "Emprestado")).ToList(), "Id", "Titulo");

            return View();
        }

        // POST: Emprestimos/Create
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,LivroId,DataEmprestimo,DataPrevisaoDevolucao,Status,Leitor")] Emprestimo emprestimo)
        {
            if (ModelState.IsValid)
            {
                emprestimo.Status = "Emprestado";

                db.Emprestimos.Add(emprestimo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Leitor = new SelectList(UserManager.Users, "UserName", "UserName");
            ViewBag.LivroId = new SelectList(db.Livros.Where(c => c.Ativo && !c.Emprestimos.Any(d => d.Status == "Emprestado")).ToList(), "Id", "Titulo");
            return View(emprestimo);
        }

        // GET: Emprestimos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Emprestimo emprestimo = db.Emprestimos.Find(id);
            if (emprestimo == null)
            {
                return HttpNotFound();
            }
            ViewBag.Leitor = new SelectList(UserManager.Users, "UserName", "UserName");
            ViewBag.LivroId = new SelectList(db.Livros.Where(c => c.Ativo && !c.Emprestimos.Any(d => d.Status == "Emprestado")).ToList(), "Id", "Titulo");

            return View(emprestimo);
        }

        // POST: Emprestimos/Edit/5
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,LivroId,DataEmprestimo,DataPrevisaoDevolucao,Leitor,Status")] Emprestimo emprestimo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(emprestimo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Leitor = new SelectList(UserManager.Users, "UserName", "UserName");
            ViewBag.LivroId = new SelectList(db.Livros.Where(c => c.Ativo && !c.Emprestimos.Any(d => d.Status == "Emprestado")).ToList(), "Id", "Titulo");

            return View(emprestimo);
        }

        // GET: Emprestimos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Emprestimo emprestimo = db.Emprestimos.Find(id);
            if (emprestimo == null)
            {
                return HttpNotFound();
            }
            return View(emprestimo);
        }

        // POST: Emprestimos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Emprestimo emprestimo = db.Emprestimos.Find(id);
            db.Emprestimos.Remove(emprestimo);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        [HttpPost, ActionName("Devolver")]
        [ValidateAntiForgeryToken]
        public ActionResult Devolver(int id)
        {
            Emprestimo emprestimo = db.Emprestimos.Find(id);

            emprestimo.Status = "Devolvido";
            db.SaveChanges();



            var livro = db.ReservaLivros
                .Include(c => c.Livro)
                .Include(c => c.Reserva)
                .Where(c => c.LivroId == emprestimo.LivroId).OrderBy(c => c.Reserva.DataReserva).FirstOrDefault();

            if (livro != null)
            {
                string enviaMensagem = @"Olá, 
<br/><br/>
O livro que você reservou está disponível. 
<br/><br/>
Livro: " + livro.Livro.Titulo + @"

";
                var email = new MailMessage
                {
                    From = new MailAddress("rodrigoernandesgdh@gmail.com"),
                    IsBodyHtml = true,
                    Body = enviaMensagem,
                    BodyEncoding = Encoding.GetEncoding("ISO-8859-1"),
                    Subject = "Livro Disponível",
                    SubjectEncoding = Encoding.GetEncoding("ISO-8859-1")
                };
                email.To.Add(livro.Reserva.Leitor);

                var sc = new SmtpClient("smtp.gmail.com", 587)
                {
                    Credentials = new NetworkCredential("rodrigoernandesgdh@gmail.com", "CavaloeCaneta"),
                    EnableSsl = true
                };

                sc.Send(email);
            }
            if (emprestimo.DataPrevisaoDevolucao < DateTime.Today)
            {
                db.Multas.Add(new Multa
                {
                    Leitor = emprestimo.Leitor,
                    LivroId = emprestimo.LivroId,
                    MotivoMulta = "Atraso",
                    StatusPagamento = "Pendente",
                    Valor = 10
                });
                db.SaveChanges();
                return RedirectToAction("Index", "Multas");
            }


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
