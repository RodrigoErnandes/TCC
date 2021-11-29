using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IdentitySample.Models
{
    [Table("Multa")]
    public class Multa
    {
        [Key]
        [Display(Name = "Código")]
        public int Id { get; set; }

        [Display(Name = "Valor da Multa")]
        public decimal Valor { get; set; }

        [Display(Name = "Status do Pagamento")]
        [Required(ErrorMessage = "Campo Obrigatório | Favor selecionar o status atual do pagamento")]
        public string StatusPagamento { get; set; }

        [Display(Name = "Motivo da Multa")]
        [Required(ErrorMessage = "Campo Obrigatório | Favor selecionar o motivoda da cobrança da multa")]
        public string MotivoMulta { get; set; }

        [Display(Name = "Observação")]
        public string Observacao { get; set; }

        [Display(Name = "Livro")]
        [Required(ErrorMessage = "Campo Obrigatório | Favor selecionar o Livro")]
        public int LivroId { get; set; }
        [ForeignKey("LivroId")]
        public Livro Livro { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório | Favor selecionar o Livro")]
        public string Leitor { get; set; }

    }
}