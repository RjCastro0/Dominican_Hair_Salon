using System;
using System.Collections.Generic;

namespace Dominican_Hair_Salon.Models;

public partial class Inventario
{
    public int SucursalId { get; set; }

    public int InventarioId { get; set; }

    public string ProductName { get; set; } = null!;

    public int Cantidad { get; set; }

    public string Status { get; set; } = null!;

    public virtual Sucursal Sucursal { get; set; } = null!;
}
