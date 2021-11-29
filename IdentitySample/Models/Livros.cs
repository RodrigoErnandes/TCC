using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IdentitySample.Models
{
    [Table("Livros")]
    public class Livro
    {
        [Key]
        [Display(Name = "Código")]
        public int Id { get; set; }

        [Display(Name = "Título do Livro")]
        [Required(ErrorMessage = "Campo Obrigatório | Favor digitar o título do livro ")]
        public string Titulo { get; set; }

        [Display(Name = "Autor do Livro")]
        [Required(ErrorMessage = "Campo Obrigatório | Favor digitar o autor do livro ")]
        public string Autor { get; set; }

        [Display(Name = "Editora")]
        [Required(ErrorMessage = "Campo Obrigatório | Favor digitar a editora do livro")]
        public string Editora { get; set; }

        [Display(Name = "Páginas do Livro")]
        [Required(ErrorMessage = "Campo Obrigatório | Favor digitar a quantidade de páginas do livro ")]
        public int Paginas { get; set; }

        public ICollection<Emprestimo> Emprestimos { get; set; }
        public ICollection<Multa> Multas { get; set; }
    }
}
