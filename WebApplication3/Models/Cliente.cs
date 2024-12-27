using System;
using System.Collections.Generic;

namespace WebApplication3.Models;

public partial class Cliente
{
    public string Dni { get; set; } = null!;

    public string Nombres { get; set; } = null!;

    public string Apellidos { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public string Celular { get; set; } = null!;

    public string? Direccion { get; set; }

    public virtual ICollection<Venta> Venta { get; set; } = new List<Venta>();
}
