using System;
using System.Collections.Generic;

namespace Dominican_Hair_Salon.Models;

public partial class Menu
{
    public int ServicioId { get; set; }

    public string NombreServicio { get; set; } = null!;

    public string Statuts { get; set; } = null!;

    public virtual ICollection<Venta> Venta { get; set; } = new List<Venta>();
}
