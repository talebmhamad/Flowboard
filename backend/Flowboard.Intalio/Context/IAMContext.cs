using Flowboard.Intalio.Context.Models;
using Microsoft.EntityFrameworkCore;

namespace Flowboard.Intalio.Context;

public partial class IAMContext : DbContext
{
    public IAMContext()
    {
    }

    public IAMContext(DbContextOptions<IAMContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AccessToken> AccessTokens { get; set; }

    public virtual DbSet<AggregatedCounter> AggregatedCounters { get; set; }

    public virtual DbSet<AnyType> AnyTypes { get; set; }

    public virtual DbSet<AnyTypeAttributeValue> AnyTypeAttributeValues { get; set; }

    public virtual DbSet<AnyTypeObject> AnyTypeObjects { get; set; }

    public virtual DbSet<AnyTypeObjectAttribute> AnyTypeObjectAttributes { get; set; }

    public virtual DbSet<AnyTypeObjectVirtualAttribute> AnyTypeObjectVirtualAttributes { get; set; }

    public virtual DbSet<AnyTypeObjectVirtualAttributesAnyTypeObjectAttribute> AnyTypeObjectVirtualAttributesAnyTypeObjectAttributes { get; set; }

    public virtual DbSet<Flowboard.Intalio.Context.Models.Application> Applications { get; set; }

    public virtual DbSet<ApplicationPostLogoutRedirectUri> ApplicationPostLogoutRedirectUris { get; set; }

    public virtual DbSet<ApplicationRedirectUri> ApplicationRedirectUris { get; set; }

    public virtual DbSet<ApplicationServer> ApplicationServers { get; set; }

    public virtual DbSet<ApplicationStructureAttributeMapping> ApplicationStructureAttributeMappings { get; set; }

    public virtual DbSet<ApplicationStructureVirtualAttributeMapping> ApplicationStructureVirtualAttributeMappings { get; set; }

    public virtual DbSet<ApplicationUserAttributeMapping> ApplicationUserAttributeMappings { get; set; }

    public virtual DbSet<ApplicationUserVirtualAttributeMapping> ApplicationUserVirtualAttributeMappings { get; set; }

    public virtual DbSet<Attachment> Attachments { get; set; }

    public virtual DbSet<AttachmentDatum> AttachmentData { get; set; }

    public virtual DbSet<Flowboard.Intalio.Context.Models.Attribute> Attributes { get; set; }

    public virtual DbSet<AttributeType> AttributeTypes { get; set; }

    public virtual DbSet<AttributeValidator> AttributeValidators { get; set; }

    public virtual DbSet<Audit> Audits { get; set; }

    public virtual DbSet<Counter> Counters { get; set; }

    public virtual DbSet<CustomGrantType> CustomGrantTypes { get; set; }

    public virtual DbSet<CustomizationFile> CustomizationFiles { get; set; }

    public virtual DbSet<Delegation> Delegations { get; set; }

    public virtual DbSet<ExceptionLog> ExceptionLogs { get; set; }

    public virtual DbSet<Group> Groups { get; set; }

    public virtual DbSet<GroupsUser> GroupsUsers { get; set; }

    public virtual DbSet<Hash> Hashes { get; set; }

    public virtual DbSet<Identity> Identities { get; set; }

    public virtual DbSet<IntegrationAnyTypeObjectResource> IntegrationAnyTypeObjectResources { get; set; }

    public virtual DbSet<IntegrationAnyTypeObjectResourceAttribute> IntegrationAnyTypeObjectResourceAttributes { get; set; }

    public virtual DbSet<IntegrationDataSource> IntegrationDataSources { get; set; }

    public virtual DbSet<IntegrationDataSourceProperty> IntegrationDataSourceProperties { get; set; }

    public virtual DbSet<IntegrationItem> IntegrationItems { get; set; }

    public virtual DbSet<IntegrationItemDataSourceProperty> IntegrationItemDataSourceProperties { get; set; }

    public virtual DbSet<IntegrationStructureResource> IntegrationStructureResources { get; set; }

    public virtual DbSet<IntegrationStructureResourceAttribute> IntegrationStructureResourceAttributes { get; set; }

    public virtual DbSet<IntegrationStructureResourceStructure> IntegrationStructureResourceStructures { get; set; }

    public virtual DbSet<IntegrationType> IntegrationTypes { get; set; }

    public virtual DbSet<IntegrationUserResource> IntegrationUserResources { get; set; }

    public virtual DbSet<IntegrationUserResourceApplicationRole> IntegrationUserResourceApplicationRoles { get; set; }

    public virtual DbSet<IntegrationUserResourceAttribute> IntegrationUserResourceAttributes { get; set; }

    public virtual DbSet<Job> Jobs { get; set; }

    public virtual DbSet<JobParameter> JobParameters { get; set; }

    public virtual DbSet<JobQueue> JobQueues { get; set; }

    public virtual DbSet<JobsSchedule> JobsSchedules { get; set; }

    public virtual DbSet<JobsSequence> JobsSequences { get; set; }

    public virtual DbSet<Language> Languages { get; set; }

    public virtual DbSet<License> Licenses { get; set; }

    public virtual DbSet<List> Lists { get; set; }

    public virtual DbSet<LoginProvider> LoginProviders { get; set; }

    public virtual DbSet<LoginProviderType> LoginProviderTypes { get; set; }

    public virtual DbSet<NotificationTemplate> NotificationTemplates { get; set; }

    public virtual DbSet<Parameter> Parameters { get; set; }

    public virtual DbSet<Privilege> Privileges { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<RolesPrivilege> RolesPrivileges { get; set; }

    public virtual DbSet<Schema> Schemas { get; set; }

    public virtual DbSet<SecretQuestion> SecretQuestions { get; set; }

    public virtual DbSet<SecretQuestionsUser> SecretQuestionsUsers { get; set; }

    public virtual DbSet<Server> Servers { get; set; }

    public virtual DbSet<Set> Sets { get; set; }

    public virtual DbSet<State> States { get; set; }

    public virtual DbSet<Structure> Structures { get; set; }

    public virtual DbSet<StructureAttribute> StructureAttributes { get; set; }

    public virtual DbSet<StructureAttributeValue> StructureAttributeValues { get; set; }

    public virtual DbSet<StructureVirtualAttribute> StructureVirtualAttributes { get; set; }

    public virtual DbSet<StructureVirtualAttributesStructureAttribute> StructureVirtualAttributesStructureAttributes { get; set; }

    public virtual DbSet<StructuresUser> StructuresUsers { get; set; }

    public virtual DbSet<SystemRole> SystemRoles { get; set; }

    public virtual DbSet<SystemStructureUserAdmin> SystemStructureUserAdmins { get; set; }

    public virtual DbSet<TranslatorDictionary> TranslatorDictionaries { get; set; }

    public virtual DbSet<TwoFactorAuthenticationProvider> TwoFactorAuthenticationProviders { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserApplicationRole> UserApplicationRoles { get; set; }

    public virtual DbSet<UserAttribute> UserAttributes { get; set; }

    public virtual DbSet<UserAttributeValue> UserAttributeValues { get; set; }

    public virtual DbSet<UserType> UserTypes { get; set; }

    public virtual DbSet<UserVirtualAttribute> UserVirtualAttributes { get; set; }

    public virtual DbSet<UserVirtualAttributesUserAttribute> UserVirtualAttributesUserAttributes { get; set; }

    public virtual DbSet<UsersAnyType> UsersAnyTypes { get; set; }

    public virtual DbSet<Validator> Validators { get; set; }

    public virtual DbSet<ValidatorProperty> ValidatorProperties { get; set; }

    public virtual DbSet<ValidatorsAttributeType> ValidatorsAttributeTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=MT\\SQLEXPRESS;Database=IAM;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AccessToken>(entity =>
        {
            entity.ToTable("AccessToken");

            entity.HasIndex(e => e.ApplicationId, "IX_AccessToken_ApplicationId");

            entity.HasIndex(e => e.UserId, "IX_AccessToken_UserId");

            entity.Property(e => e.Key).HasMaxLength(600);
            entity.Property(e => e.SessionId).HasMaxLength(600);
            entity.Property(e => e.Type).HasMaxLength(50);

            entity.HasOne(d => d.Application).WithMany(p => p.AccessTokens)
                .HasForeignKey(d => d.ApplicationId)
                .HasConstraintName("FK_AccessToken_Application");

            entity.HasOne(d => d.User).WithMany(p => p.AccessTokens)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_AccessToken_User");
        });

        modelBuilder.Entity<AggregatedCounter>(entity =>
        {
            entity.HasKey(e => e.Key).HasName("PK_HangFire_CounterAggregated");

            entity.ToTable("AggregatedCounter", "Scheduler");

            entity.HasIndex(e => e.ExpireAt, "IX_HangFire_AggregatedCounter_ExpireAt").HasFilter("([ExpireAt] IS NOT NULL)");

            entity.Property(e => e.Key).HasMaxLength(100);
            entity.Property(e => e.ExpireAt).HasColumnType("datetime");
        });

        modelBuilder.Entity<AnyType>(entity =>
        {
            entity.ToTable("AnyType");

            entity.HasIndex(e => e.AnyTypeObjectId, "IX_AnyType_AnyTypeObjectId");

            entity.Property(e => e.Name).HasMaxLength(150);

            entity.HasOne(d => d.AnyTypeObject).WithMany(p => p.AnyTypes)
                .HasForeignKey(d => d.AnyTypeObjectId)
                .HasConstraintName("FK_AnyType_AnyTypeObject");
        });

        modelBuilder.Entity<AnyTypeAttributeValue>(entity =>
        {
            entity.ToTable("AnyTypeAttributeValue");

            entity.HasIndex(e => e.AnyTypeId, "IX_AnyTypeAttributeValue_AnyTypeId");

            entity.HasIndex(e => e.AttributeId, "IX_AnyTypeAttributeValue_AttributeId");

            entity.HasOne(d => d.AnyType).WithMany(p => p.AnyTypeAttributeValues)
                .HasForeignKey(d => d.AnyTypeId)
                .HasConstraintName("FK_AnyTypeAttributeValue_AnyType");

            entity.HasOne(d => d.Attribute).WithMany(p => p.AnyTypeAttributeValues)
                .HasForeignKey(d => d.AttributeId)
                .HasConstraintName("FK_AnyTypeAttributeValue_Attribute");
        });

        modelBuilder.Entity<AnyTypeObject>(entity =>
        {
            entity.ToTable("AnyTypeObject");

            entity.Property(e => e.Name).HasMaxLength(150);
        });

        modelBuilder.Entity<AnyTypeObjectAttribute>(entity =>
        {
            entity.ToTable("AnyTypeObjectAttribute");

            entity.HasIndex(e => e.AnyTypeObjectId, "IX_AnyTypeObjectAttribute_AnyTypeObjectId");

            entity.HasIndex(e => e.AttributeId, "IX_AnyTypeObjectAttribute_AttributeId");

            entity.HasOne(d => d.AnyTypeObject).WithMany(p => p.AnyTypeObjectAttributes)
                .HasForeignKey(d => d.AnyTypeObjectId)
                .HasConstraintName("FK_AnyTypeObjectAttribute_AnyTypeObject");

            entity.HasOne(d => d.Attribute).WithMany(p => p.AnyTypeObjectAttributes)
                .HasForeignKey(d => d.AttributeId)
                .HasConstraintName("FK_AnyTypeObjectAttribute_Attribute");
        });

        modelBuilder.Entity<AnyTypeObjectVirtualAttribute>(entity =>
        {
            entity.ToTable("AnyTypeObjectVirtualAttribute");

            entity.HasIndex(e => e.AnyTypeObjectId, "IX_AnyTypeObjectVirtualAttribute_AnyTypeObjectId");

            entity.Property(e => e.Name).HasMaxLength(150);
            entity.Property(e => e.Separator).HasMaxLength(10);

            entity.HasOne(d => d.AnyTypeObject).WithMany(p => p.AnyTypeObjectVirtualAttributes)
                .HasForeignKey(d => d.AnyTypeObjectId)
                .HasConstraintName("FK_AnyTypeObjectVirtualAttribute_AnyTypeObject");
        });

        modelBuilder.Entity<AnyTypeObjectVirtualAttributesAnyTypeObjectAttribute>(entity =>
        {
            entity.HasIndex(e => e.AnyTypeObjectAttributeId, "IX_AnyTypeObjectVirtualAttributesAnyTypeObjectAttributes_AnyTypeObjectAttributeId");

            entity.HasIndex(e => e.AnyTypeObjectVirtualAttributeId, "IX_AnyTypeObjectVirtualAttributesAnyTypeObjectAttributes_AnyTypeObjectVirtualAttributeId");

            entity.HasOne(d => d.AnyTypeObjectAttribute).WithMany(p => p.AnyTypeObjectVirtualAttributesAnyTypeObjectAttributes)
                .HasForeignKey(d => d.AnyTypeObjectAttributeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AnyObjectVRAnyTypeObjectAttributes_AnyObjectAttribute");

            entity.HasOne(d => d.AnyTypeObjectVirtualAttribute).WithMany(p => p.AnyTypeObjectVirtualAttributesAnyTypeObjectAttributes)
                .HasForeignKey(d => d.AnyTypeObjectVirtualAttributeId)
                .HasConstraintName("FK_AnyObjectVRAnyTypeObjectAttributes_AnyObjectVirtualAttribute");
        });

        modelBuilder.Entity<Flowboard.Intalio.Context.Models.Application>(entity =>
        {
            entity.ToTable("Application");

            entity.HasIndex(e => e.Name, "IX_Application").IsUnique();

            entity.Property(e => e.ApiScopeName).HasMaxLength(50);
            entity.Property(e => e.ClientId).HasMaxLength(550);
            entity.Property(e => e.ClientSecret).HasMaxLength(550);
            entity.Property(e => e.DisablePkce).HasColumnName("DisablePKCE");
            entity.Property(e => e.License).HasMaxLength(1000);
            entity.Property(e => e.Name).HasMaxLength(150);
            entity.Property(e => e.Title).HasMaxLength(150);
        });

        modelBuilder.Entity<ApplicationPostLogoutRedirectUri>(entity =>
        {
            entity.HasIndex(e => e.ApplicationId, "IX_ApplicationPostLogoutRedirectUris_ApplicationId");

            entity.Property(e => e.PostLogoutRedirectUri).HasMaxLength(250);

            entity.HasOne(d => d.Application).WithMany(p => p.ApplicationPostLogoutRedirectUris)
                .HasForeignKey(d => d.ApplicationId)
                .HasConstraintName("FK_ApplicationPostLogoutRedirectUris_Application");
        });

        modelBuilder.Entity<ApplicationRedirectUri>(entity =>
        {
            entity.HasIndex(e => e.ApplicationId, "IX_ApplicationRedirectUris_ApplicationId");

            entity.Property(e => e.RedirectUri).HasMaxLength(250);

            entity.HasOne(d => d.Application).WithMany(p => p.ApplicationRedirectUris)
                .HasForeignKey(d => d.ApplicationId)
                .HasConstraintName("FK_ApplicationRedirectUris_Application");
        });

        modelBuilder.Entity<ApplicationServer>(entity =>
        {
            entity.ToTable("ApplicationServer");

            entity.Property(e => e.ServerName).HasMaxLength(150);
            entity.Property(e => e.Url)
                .HasMaxLength(500)
                .HasColumnName("URL");
        });

        modelBuilder.Entity<ApplicationStructureAttributeMapping>(entity =>
        {
            entity.ToTable("ApplicationStructureAttributeMapping");

            entity.HasIndex(e => e.ApplicationId, "IX_ApplicationStructureAttributeMapping_ApplicationId");

            entity.HasIndex(e => e.LanguageId, "IX_ApplicationStructureAttributeMapping_LanguageId");

            entity.HasIndex(e => e.StructureAttributeId, "IX_ApplicationStructureAttributeMapping_StructureAttributeId");

            entity.Property(e => e.MappingName).HasMaxLength(150);

            entity.HasOne(d => d.Application).WithMany(p => p.ApplicationStructureAttributeMappings)
                .HasForeignKey(d => d.ApplicationId)
                .HasConstraintName("FK_ApplicationStructureAttributeMapping_Application");

            entity.HasOne(d => d.Language).WithMany(p => p.ApplicationStructureAttributeMappings)
                .HasForeignKey(d => d.LanguageId)
                .HasConstraintName("FK_ApplicationStructureAttributeMapping_Language");

            entity.HasOne(d => d.StructureAttribute).WithMany(p => p.ApplicationStructureAttributeMappings)
                .HasForeignKey(d => d.StructureAttributeId)
                .HasConstraintName("FK_ApplicationStructureAttributeMapping_StructureAttribute");
        });

        modelBuilder.Entity<ApplicationStructureVirtualAttributeMapping>(entity =>
        {
            entity.ToTable("ApplicationStructureVirtualAttributeMapping");

            entity.HasIndex(e => e.ApplicationId, "IX_ApplicationStructureVirtualAttributeMapping_ApplicationId");

            entity.HasIndex(e => e.LanguageId, "IX_ApplicationStructureVirtualAttributeMapping_LanguageId");

            entity.HasIndex(e => e.StructureVirtualAttributeId, "IX_ApplicationStructureVirtualAttributeMapping_StructureVirtualAttributeId");

            entity.Property(e => e.MappingName).HasMaxLength(150);

            entity.HasOne(d => d.Application).WithMany(p => p.ApplicationStructureVirtualAttributeMappings).HasForeignKey(d => d.ApplicationId);

            entity.HasOne(d => d.Language).WithMany(p => p.ApplicationStructureVirtualAttributeMappings)
                .HasForeignKey(d => d.LanguageId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_ApplicationStructureVirtualAttributeMapping_Language");

            entity.HasOne(d => d.StructureVirtualAttribute).WithMany(p => p.ApplicationStructureVirtualAttributeMappings)
                .HasForeignKey(d => d.StructureVirtualAttributeId)
                .HasConstraintName("FK_ApplicationStructureVirtualAttributeMapping_StructureVirtualAttribute");
        });

        modelBuilder.Entity<ApplicationUserAttributeMapping>(entity =>
        {
            entity.ToTable("ApplicationUserAttributeMapping");

            entity.HasIndex(e => e.ApplicationId, "IX_ApplicationUserAttributeMapping_ApplicationId");

            entity.HasIndex(e => e.LanguageId, "IX_ApplicationUserAttributeMapping_LanguageId");

            entity.HasIndex(e => e.UserAttributeId, "IX_ApplicationUserAttributeMapping_UserAttributeId");

            entity.Property(e => e.MappingName).HasMaxLength(150);

            entity.HasOne(d => d.Application).WithMany(p => p.ApplicationUserAttributeMappings)
                .HasForeignKey(d => d.ApplicationId)
                .HasConstraintName("FK_ApplicationUserAttributeMapping_Application");

            entity.HasOne(d => d.Language).WithMany(p => p.ApplicationUserAttributeMappings)
                .HasForeignKey(d => d.LanguageId)
                .HasConstraintName("FK_ApplicationUserAttributeMapping_Language");

            entity.HasOne(d => d.UserAttribute).WithMany(p => p.ApplicationUserAttributeMappings)
                .HasForeignKey(d => d.UserAttributeId)
                .HasConstraintName("FK_ApplicationUserAttributeMapping_UserAttribute");
        });

        modelBuilder.Entity<ApplicationUserVirtualAttributeMapping>(entity =>
        {
            entity.ToTable("ApplicationUserVirtualAttributeMapping");

            entity.HasIndex(e => e.ApplicationId, "IX_ApplicationUserVirtualAttributeMapping_ApplicationId");

            entity.HasIndex(e => e.LanguageId, "IX_ApplicationUserVirtualAttributeMapping_LanguageId");

            entity.HasIndex(e => e.UserVirtualAttributeId, "IX_ApplicationUserVirtualAttributeMapping_UserVirtualAttributeId");

            entity.Property(e => e.MappingName).HasMaxLength(150);

            entity.HasOne(d => d.Application).WithMany(p => p.ApplicationUserVirtualAttributeMappings)
                .HasForeignKey(d => d.ApplicationId)
                .HasConstraintName("FK_ApplicationUserVirtualAttributeMapping_Application");

            entity.HasOne(d => d.Language).WithMany(p => p.ApplicationUserVirtualAttributeMappings)
                .HasForeignKey(d => d.LanguageId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_ApplicationUserVirtualAttributeMapping_Language");

            entity.HasOne(d => d.UserVirtualAttribute).WithMany(p => p.ApplicationUserVirtualAttributeMappings)
                .HasForeignKey(d => d.UserVirtualAttributeId)
                .HasConstraintName("FK_ApplicationUserVirtualAttributeMapping_UserVirtualAttribute");
        });

        modelBuilder.Entity<Attachment>(entity =>
        {
            entity.ToTable("Attachment");

            entity.HasIndex(e => e.AttachmentDataId, "IX_Attachment_AttachmentDataId");

            entity.Property(e => e.ContentType).HasMaxLength(150);
            entity.Property(e => e.Extension).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(100);

            entity.HasOne(d => d.AttachmentData).WithMany(p => p.Attachments)
                .HasForeignKey(d => d.AttachmentDataId)
                .HasConstraintName("FK_Attachment_AttachmentData");
        });

        modelBuilder.Entity<Flowboard.Intalio.Context.Models.Attribute>(entity =>
        {
            entity.ToTable("Attribute");

            entity.HasIndex(e => e.AttributeTypeId, "IX_Attribute_AttributeTypeId");

            entity.HasIndex(e => e.Name, "IX_Attribute_Name_Unique").IsUnique();

            entity.Property(e => e.Group).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(150);

            entity.HasOne(d => d.AttributeType).WithMany(p => p.Attributes)
                .HasForeignKey(d => d.AttributeTypeId)
                .HasConstraintName("FK_Attribute_AttributeType");
        });

        modelBuilder.Entity<AttributeType>(entity =>
        {
            entity.ToTable("AttributeType");

            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Type).HasMaxLength(50);
        });

        modelBuilder.Entity<AttributeValidator>(entity =>
        {
            entity.ToTable("AttributeValidator");

            entity.HasIndex(e => e.AttributeId, "IX_AttributeValidator_AttributeId");

            entity.HasIndex(e => e.ValidatorId, "IX_AttributeValidator_ValidatorId");

            entity.HasIndex(e => e.ValidatorPropertyId, "IX_AttributeValidator_ValidatorPropertyId");

            entity.Property(e => e.Value).HasMaxLength(550);

            entity.HasOne(d => d.Attribute).WithMany(p => p.AttributeValidators)
                .HasForeignKey(d => d.AttributeId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_AttributeValidator_Attribute");

            entity.HasOne(d => d.Validator).WithMany(p => p.AttributeValidators)
                .HasForeignKey(d => d.ValidatorId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_AttributeValidator_Validator");

            entity.HasOne(d => d.ValidatorProperty).WithMany(p => p.AttributeValidators)
                .HasForeignKey(d => d.ValidatorPropertyId)
                .HasConstraintName("FK_AttributeValidator_ValidatorProperty");
        });

        modelBuilder.Entity<Audit>(entity =>
        {
            entity.ToTable("Audit");

            entity.HasIndex(e => e.CreatedByUserId, "IX_Audit_CreatedByUserId");

            entity.HasOne(d => d.CreatedByUser).WithMany(p => p.Audits)
                .HasForeignKey(d => d.CreatedByUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Audit_User");
        });

        modelBuilder.Entity<Counter>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Counter", "Scheduler");

            entity.HasIndex(e => e.Key, "CX_HangFire_Counter").IsClustered();

            entity.Property(e => e.ExpireAt).HasColumnType("datetime");
            entity.Property(e => e.Key).HasMaxLength(100);
        });

        modelBuilder.Entity<CustomGrantType>(entity =>
        {
            entity.ToTable("CustomGrantType");

            entity.Property(e => e.AssemblyFullQualifiedName).HasMaxLength(450);
            entity.Property(e => e.ClassFileName).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(150);
        });

        modelBuilder.Entity<CustomizationFile>(entity =>
        {
            entity.ToTable("CustomizationFile");

            entity.Property(e => e.AssemblyFullQualifiedName).HasMaxLength(750);
            entity.Property(e => e.Name).HasMaxLength(150);
        });

        modelBuilder.Entity<Delegation>(entity =>
        {
            entity.ToTable("Delegation");

            entity.HasIndex(e => e.FromUserId, "IX_Delegation_FromUserId");

            entity.HasIndex(e => e.ToUserId, "IX_Delegation_ToUserId");

            entity.HasOne(d => d.FromUser).WithMany(p => p.DelegationFromUsers)
                .HasForeignKey(d => d.FromUserId)
                .HasConstraintName("FK_Delegation_User");

            entity.HasOne(d => d.ToUser).WithMany(p => p.DelegationToUsers)
                .HasForeignKey(d => d.ToUserId)
                .HasConstraintName("FK_Delegation_User1");
        });

        modelBuilder.Entity<ExceptionLog>(entity =>
        {
            entity.ToTable("ExceptionLog");

            entity.Property(e => e.Level).HasMaxLength(50);
            entity.Property(e => e.MachineName).HasMaxLength(150);
        });

        modelBuilder.Entity<Group>(entity =>
        {
            entity.ToTable("Group");

            entity.Property(e => e.Name).HasMaxLength(150);
        });

        modelBuilder.Entity<GroupsUser>(entity =>
        {
            entity.HasIndex(e => e.GroupId, "IX_GroupsUsers_GroupId");

            entity.HasIndex(e => e.UserId, "IX_GroupsUsers_UserId");

            entity.HasOne(d => d.Group).WithMany(p => p.GroupsUsers)
                .HasForeignKey(d => d.GroupId)
                .HasConstraintName("FK_GroupsUsers_Group");

            entity.HasOne(d => d.User).WithMany(p => p.GroupsUsers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_GroupsUsers_User");
        });

        modelBuilder.Entity<Hash>(entity =>
        {
            entity.HasKey(e => new { e.Key, e.Field }).HasName("PK_HangFire_Hash");

            entity.ToTable("Hash", "Scheduler");

            entity.HasIndex(e => e.ExpireAt, "IX_HangFire_Hash_ExpireAt").HasFilter("([ExpireAt] IS NOT NULL)");

            entity.Property(e => e.Key).HasMaxLength(100);
            entity.Property(e => e.Field).HasMaxLength(100);
        });

        modelBuilder.Entity<Identity>(entity =>
        {
            entity.ToTable("Identity");

            entity.HasIndex(e => e.AnyTypeId, "IX_Identity_AnyTypeId");

            entity.HasIndex(e => e.StructureId, "IX_Identity_StructureId");

            entity.HasIndex(e => e.UserId, "IX_Identity_UserId");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.AnyType).WithMany(p => p.Identities)
                .HasForeignKey(d => d.AnyTypeId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Identity_AnyType");

            entity.HasOne(d => d.Structure).WithMany(p => p.Identities)
                .HasForeignKey(d => d.StructureId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Identity_Structure");

            entity.HasOne(d => d.User).WithMany(p => p.Identities)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Identity_User");
        });

        modelBuilder.Entity<IntegrationAnyTypeObjectResource>(entity =>
        {
            entity.ToTable("IntegrationAnyTypeObjectResource");

            entity.HasIndex(e => e.IntegrationItemId, "IX_IntegrationAnyTypeObjectResource_IntegrationItemId");

            entity.HasIndex(e => e.IntegrationTypeId, "IX_IntegrationAnyTypeObjectResource_IntegrationTypeId");

            entity.Property(e => e.Name).HasMaxLength(150);

            entity.HasOne(d => d.IntegrationItem).WithMany(p => p.IntegrationAnyTypeObjectResources)
                .HasForeignKey(d => d.IntegrationItemId)
                .HasConstraintName("FK_IntegrationAnyTypeObjectResource_IntegrationItem");

            entity.HasOne(d => d.IntegrationType).WithMany(p => p.IntegrationAnyTypeObjectResources)
                .HasForeignKey(d => d.IntegrationTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_IntegrationAnyTypeObjectResource_IntegrationType");
        });

        modelBuilder.Entity<IntegrationAnyTypeObjectResourceAttribute>(entity =>
        {
            entity.HasIndex(e => e.AnyTypeObjectAttributeId, "IX_IntegrationAnyTypeObjectResourceAttributes_AnyTypeObjectAttributeId");

            entity.HasIndex(e => e.IntegrationAnyTypeObjectResourceId, "IX_IntegrationAnyTypeObjectResourceAttributes_IntegrationAnyTypeObjectResourceId");

            entity.Property(e => e.MappingName).HasMaxLength(150);

            entity.HasOne(d => d.AnyTypeObjectAttribute).WithMany(p => p.IntegrationAnyTypeObjectResourceAttributes)
                .HasForeignKey(d => d.AnyTypeObjectAttributeId)
                .HasConstraintName("FK_IntegrationAnyTypeObjectResourceAttributes_AnyTypeObjectAttribute");

            entity.HasOne(d => d.IntegrationAnyTypeObjectResource).WithMany(p => p.IntegrationAnyTypeObjectResourceAttributes)
                .HasForeignKey(d => d.IntegrationAnyTypeObjectResourceId)
                .HasConstraintName("FK_IntegATypeObjectResourceAttr_IntegATypeObjectResource");
        });

        modelBuilder.Entity<IntegrationDataSource>(entity =>
        {
            entity.ToTable("IntegrationDataSource");

            entity.Property(e => e.AssemblyFullQualifiedNameStructurePull).HasMaxLength(750);
            entity.Property(e => e.AssemblyFullQualifiedNameStructurePush).HasMaxLength(750);
            entity.Property(e => e.AssemblyFullQualifiedNameUserPull).HasMaxLength(750);
            entity.Property(e => e.AssemblyFullQualifiedNameUserPush).HasMaxLength(750);
            entity.Property(e => e.ClassFileNameStructurePull).HasMaxLength(50);
            entity.Property(e => e.ClassFileNameStructurePush).HasMaxLength(50);
            entity.Property(e => e.ClassFileNameUserPull).HasMaxLength(50);
            entity.Property(e => e.ClassFileNameUserPush).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(150);
        });

        modelBuilder.Entity<IntegrationDataSourceProperty>(entity =>
        {
            entity.ToTable("IntegrationDataSourceProperty");

            entity.HasIndex(e => e.IntegrationDataSourceId, "IX_IntegrationDataSourceProperty_IntegrationDataSourceId");

            entity.Property(e => e.DefaultValue).HasMaxLength(550);
            entity.Property(e => e.Description).HasMaxLength(550);
            entity.Property(e => e.Name).HasMaxLength(150);

            entity.HasOne(d => d.IntegrationDataSource).WithMany(p => p.IntegrationDataSourceProperties)
                .HasForeignKey(d => d.IntegrationDataSourceId)
                .HasConstraintName("FK_IntegrationDataSourceProperty_IntegrationDataSource");
        });

        modelBuilder.Entity<IntegrationItem>(entity =>
        {
            entity.ToTable("IntegrationItem");

            entity.HasIndex(e => e.IntegrationDataSourceId, "IX_IntegrationItem_IntegrationDataSourceId");

            entity.Property(e => e.PrimaryKey).HasMaxLength(150);

            entity.HasOne(d => d.IntegrationDataSource).WithMany(p => p.IntegrationItems)
                .HasForeignKey(d => d.IntegrationDataSourceId)
                .HasConstraintName("FK_IntegrationItem_IntegrationDataSource");
        });

        modelBuilder.Entity<IntegrationItemDataSourceProperty>(entity =>
        {
            entity.HasIndex(e => e.IntegrationDataSourcePropertyId, "IX_IntegrationItemDataSourceProperties_IntegrationDataSourcePropertyId");

            entity.HasIndex(e => e.IntegrationItemId, "IX_IntegrationItemDataSourceProperties_IntegrationItemId");

            entity.Property(e => e.Value).HasMaxLength(550);

            entity.HasOne(d => d.IntegrationDataSourceProperty).WithMany(p => p.IntegrationItemDataSourceProperties)
                .HasForeignKey(d => d.IntegrationDataSourcePropertyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_IntegrationItemDataSourceProperties_IntegrationDataSourceProperty");

            entity.HasOne(d => d.IntegrationItem).WithMany(p => p.IntegrationItemDataSourceProperties)
                .HasForeignKey(d => d.IntegrationItemId)
                .HasConstraintName("FK_IntegrationItemDataSourceProperties_IntegrationItem");
        });

        modelBuilder.Entity<IntegrationStructureResource>(entity =>
        {
            entity.ToTable("IntegrationStructureResource");

            entity.HasIndex(e => e.IntegrationItemId, "IX_IntegrationStructureResource_IntegrationItemId");

            entity.HasIndex(e => e.IntegrationTypeId, "IX_IntegrationStructureResource_IntegrationTypeId");

            entity.HasIndex(e => e.ScheduleId, "IX_IntegrationStructureResource_ScheduleId");

            entity.Property(e => e.Name).HasMaxLength(150);

            entity.HasOne(d => d.IntegrationItem).WithMany(p => p.IntegrationStructureResources)
                .HasForeignKey(d => d.IntegrationItemId)
                .HasConstraintName("FK_IntegrationStructureResource_IntegrationItem");

            entity.HasOne(d => d.IntegrationType).WithMany(p => p.IntegrationStructureResources)
                .HasForeignKey(d => d.IntegrationTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_IntegrationStructureResource_IntegrationType");

            entity.HasOne(d => d.Schedule).WithMany(p => p.IntegrationStructureResources)
                .HasForeignKey(d => d.ScheduleId)
                .HasConstraintName("FK_IntegrationStructureResource_JobsSchedule");
        });

        modelBuilder.Entity<IntegrationStructureResourceAttribute>(entity =>
        {
            entity.HasIndex(e => e.IntegrationStructureResourceId, "IX_IntegrationStructureResourceAttributes_IntegrationStructureResourceId");

            entity.HasIndex(e => e.StructureAttributeId, "IX_IntegrationStructureResourceAttributes_StructureAttributeId");

            entity.Property(e => e.MappingName).HasMaxLength(150);
            entity.Property(e => e.Name).HasMaxLength(150);

            entity.HasOne(d => d.IntegrationStructureResource).WithMany(p => p.IntegrationStructureResourceAttributes)
                .HasForeignKey(d => d.IntegrationStructureResourceId)
                .HasConstraintName("FK_IntegrationStructureResourceAttributes_IntegrationStructureResource");

            entity.HasOne(d => d.StructureAttribute).WithMany(p => p.IntegrationStructureResourceAttributes)
                .HasForeignKey(d => d.StructureAttributeId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_IntegrationStructureResourceAttributes_StructureAttribute");
        });

        modelBuilder.Entity<IntegrationStructureResourceStructure>(entity =>
        {
            entity.HasIndex(e => e.IntegrationStructureResourceId, "IX_IntegrationStructureResourceStructures_IntegrationStructureResourceId");

            entity.HasIndex(e => e.StructureId, "IX_IntegrationStructureResourceStructures_StructureId");

            entity.HasOne(d => d.IntegrationStructureResource).WithMany(p => p.IntegrationStructureResourceStructures)
                .HasForeignKey(d => d.IntegrationStructureResourceId)
                .HasConstraintName("FK_IntegrationStructureResourceStructures_IntegrationStructureResource");

            entity.HasOne(d => d.Structure).WithMany(p => p.IntegrationStructureResourceStructures)
                .HasForeignKey(d => d.StructureId)
                .HasConstraintName("FK_IntegrationStructureResourceStructures_Structure");
        });

        modelBuilder.Entity<IntegrationType>(entity =>
        {
            entity.ToTable("IntegrationType");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<IntegrationUserResource>(entity =>
        {
            entity.ToTable("IntegrationUserResource");

            entity.HasIndex(e => e.ApplicationId, "IX_IntegrationUserResource_ApplicationId");

            entity.HasIndex(e => e.IntegrationItemId, "IX_IntegrationUserResource_IntegrationItemId");

            entity.HasIndex(e => e.IntegrationTypeId, "IX_IntegrationUserResource_IntegrationTypeId");

            entity.HasIndex(e => e.ScheduleId, "IX_IntegrationUserResource_ScheduleId");

            entity.Property(e => e.Name).HasMaxLength(150);

            entity.HasOne(d => d.Application).WithMany(p => p.IntegrationUserResources)
                .HasForeignKey(d => d.ApplicationId)
                .HasConstraintName("FK_IntegrationUserResource_Application");

            entity.HasOne(d => d.IntegrationItem).WithMany(p => p.IntegrationUserResources)
                .HasForeignKey(d => d.IntegrationItemId)
                .HasConstraintName("FK_IntegrationUserResource_IntegrationItem");

            entity.HasOne(d => d.IntegrationType).WithMany(p => p.IntegrationUserResources)
                .HasForeignKey(d => d.IntegrationTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_IntegrationUserResource_IntegrationType");

            entity.HasOne(d => d.Schedule).WithMany(p => p.IntegrationUserResources)
                .HasForeignKey(d => d.ScheduleId)
                .HasConstraintName("FK_IntegrationUserResource_JobsSchedule");
        });

        modelBuilder.Entity<IntegrationUserResourceApplicationRole>(entity =>
        {
            entity.ToTable("IntegrationUserResourceApplicationRole");

            entity.HasIndex(e => e.ApplicationId, "IX_IntegrationUserResourceApplicationRole_ApplicationId");

            entity.HasIndex(e => e.IntegrationUserResourceId, "IX_IntegrationUserResourceApplicationRole_IntegrationUserResourceId");

            entity.HasIndex(e => e.RoleId, "IX_IntegrationUserResourceApplicationRole_RoleId");

            entity.HasIndex(e => e.UserTypeId, "IX_IntegrationUserResourceApplicationRole_UserTypeId");

            entity.HasOne(d => d.Application).WithMany(p => p.IntegrationUserResourceApplicationRoles)
                .HasForeignKey(d => d.ApplicationId)
                .HasConstraintName("FK_IntegrationUserResourceApplicationRole_Application");

            entity.HasOne(d => d.IntegrationUserResource).WithMany(p => p.IntegrationUserResourceApplicationRoles)
                .HasForeignKey(d => d.IntegrationUserResourceId)
                .HasConstraintName("FK_IntegrationUserResourceApplicationRole_IntegrationUserResource");

            entity.HasOne(d => d.Role).WithMany(p => p.IntegrationUserResourceApplicationRoles)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK_IntegrationUserResourceApplicationRole_Role");

            entity.HasOne(d => d.UserType).WithMany(p => p.IntegrationUserResourceApplicationRoles)
                .HasForeignKey(d => d.UserTypeId)
                .HasConstraintName("FK_IntegrationUserResourceApplicationRole_UserType");
        });

        modelBuilder.Entity<IntegrationUserResourceAttribute>(entity =>
        {
            entity.HasIndex(e => e.IntegrationUserResourceId, "IX_IntegrationUserResourceAttributes_IntegrationUserResourceId");

            entity.HasIndex(e => e.UserAttributeId, "IX_IntegrationUserResourceAttributes_UserAttributeId");

            entity.Property(e => e.MappingName).HasMaxLength(150);
            entity.Property(e => e.Name).HasMaxLength(150);

            entity.HasOne(d => d.IntegrationUserResource).WithMany(p => p.IntegrationUserResourceAttributes)
                .HasForeignKey(d => d.IntegrationUserResourceId)
                .HasConstraintName("FK_IntegrationUserResourceAttributes_IntegrationUserResource");

            entity.HasOne(d => d.UserAttribute).WithMany(p => p.IntegrationUserResourceAttributes)
                .HasForeignKey(d => d.UserAttributeId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_IntegrationUserResourceAttributes_UserAttribute");
        });

        modelBuilder.Entity<Job>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_HangFire_Job");

            entity.ToTable("Job", "Scheduler");

            entity.HasIndex(e => e.ExpireAt, "IX_HangFire_Job_ExpireAt").HasFilter("([ExpireAt] IS NOT NULL)");

            entity.HasIndex(e => e.StateName, "IX_HangFire_Job_StateName").HasFilter("([StateName] IS NOT NULL)");

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.ExpireAt).HasColumnType("datetime");
            entity.Property(e => e.StateName).HasMaxLength(20);
        });

        modelBuilder.Entity<JobParameter>(entity =>
        {
            entity.HasKey(e => new { e.JobId, e.Name }).HasName("PK_HangFire_JobParameter");

            entity.ToTable("JobParameter", "Scheduler");

            entity.Property(e => e.Name).HasMaxLength(40);

            entity.HasOne(d => d.Job).WithMany(p => p.JobParameters)
                .HasForeignKey(d => d.JobId)
                .HasConstraintName("FK_HangFire_JobParameter_Job");
        });

        modelBuilder.Entity<JobQueue>(entity =>
        {
            entity.HasKey(e => new { e.Queue, e.Id }).HasName("PK_HangFire_JobQueue");

            entity.ToTable("JobQueue", "Scheduler");

            entity.Property(e => e.Queue).HasMaxLength(50);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.FetchedAt).HasColumnType("datetime");
        });

        modelBuilder.Entity<JobsSchedule>(entity =>
        {
            entity.ToTable("JobsSchedule");

            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.FrequencyRecursOn).HasMaxLength(150);
            entity.Property(e => e.JobId).HasMaxLength(150);
            entity.Property(e => e.Name).HasMaxLength(200);
        });

        modelBuilder.Entity<JobsSequence>(entity =>
        {
            entity.ToTable("JobsSequence");

            entity.HasIndex(e => e.ScheduleId, "IX_JobsSequence_ScheduleId");

            entity.Property(e => e.Name).HasMaxLength(150);

            entity.HasOne(d => d.Schedule).WithMany(p => p.JobsSequences)
                .HasForeignKey(d => d.ScheduleId)
                .HasConstraintName("FK_JobsSequence_JobsSchedule");
        });

        modelBuilder.Entity<Language>(entity =>
        {
            entity.ToTable("Language");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Code).HasMaxLength(10);
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<License>(entity =>
        {
            entity.ToTable("License");

            entity.Property(e => e.Content).HasMaxLength(1000);
        });

        modelBuilder.Entity<List>(entity =>
        {
            entity.HasKey(e => new { e.Key, e.Id }).HasName("PK_HangFire_List");

            entity.ToTable("List", "Scheduler");

            entity.HasIndex(e => e.ExpireAt, "IX_HangFire_List_ExpireAt").HasFilter("([ExpireAt] IS NOT NULL)");

            entity.Property(e => e.Key).HasMaxLength(100);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.ExpireAt).HasColumnType("datetime");
        });

        modelBuilder.Entity<LoginProvider>(entity =>
        {
            entity.ToTable("LoginProvider");

            entity.HasIndex(e => e.LoginProviderTypeId, "IX_LoginProvider_LoginProviderTypeId");

            entity.HasIndex(e => e.Name, "IX_LoginProvider_Name").IsUnique();

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.AuthorityEndpoint).HasMaxLength(250);
            entity.Property(e => e.CallbackPath).HasMaxLength(250);
            entity.Property(e => e.ClientId).HasMaxLength(550);
            entity.Property(e => e.ClientSecret).HasMaxLength(2000);
            entity.Property(e => e.Domains).HasMaxLength(550);
            entity.Property(e => e.IconClass).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(150);
            entity.Property(e => e.Scope).HasMaxLength(550);

            entity.HasOne(d => d.LoginProviderType).WithMany(p => p.LoginProviders)
                .HasForeignKey(d => d.LoginProviderTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LoginProvider_LoginProviderType");
        });

        modelBuilder.Entity<LoginProviderType>(entity =>
        {
            entity.ToTable("LoginProviderType");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<NotificationTemplate>(entity =>
        {
            entity.ToTable("NotificationTemplate");

            entity.Property(e => e.BookmarkList).HasMaxLength(500);
            entity.Property(e => e.Name).HasMaxLength(150);
            entity.Property(e => e.Subject).HasMaxLength(150);
        });

        modelBuilder.Entity<Parameter>(entity =>
        {
            entity.ToTable("Parameter");

            entity.Property(e => e.Content).HasMaxLength(600);
            entity.Property(e => e.Description).HasMaxLength(400);
            entity.Property(e => e.Keyword).HasMaxLength(50);
        });

        modelBuilder.Entity<Privilege>(entity =>
        {
            entity.ToTable("Privilege");

            entity.Property(e => e.Description).HasMaxLength(350);
            entity.Property(e => e.Name).HasMaxLength(150);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("Role");

            entity.Property(e => e.Description).HasMaxLength(350);
            entity.Property(e => e.Name).HasMaxLength(150);
        });

        modelBuilder.Entity<RolesPrivilege>(entity =>
        {
            entity.HasIndex(e => e.PrivilegeId, "IX_RolesPrivileges_PrivilegeId");

            entity.HasIndex(e => e.RoleId, "IX_RolesPrivileges_RoleId");

            entity.HasOne(d => d.Privilege).WithMany(p => p.RolesPrivileges)
                .HasForeignKey(d => d.PrivilegeId)
                .HasConstraintName("FK_RolesPrivileges_Privilege");

            entity.HasOne(d => d.Role).WithMany(p => p.RolesPrivileges)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK_RolesPrivileges_Role");
        });

        modelBuilder.Entity<Schema>(entity =>
        {
            entity.HasKey(e => e.Version).HasName("PK_HangFire_Schema");

            entity.ToTable("Schema", "Scheduler");

            entity.Property(e => e.Version).ValueGeneratedNever();
        });

        modelBuilder.Entity<SecretQuestion>(entity =>
        {
            entity.ToTable("SecretQuestion");

            entity.Property(e => e.Question).HasMaxLength(50);
        });

        modelBuilder.Entity<SecretQuestionsUser>(entity =>
        {
            entity.HasIndex(e => e.SecretQuestionId, "IX_SecretQuestionsUsers_SecretQuestionId");

            entity.HasIndex(e => e.UserId, "IX_SecretQuestionsUsers_UserId");

            entity.Property(e => e.Answer).HasMaxLength(350);

            entity.HasOne(d => d.SecretQuestion).WithMany(p => p.SecretQuestionsUsers)
                .HasForeignKey(d => d.SecretQuestionId)
                .HasConstraintName("FK_SecretQuestionsUsers_SecretQuestion");

            entity.HasOne(d => d.User).WithMany(p => p.SecretQuestionsUsers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_SecretQuestionsUsers_User");
        });

        modelBuilder.Entity<Server>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_HangFire_Server");

            entity.ToTable("Server", "Scheduler");

            entity.HasIndex(e => e.LastHeartbeat, "IX_HangFire_Server_LastHeartbeat");

            entity.Property(e => e.Id).HasMaxLength(200);
            entity.Property(e => e.LastHeartbeat).HasColumnType("datetime");
        });

        modelBuilder.Entity<Set>(entity =>
        {
            entity.HasKey(e => new { e.Key, e.Value }).HasName("PK_HangFire_Set");

            entity.ToTable("Set", "Scheduler");

            entity.HasIndex(e => e.ExpireAt, "IX_HangFire_Set_ExpireAt").HasFilter("([ExpireAt] IS NOT NULL)");

            entity.HasIndex(e => new { e.Key, e.Score }, "IX_HangFire_Set_Score");

            entity.Property(e => e.Key).HasMaxLength(100);
            entity.Property(e => e.Value).HasMaxLength(256);
            entity.Property(e => e.ExpireAt).HasColumnType("datetime");
        });

        modelBuilder.Entity<State>(entity =>
        {
            entity.HasKey(e => new { e.JobId, e.Id }).HasName("PK_HangFire_State");

            entity.ToTable("State", "Scheduler");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(20);
            entity.Property(e => e.Reason).HasMaxLength(100);

            entity.HasOne(d => d.Job).WithMany(p => p.States)
                .HasForeignKey(d => d.JobId)
                .HasConstraintName("FK_HangFire_State_Job");
        });

        modelBuilder.Entity<Structure>(entity =>
        {
            entity.ToTable("Structure");

            entity.HasIndex(e => e.ManagerId, "IX_Structure_ManagerId");

            entity.HasIndex(e => e.StructureParentId, "IX_Structure_StructureParentId");

            entity.Property(e => e.Code).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(450);

            entity.HasOne(d => d.Manager).WithMany(p => p.Structures)
                .HasForeignKey(d => d.ManagerId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Structure_User");

            entity.HasOne(d => d.StructureParent).WithMany(p => p.InverseStructureParent)
                .HasForeignKey(d => d.StructureParentId)
                .HasConstraintName("FK_Structure_Structure");
        });

        modelBuilder.Entity<StructureAttribute>(entity =>
        {
            entity.ToTable("StructureAttribute");

            entity.HasIndex(e => e.AttributeId, "IX_StructureAttribute_AttributeId");

            entity.HasOne(d => d.Attribute).WithMany(p => p.StructureAttributes)
                .HasForeignKey(d => d.AttributeId)
                .HasConstraintName("FK_StructureAttribute_Attribute");
        });

        modelBuilder.Entity<StructureAttributeValue>(entity =>
        {
            entity.ToTable("StructureAttributeValue");

            entity.HasIndex(e => e.StructureId, "IX_StructureAttributeValue_StructureId");

            entity.HasIndex(e => e.AttributeId, "IX_Structure_AttributeId");

            entity.HasOne(d => d.Attribute).WithMany(p => p.StructureAttributeValues)
                .HasForeignKey(d => d.AttributeId)
                .HasConstraintName("FK_StructureAttributeValue_Attribute");

            entity.HasOne(d => d.Structure).WithMany(p => p.StructureAttributeValues)
                .HasForeignKey(d => d.StructureId)
                .HasConstraintName("FK_StructureAttributeValue_Structure");
        });

        modelBuilder.Entity<StructureVirtualAttribute>(entity =>
        {
            entity.ToTable("StructureVirtualAttribute");

            entity.Property(e => e.Name).HasMaxLength(150);
            entity.Property(e => e.Separator).HasMaxLength(10);
        });

        modelBuilder.Entity<StructureVirtualAttributesStructureAttribute>(entity =>
        {
            entity.HasIndex(e => e.StructureAttributeId, "IX_StructureVirtualAttributesStructureAttributes_StructureAttributeId");

            entity.HasIndex(e => e.StructureVirtualAttributeId, "IX_StructureVirtualAttributesStructureAttributes_StructureVirtualAttributeId");

            entity.HasOne(d => d.StructureAttribute).WithMany(p => p.StructureVirtualAttributesStructureAttributes)
                .HasForeignKey(d => d.StructureAttributeId)
                .HasConstraintName("FK_StructureVirtualAttributesStructureAttributes_StructureAttribute");

            entity.HasOne(d => d.StructureVirtualAttribute).WithMany(p => p.StructureVirtualAttributesStructureAttributes)
                .HasForeignKey(d => d.StructureVirtualAttributeId)
                .HasConstraintName("FK_StructureVirtualAttributesStructureAttributes_StructureVirtualAttribute");
        });

        modelBuilder.Entity<StructuresUser>(entity =>
        {
            entity.HasIndex(e => e.StructureId, "IX_StructuresUsers_StructureId");

            entity.HasIndex(e => e.UserId, "IX_StructuresUsers_UserId");

            entity.HasOne(d => d.Structure).WithMany(p => p.StructuresUsers)
                .HasForeignKey(d => d.StructureId)
                .HasConstraintName("FK_StructuresUsers_Structure");

            entity.HasOne(d => d.User).WithMany(p => p.StructuresUsers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_StructuresUsers_User");
        });

        modelBuilder.Entity<SystemRole>(entity =>
        {
            entity.ToTable("SystemRole");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<SystemStructureUserAdmin>(entity =>
        {
            entity.ToTable("SystemStructureUserAdmin");

            entity.HasIndex(e => e.StructureId, "IX_SystemStructureUserAdmin_StructureId");

            entity.HasIndex(e => e.UserId, "IX_SystemStructureUserAdmin_UserId");

            entity.HasOne(d => d.Structure).WithMany(p => p.SystemStructureUserAdmins)
                .HasForeignKey(d => d.StructureId)
                .HasConstraintName("FK_SystemStructureUserAdmin_Structure");

            entity.HasOne(d => d.User).WithMany(p => p.SystemStructureUserAdmins)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_SystemStructureUserAdmin_User");
        });

        modelBuilder.Entity<TranslatorDictionary>(entity =>
        {
            entity.ToTable("TranslatorDictionary");

            entity.HasIndex(e => e.Keyword, "IX_TranslatorDictionary_Keyword").IsUnique();

            entity.Property(e => e.Ar)
                .HasMaxLength(500)
                .HasColumnName("AR");
            entity.Property(e => e.En)
                .HasMaxLength(500)
                .HasColumnName("EN");
            entity.Property(e => e.Fr)
                .HasMaxLength(500)
                .HasColumnName("FR");
            entity.Property(e => e.Keyword).HasMaxLength(200);
        });

        modelBuilder.Entity<TwoFactorAuthenticationProvider>(entity =>
        {
            entity.ToTable("TwoFactorAuthenticationProvider");

            entity.Property(e => e.AssemblyFullQualifiedName).HasMaxLength(750);
            entity.Property(e => e.ClassFileName).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(150);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.HasIndex(e => e.LoginProviderId, "IX_User_LoginProviderId");

            entity.HasIndex(e => e.ManagerId, "IX_User_ManagerId");

            entity.HasIndex(e => e.PhotoAttachmentId, "IX_User_PhotoAttachmentId");

            entity.HasIndex(e => e.SystemRoleId, "IX_User_SystemRoleId");

            entity.Property(e => e.Email).HasMaxLength(250);
            entity.Property(e => e.Enable2Faauth).HasColumnName("Enable2FAAuth");
            entity.Property(e => e.ExternalLoginId).HasMaxLength(450);
            entity.Property(e => e.FirstName).HasMaxLength(150);
            entity.Property(e => e.Gender)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.HashSalt).HasMaxLength(300);
            entity.Property(e => e.LastName).HasMaxLength(150);
            entity.Property(e => e.LoginProviderId).HasDefaultValue((byte)1);
            entity.Property(e => e.MiddleName).HasMaxLength(150);
            entity.Property(e => e.Password).HasMaxLength(2000);
            entity.Property(e => e.TwoFaAuthKey).HasMaxLength(64);
            entity.Property(e => e.Username).HasMaxLength(350);

            entity.HasOne(d => d.LoginProvider).WithMany(p => p.Users)
                .HasForeignKey(d => d.LoginProviderId)
                .HasConstraintName("FK_User_LoginProvider");

            entity.HasOne(d => d.Manager).WithMany(p => p.InverseManager)
                .HasForeignKey(d => d.ManagerId)
                .HasConstraintName("FK_User_User");

            entity.HasOne(d => d.PhotoAttachment).WithMany(p => p.Users)
                .HasForeignKey(d => d.PhotoAttachmentId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_User_Attachment");

            entity.HasOne(d => d.SystemRole).WithMany(p => p.Users)
                .HasForeignKey(d => d.SystemRoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User_SystemRole");
        });

        modelBuilder.Entity<UserApplicationRole>(entity =>
        {
            entity.ToTable("UserApplicationRole");

            entity.HasIndex(e => e.ApplicationId, "IX_UserApplicationRole_ApplicationId");

            entity.HasIndex(e => e.RoleId, "IX_UserApplicationRole_RoleId");

            entity.HasIndex(e => e.UserId, "IX_UserApplicationRole_UserId");

            entity.HasIndex(e => e.UserTypeId, "IX_UserApplicationRole_UserTypeId");

            entity.HasOne(d => d.Application).WithMany(p => p.UserApplicationRoles)
                .HasForeignKey(d => d.ApplicationId)
                .HasConstraintName("FK_UserApplicationRole_Application");

            entity.HasOne(d => d.Role).WithMany(p => p.UserApplicationRoles)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK_UserApplicationRole_Role");

            entity.HasOne(d => d.User).WithMany(p => p.UserApplicationRoles)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_UserApplicationRole_User");

            entity.HasOne(d => d.UserType).WithMany(p => p.UserApplicationRoles)
                .HasForeignKey(d => d.UserTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserApplicationRole_UserType");
        });

        modelBuilder.Entity<UserAttribute>(entity =>
        {
            entity.ToTable("UserAttribute");

            entity.HasIndex(e => e.AttributeId, "IX_UserAttribute_AttributeId");

            entity.HasOne(d => d.Attribute).WithMany(p => p.UserAttributes)
                .HasForeignKey(d => d.AttributeId)
                .HasConstraintName("FK_UserAttribute_Attribute");
        });

        modelBuilder.Entity<UserAttributeValue>(entity =>
        {
            entity.ToTable("UserAttributeValue");

            entity.HasIndex(e => e.StructureId, "IX_UserAttributeValue_StructureId");

            entity.HasIndex(e => e.UserId, "IX_UserAttributeValue_UserId");

            entity.HasIndex(e => e.AttributeId, "IX_User_AttributeId");

            entity.HasOne(d => d.Attribute).WithMany(p => p.UserAttributeValues)
                .HasForeignKey(d => d.AttributeId)
                .HasConstraintName("FK_UserAttributeValue_Attribute");

            entity.HasOne(d => d.Structure).WithMany(p => p.UserAttributeValues)
                .HasForeignKey(d => d.StructureId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_UserAttributeValue_Structure");

            entity.HasOne(d => d.User).WithMany(p => p.UserAttributeValues)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_UserAttributeValue_User");
        });

        modelBuilder.Entity<UserType>(entity =>
        {
            entity.ToTable("UserType");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<UserVirtualAttribute>(entity =>
        {
            entity.ToTable("UserVirtualAttribute");

            entity.Property(e => e.Name).HasMaxLength(150);
            entity.Property(e => e.Separator).HasMaxLength(10);
        });

        modelBuilder.Entity<UserVirtualAttributesUserAttribute>(entity =>
        {
            entity.HasIndex(e => e.UserAttributeId, "IX_UserVirtualAttributesUserAttributes_UserAttributeId");

            entity.HasIndex(e => e.UserVirtualAttributeId, "IX_UserVirtualAttributesUserAttributes_UserVirtualAttributeId");

            entity.Property(e => e.StaticAttributeName).HasMaxLength(150);

            entity.HasOne(d => d.UserAttribute).WithMany(p => p.UserVirtualAttributesUserAttributes)
                .HasForeignKey(d => d.UserAttributeId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_UserVirtualAttributesUserAttributes_UserAttribute");

            entity.HasOne(d => d.UserVirtualAttribute).WithMany(p => p.UserVirtualAttributesUserAttributes)
                .HasForeignKey(d => d.UserVirtualAttributeId)
                .HasConstraintName("FK_UserVirtualAttributesUserAttributes_UserVirtualAttribute");
        });

        modelBuilder.Entity<UsersAnyType>(entity =>
        {
            entity.HasIndex(e => e.AnyTypeId, "IX_UsersAnyTypes_AnyTypeId");

            entity.HasIndex(e => e.UserId, "IX_UsersAnyTypes_UserId");

            entity.HasOne(d => d.AnyType).WithMany(p => p.UsersAnyTypes)
                .HasForeignKey(d => d.AnyTypeId)
                .HasConstraintName("FK_UsersAnyTypes_AnyType");

            entity.HasOne(d => d.User).WithMany(p => p.UsersAnyTypes)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_UsersAnyTypes_User");
        });

        modelBuilder.Entity<Validator>(entity =>
        {
            entity.ToTable("Validator");

            entity.Property(e => e.AssemblyFullQualifiedName).HasMaxLength(750);
            entity.Property(e => e.ClassFileName).HasMaxLength(50);
            entity.Property(e => e.JavascriptFileName).HasMaxLength(50);
            entity.Property(e => e.JavascriptFunctionName).HasMaxLength(150);
            entity.Property(e => e.Name).HasMaxLength(150);
        });

        modelBuilder.Entity<ValidatorProperty>(entity =>
        {
            entity.ToTable("ValidatorProperty");

            entity.HasIndex(e => e.ValidatorId, "IX_ValidatorProperty_ValidatorId");

            entity.Property(e => e.DefaultValue).HasMaxLength(550);
            entity.Property(e => e.Description).HasMaxLength(550);
            entity.Property(e => e.Name).HasMaxLength(150);

            entity.HasOne(d => d.Validator).WithMany(p => p.ValidatorProperties)
                .HasForeignKey(d => d.ValidatorId)
                .HasConstraintName("FK_ValidatorProperty_Validator");
        });

        modelBuilder.Entity<ValidatorsAttributeType>(entity =>
        {
            entity.HasIndex(e => e.AttributeTypeId, "IX_ValidatorsAttributeTypes_AttributeTypeId");

            entity.HasIndex(e => e.ValidatorId, "IX_ValidatorsAttributeTypes_ValidatorId");

            entity.HasOne(d => d.AttributeType).WithMany(p => p.ValidatorsAttributeTypes)
                .HasForeignKey(d => d.AttributeTypeId)
                .HasConstraintName("FK_ValidatorsAttributeTypes_AttributeType");

            entity.HasOne(d => d.Validator).WithMany(p => p.ValidatorsAttributeTypes)
                .HasForeignKey(d => d.ValidatorId)
                .HasConstraintName("FK_ValidatorsAttributeTypes_Validator");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
