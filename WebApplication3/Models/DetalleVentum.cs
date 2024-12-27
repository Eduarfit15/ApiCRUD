using System;
using System.Collections.Generic;

namespace WebApplication3.Models;

public partial class DetalleVentum
{
    public int IdDetalleVenta { get; set; }

    public int IdFactura { get; set; }

    public int IdProducto { get; set; }

    public int Cantidad { get; set; }

    public decimal Subtotal { get; set; }

    public virtual Venta? IdFacturaNavigation { get; set; } = null!;

    public virtual Producto? IdProductoNavigation { get; set; } = null!;
}
