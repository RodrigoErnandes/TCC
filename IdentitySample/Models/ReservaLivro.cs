using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IdentitySample.Models
{
    [Table("ReservaLivros")]
    public class ReservaLivro
    {
        internal object Destino;

        public int Id { get; set; }

        [Display(Name = "Livro")]
        [Required(ErrorMessage = "Campo Obrigatório | Favor selecionar o livro")]
        public int LivroId { get; set; }
        [ForeignKey("LivroId")]
        public Livro Livro { get; set; }


       

        [Display(Name = "Reserva")]
        [Required(ErrorMessage = "Campo Obrigatório | Favor selecionar a reserva")]
        public int ReservaId { get; set; }
        [ForeignKey("ReservaId")]
        public Reserva Reserva { get; set; }
    }
}