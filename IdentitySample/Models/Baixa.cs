using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IdentitySample.Models
{
    [Table("Baixas")]
    public class Baixa
    {
        [Key]
        [Display(Name = "Código")]
        public int Id { get; set; }

        [Display(Name = "Livro")]
        [Required(ErrorMessage = "Campo Obrigatório | Favor selecionar o livro")]
        public int LivroId { get; set; }
        [ForeignKey("LivroId")]
        public Livro Livro { get; set; }

        [Display(Name = "Motivo da baixa do livro")]
        [Required(ErrorMessage = "Campo Obrigatório | Motivo da baixa do livro")]
        public string MotivoBaixa { get; set; }

        [Display(Name = "Destinação do livro baixado")]
        [Required(ErrorMessage = "Campo Obrigatório | Favor informar a destinação do livro baixado")]
        public string Destino { get; set; }

        [Display(Name = "Data da baixa")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Campo Obrigatório | Favor digitar a data dabaixa do livro")]
        public DateTime Databaixa { get; set; }
    }
}