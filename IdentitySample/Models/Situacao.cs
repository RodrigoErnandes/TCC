using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdentitySample.Models
{
    [Table("Situacao")]
    public class Situacao
    {
        public Situacao()
        {
            //Emprestimos = new HashSet<Emprestimo>();
        }

        [Key]
        [Display(Name = "Código")]
        public int Id { get; set; }


        [Display(Name = "NomeSituacao")]
        public string NomeSituacao { get; set; }



        //public ICollection<Emprestimo> Emprestimos { get; set; }
    }
}