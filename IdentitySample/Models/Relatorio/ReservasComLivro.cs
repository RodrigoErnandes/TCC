using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IdentitySample.Models.Relatorio
{
    public class ReservasComLivro
    {
        public int LivroId { get; set; }
        public string Leitor { get; set; }
        public DateTime DataReserva { get; set; }
        public string Observacao { get; set; }
        public string Titulo { get; set; }

        public List<ReservasComLivro> ObterTodos()
        {
            return null;
        }
    }
}