using System;
using System.Collections.Generic;

namespace Flowboard.Intalio.Context.Models;

public partial class LoginProviderType
{
    public byte Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<LoginProvider> LoginProviders { get; set; } = new List<LoginProvider>();
}
