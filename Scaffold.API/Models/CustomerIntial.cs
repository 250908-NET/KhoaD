using System;
using System.Collections.Generic;

namespace Scaffold.API.Models;

public partial class CustomerIntial
{
    public int CustomerId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Initials { get; set; }
}
