using System;
using System.Collections.Generic;

namespace Dominican_Hair_Salon.Models;

public partial class RegistroDeVenta
{
    public int VentaId { get; set; }

    public int TicketId { get; set; }

    public decimal Total { get; set; }

    public DateTime Fecha { get; set; }

    public virtual TicketDeVenta Ticket { get; set; } = null!;
}
