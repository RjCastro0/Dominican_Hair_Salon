using System;
using System.Collections.Generic;

namespace Dominican_Hair_Salon.Models;

public partial class Usuario
{
    public int UserId { get; set; }

    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int? Sucursal { get; set; }

    public string Status { get; set; } = null!;

    public string Role { get; set; } = null!;

    public virtual Role RoleNavigation { get; set; } = null!;

    public virtual Sucursal? SucursalNavigation { get; set; }
}
