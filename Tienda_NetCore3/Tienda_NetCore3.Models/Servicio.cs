using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Tienda_NetCore3.Models
{
    public class Servicio
    {
        [Key]
        public int Id { get; set; } 

        [Required]
        [Display(Name ="Nombre del servicio")]
        public string Nombre { get; set; }

        [Required]
        public double Precio { get; set; }

        [Display(Name = "Descripción detallada")]
        public string Descripcion { get; set; }

        [DataType(DataType.ImageUrl)]
        [Display(Name = "Imagen")]
        public string UrlImagen { get; set; }

        [Required]
        public int IdCategoria { get; set; }

        [ForeignKey("IdCategoria")]
        public Categoria Categoria { get; set; }

        [Required]
        public int IdFrecuencia { get; set; }

        [ForeignKey("IdFrecuencia")]
        public Frecuencia Frecuencia { get; set; }


    }
}
