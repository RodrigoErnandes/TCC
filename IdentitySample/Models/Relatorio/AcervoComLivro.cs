using System.Collections.Generic;

namespace IdentitySample.Models.Relatorio
{
    public class AcervoComLivro
    {
        public int LivroId { get; set; }
        public string Titulo { get; set; }
        public string Estado { get; set; }
        public string Corredor { get; set; }
        public string Prateleira { get; set; }
        public string Genero { get; set; }


        public List<AcervoComLivro> ObterTodos()
        {
            return null;
        }
    }
}