using System;
using System.Collections.Generic;

namespace Flowboard.Intalio.Context.Models;

public partial class User
{
    public long Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string? MiddleName { get; set; }

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Gender { get; set; } = null!;

    public string? Username { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    public long? PhotoAttachmentId { get; set; }

    public string? HashSalt { get; set; }

    public Guid Identifier { get; set; }

    public string? Password { get; set; }

    public Guid Stamp { get; set; }

    public byte SystemRoleId { get; set; }

    public bool Active { get; set; }

    public bool? Enable2Faauth { get; set; }

    public string? ExternalLoginId { get; set; }

    public byte? LoginProviderId { get; set; }

    public long? ManagerId { get; set; }

    public long? StructureId { get; set; }

    public long? CreatedByUserId { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public bool Activated { get; set; }

    public string? TwoFaAuthKey { get; set; }

    public bool? ForceChangePassword { get; set; }

    public int? FailedLoginCount { get; set; }

    public DateTime? NextAllowedLoginUtc { get; set; }

    public virtual ICollection<AccessToken> AccessTokens { get; set; } = new List<AccessToken>();

    public virtual ICollection<Audit> Audits { get; set; } = new List<Audit>();

    public virtual ICollection<Delegation> DelegationFromUsers { get; set; } = new List<Delegation>();

    public virtual ICollection<Delegation> DelegationToUsers { get; set; } = new List<Delegation>();

    public virtual ICollection<GroupsUser> GroupsUsers { get; set; } = new List<GroupsUser>();

    public virtual ICollection<Identity> Identities { get; set; } = new List<Identity>();

    public virtual ICollection<User> InverseManager { get; set; } = new List<User>();

    public virtual LoginProvider? LoginProvider { get; set; }

    public virtual User? Manager { get; set; }

    public virtual Attachment? PhotoAttachment { get; set; }

    public virtual ICollection<SecretQuestionsUser> SecretQuestionsUsers { get; set; } = new List<SecretQuestionsUser>();

    public virtual ICollection<Structure> Structures { get; set; } = new List<Structure>();

    public virtual ICollection<StructuresUser> StructuresUsers { get; set; } = new List<StructuresUser>();

    public virtual SystemRole SystemRole { get; set; } = null!;

    public virtual ICollection<SystemStructureUserAdmin> SystemStructureUserAdmins { get; set; } = new List<SystemStructureUserAdmin>();

    public virtual ICollection<UserApplicationRole> UserApplicationRoles { get; set; } = new List<UserApplicationRole>();

    public virtual ICollection<UserAttributeValue> UserAttributeValues { get; set; } = new List<UserAttributeValue>();

    public virtual ICollection<UsersAnyType> UsersAnyTypes { get; set; } = new List<UsersAnyType>();
}
