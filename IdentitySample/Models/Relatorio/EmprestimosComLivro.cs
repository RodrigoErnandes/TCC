using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IdentitySample.Models
{
    public class EmprestimosComLivro
    {
        public int Id { get; set; }


        public int LivroId { get; set; }
        public Livro Livro { get; set; }

        public DateTime DataEmprestimo { get; set; }

        public DateTime DataPrevisaoDevolucao { get; set; }

       
        public int SituacaoId { get; set; }
        [ForeignKey("SituacaoId")]
        public Situacao Situacao { get; set; }

        public string Leitor { get; set; }

        public string Titulo { get; set; }
        public List<EmprestimosComLivro> ObterTodos()
        {
            return null;
        }
    }
}