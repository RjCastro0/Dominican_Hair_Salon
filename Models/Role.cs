using System;
using System.Collections.Generic;

namespace Dominican_Hair_Salon.Models;

public partial class Role
{
    public string RoleName { get; set; } = null!;

    public string Status { get; set; } = null!;

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
