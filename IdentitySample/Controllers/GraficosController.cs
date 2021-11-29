using IdentitySample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IdentitySample.Controllers
{
    public class GraficosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult RelatorioEmprestimos()
        {
            ViewBag.Status = new SelectList(new List<string>()
                {
                    {"Emprestado"},
                    {"Devolvido"},
                    {"Reservado"}
              });
            return View();
        }
    }
}