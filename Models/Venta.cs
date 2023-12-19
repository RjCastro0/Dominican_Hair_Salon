using System;
using System.Collections.Generic;

namespace Dominican_Hair_Salon.Models;

public partial class Venta
{
    public int TicketId { get; set; }

    public int ServicioId { get; set; }

    public virtual Menu Servicio { get; set; } = null!;

    public virtual TicketDeVenta Ticket { get; set; } = null!;
}
