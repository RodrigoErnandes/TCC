using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IdentitySample.Models.Relatorio
{
    public class MultasComEmprestimo
    {
        public int LivroId { get;  set; }
        public string Titulo { get;  set; }
        public decimal Valor { get;  set; }
        public string MotivoMulta { get;  set; }
        public string Leitor { get;  set; }
        public string StatusPagamento { get;  set; }

        public List<MultasComEmprestimo> ObterTodos()
        {
            return null;
        }
    }
}