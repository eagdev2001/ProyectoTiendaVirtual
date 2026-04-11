using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Proyecto_Tienda_Virtual.ModelsFromDb;

[Table("VENTAS")]
public partial class Venta
{
    [Key]
    [Column("TransaccionID")]
    public int TransaccionId { get; set; }

    [Column("ProductoID")]
    public int ProductoId { get; set; }

    [Column("CantidadADQ")]
    public int? CantidadAdq { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? FechaHora { get; set; }

    [ForeignKey("ProductoId")]
    [InverseProperty("Venta")]
    public virtual Producto Producto { get; set; } = null!;
}
