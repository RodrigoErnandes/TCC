using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IdentitySample.Models.Relatorio
{
    public class BaixasComLivro
    {
        public int LivroId { get; set; }
        public string Titulo { get; set; }
        public string Motivo { get; set; }
        public string Destino { get; set; }
        public DateTime DataBaixa { get; set; }

        public List<BaixasComLivro> ObterTodos()
        {
            return null;
        }
    }
}