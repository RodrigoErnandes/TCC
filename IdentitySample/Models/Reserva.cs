using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IdentitySample.Models
{
    [Table("Reservas")]
    public class Reserva
    {
        public Reserva()
        {
            ReservaLivros = new HashSet<ReservaLivro>();
        }

        [Key]
        [Display(Name = "Código")]
        public int Id { get; set; }


        [Display(Name = "Data da Reserva")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Campo Obrigatório | Favor digitar a data da reserva do livro")]
        public DateTime DataReserva { get; set; }

        [Display(Name = "Observação")]
        public string Observacao { get; set; }

        public string Leitor { get; set; }

        public ICollection<ReservaLivro> ReservaLivros { get; set; }
    }
}