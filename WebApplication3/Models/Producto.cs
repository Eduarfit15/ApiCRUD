using System;
using System.Collections.Generic;

namespace WebApplication3.Models;

public partial class Producto
{
    public int IdProducto { get; set; }

    public string NomProducto { get; set; } = null!;

    public int Stock { get; set; }

    public decimal Precio { get; set; }

    public virtual ICollection<DetalleVentum> DetalleVenta { get; set; } = new List<DetalleVentum>();

    public virtual ICollection<Venta> Venta { get; set; } = new List<Venta>();
}
