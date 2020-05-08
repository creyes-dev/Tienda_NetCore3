using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Tienda_NetCore3.Models
{
    public class EncabezadoCompra
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Telefono { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Direccion { get; set; }

        [Required]
        public string Ciudad { get; set; }

        [Required]
        public string CodigoPostal { get; set; }

        public DateTime FechaCompra { get; set; }

        public string Status { get; set; }

        public string Comentarios { get; set; }

        public int ConteoServicios { get; set; }

    }
}
