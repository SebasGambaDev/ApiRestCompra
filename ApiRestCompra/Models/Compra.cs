using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiRestCompra.Models
{
    public class Compra
    {
        public Compra()
        {
            Detalles = new HashSet<Detalle>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string ClienteNombre1 { get; set; }

        [MaxLength(30)]
        public string? ClienteNombre2 { get; set; }

        [Required]
        [MaxLength(30)]
        public string ClienteApellido1 { get; set; }

        [MaxLength(30)]
        public string? ClienteApellido2 { get; set; }

        [Required]
        [MaxLength(30)]
        public string ClienteEmail { get; set; }

        [Required]
        [MaxLength(100)]
        public string ClienteDireccionDespacho { get; set; }

        [Required]
        [MaxLength(15)]
        public string CiudadDespacho { get; set; }

        [Required]
        [MaxLength(100)]
        public string ClienteDireccionFacturacion { get; set; }

        [Required]
        [MaxLength(15)]
        public string CiudadFacturacion { get; set; }

        [Required]
        [MaxLength(15)]
        public string ClienteTelefono1 { get; set; }

        [MaxLength(15)]
        public string? ClienteTelefono2 { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal ValorFlete { get; set; }

        public int NumeroFactura { get; set; }

        public int? TotalArticulos { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? TotalImpuestosVenta { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? TotalImpuestosFlete { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? TotalImpuestosNetos { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? ValorTotalFactura { get; set; }



        public virtual ICollection<Detalle> Detalles { get; set; }


    }
}
