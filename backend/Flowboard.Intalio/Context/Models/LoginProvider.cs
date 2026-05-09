using System;
using System.Collections.Generic;

namespace Flowboard.Intalio.Context.Models;

public partial class LoginProvider
{
    public byte Id { get; set; }

    public string Name { get; set; } = null!;

    public string? IconClass { get; set; }

    public string? AuthorityEndpoint { get; set; }

    public string? CallbackPath { get; set; }

    public string? ClientId { get; set; }

    public string? ClientSecret { get; set; }

    public string? Scope { get; set; }

    public bool IsSystem { get; set; }

    public bool Enabled { get; set; }

    public byte LoginProviderTypeId { get; set; }

    public string? Domains { get; set; }

    public bool? IsDefault { get; set; }

    public string? Configuration { get; set; }

    public virtual LoginProviderType LoginProviderType { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
