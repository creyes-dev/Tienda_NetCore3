using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Tienda_NetCore3.Models
{
    public class Categoria
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Nombre de Categoría")]
        public string Nombre { get; set; }

        [Required]
        [Display(Name = "Ordenamiento")]
        public int Orden { get; set; }

    }
}
