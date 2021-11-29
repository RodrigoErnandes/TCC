using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IdentitySample.Models
{
    [Table("Emprestimos")]
    public class Emprestimo
    {
        [Key]
        [Display(Name = "Código")]
        public int Id { get; set; }


        [Display(Name = "Livro")]
        [Required(ErrorMessage = "Campo Obrigatório | Favor selecionar o livro")]
        public int LivroId { get; set; }
        [ForeignKey("LivroId")]
        public Livro Livro { get; set; }

        [Display(Name = "Data do empréstimo")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Campo Obrigatório | Favor digitar a data do empréstimo do livro")]
        public DateTime DataEmprestimo { get; set; }

        [Display(Name = "Data da Previsão de Devolução")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Campo Obrigatório | Favor digitar a data da devolução do livro")]
        public DateTime DataPrevisaoDevolucao { get; set; }

        [Display(Name = "Status do Empréstimo")]
        [Required(ErrorMessage = "Campo Obrigatório | Favor selecionar o status atual")]
        public string Status { get; set; }

        [Display(Name = "Leitor")]
        [Required(ErrorMessage = "Campo Obrigatório | Favor selecionar o leitor")]
        public string Leitor { get; set; }

        public ICollection<Multa> Multas { get; set; }
    }

}
