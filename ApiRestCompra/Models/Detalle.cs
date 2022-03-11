using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiRestCompra.Models
{
    public class Detalle
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength (30)]
        public string CodigoReferencia { get; set; }

        [Required]
        public int Cantidad { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$")]
        [Range(0, 9999999999999999.99)]
        public decimal ValorUnitario { get; set; }

        [MaxLength(1000)]
        public string? Descripcion { get; set; }

        [MaxLength(30)]
        public string? referencia { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$")]
        [Range(0, 9999999999999999.99)]
        public decimal ValorTotal { get; set; }

        //foreing keys

        [Required]
        public int CompraId { get; set; }

        [ForeignKey("CompraId")]
        public Compra Compra { get; set; }


    }
}
