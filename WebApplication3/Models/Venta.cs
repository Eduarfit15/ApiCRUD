using System;
using System.Collections.Generic;

namespace WebApplication3.Models;

public partial class Venta
{
    public int IdFactura { get; set; }

    public string? Cliente { get; set; }

    public int? Productos { get; set; }

    public DateOnly Fecha { get; set; }

    public decimal ImportFinal { get; set; }

    public virtual Cliente? ClienteNavigation { get; set; }

    public virtual ICollection<DetalleVentum> DetalleVenta { get; set; } = new List<DetalleVentum>();

    public virtual Producto? ProductosNavigation { get; set; } = null!;
}
