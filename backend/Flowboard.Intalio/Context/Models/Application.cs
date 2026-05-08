using System;
using System.Collections.Generic;

namespace Flowboard.Intalio.Context.Models;

public partial class Application
{
    public short Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Title { get; set; }

    public string ClientId { get; set; } = null!;

    public string ClientSecret { get; set; } = null!;

    public string License { get; set; } = null!;

    public bool Active { get; set; }

    public long CreatedByUserId { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string? ApiScopeName { get; set; }

    public bool? DisablePkce { get; set; }

    public virtual ICollection<AccessToken> AccessTokens { get; set; } = new List<AccessToken>();

    public virtual ICollection<ApplicationPostLogoutRedirectUri> ApplicationPostLogoutRedirectUris { get; set; } = new List<ApplicationPostLogoutRedirectUri>();

    public virtual ICollection<ApplicationRedirectUri> ApplicationRedirectUris { get; set; } = new List<ApplicationRedirectUri>();

    public virtual ICollection<ApplicationStructureAttributeMapping> ApplicationStructureAttributeMappings { get; set; } = new List<ApplicationStructureAttributeMapping>();

    public virtual ICollection<ApplicationStructureVirtualAttributeMapping> ApplicationStructureVirtualAttributeMappings { get; set; } = new List<ApplicationStructureVirtualAttributeMapping>();

    public virtual ICollection<ApplicationUserAttributeMapping> ApplicationUserAttributeMappings { get; set; } = new List<ApplicationUserAttributeMapping>();

    public virtual ICollection<ApplicationUserVirtualAttributeMapping> ApplicationUserVirtualAttributeMappings { get; set; } = new List<ApplicationUserVirtualAttributeMapping>();

    public virtual ICollection<IntegrationUserResourceApplicationRole> IntegrationUserResourceApplicationRoles { get; set; } = new List<IntegrationUserResourceApplicationRole>();

    public virtual ICollection<IntegrationUserResource> IntegrationUserResources { get; set; } = new List<IntegrationUserResource>();

    public virtual ICollection<UserApplicationRole> UserApplicationRoles { get; set; } = new List<UserApplicationRole>();
}
