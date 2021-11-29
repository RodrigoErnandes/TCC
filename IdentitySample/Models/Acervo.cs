using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IdentitySample.Models
{
    [Table("Acervos")]
    public class Acervo
    {
        [Key]
        [Display(Name = "Código")]
        public int Id { get; set; }

        [Display(Name = "Livro")]
        [Required(ErrorMessage = "Campo Obrigatório | Favor selecionar o livro")]
        public int LivroId { get; set; }
        [ForeignKey("LivroId")]
        public Livro Livro { get; set; }

        [Display(Name = "Corredor")]
        [Required(ErrorMessage = "Campo Obrigatório | Favor informar a corredor armazenado")]
        public string Corredor { get; set; }

        [Display(Name = "Prateleira")]
        [Required(ErrorMessage = "Campo Obrigatório | Favor informar a prateleira armazenado")]
        public string Prateleira { get; set; }

        [Display(Name = "Estado de Conservação")]
        [Required(ErrorMessage = "Campo Obrigatório | Favor descrever o estado de conservação do livro")]
        public string Estado { get; set; }

        [Display(Name = "Gênero Literário do Livro")]
        [Required(ErrorMessage = "Campo Obrigatório | Favor descrever o gênero do livro")]
        public string Genero { get; set; }

        [DefaultValue(true)]
        public bool Ativo { get; set; } = true;
    }
}