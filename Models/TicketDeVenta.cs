using System;
using System.Collections.Generic;

namespace Dominican_Hair_Salon.Models;

public partial class TicketDeVenta
{
    public int TicketId { get; set; }

    public int SurcursalId { get; set; }

    public DateTime? Fecha { get; set; }

    public string Empleada { get; set; } = null!;

    public decimal Precio { get; set; }

    public string ClienteNombre { get; set; } = null!;

    public virtual ICollection<RegistroDeVenta> RegistroDeVenta { get; set; } = new List<RegistroDeVenta>();

    public virtual Sucursal Surcursal { get; set; } = null!;

    public virtual Venta? Venta { get; set; }
}
