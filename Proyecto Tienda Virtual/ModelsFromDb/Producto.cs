using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Proyecto_Tienda_Virtual.ModelsFromDb;

[Table("PRODUCTOS")]
public partial class Producto
{
    [Key]
    [Column("ProductoID")]
    public int ProductoId { get; set; }

    [Column("DescripcionART")]
    [StringLength(100)]
    [Unicode(false)]
    [Display(Name = "Descripción")]
    public string DescripcionArt { get; set; } = null!;

    [Display(Name = "Precio")]
    public double ValorMonetario { get; set; }

    [Column("ExistenciasACT")]
    [Display(Name = "Stock")]
    public int ExistenciasAct { get; set; }

    [InverseProperty("Producto")]
    public virtual ICollection<Venta> Venta { get; set; } = new List<Venta>();
}
