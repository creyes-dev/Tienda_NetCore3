using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Tienda_NetCore3.Models
{ 
    public class DetalleCompra
    {
        public int Id { get; set; }

        [Required]
        public int IdEncabezadoCompra { get; set; }

        [ForeignKey("IdEncabezadoCompra")]
        public EncabezadoCompra EncabezadoCompra { get; set; }

        [Required]
        public int IdServicio { get; set; }

        [ForeignKey("IdEncabezadoCompra")]
        public Servicio Servicio { get; set; }

        [Required]
        public string NombreServicio { get; set; }

        [Required]
        public double Precio { get; set; }


    }
}
