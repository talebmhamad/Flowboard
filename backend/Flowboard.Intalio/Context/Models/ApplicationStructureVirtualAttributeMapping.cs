using System;
using System.Collections.Generic;

namespace Flowboard.Intalio.Context.Models;

public partial class ApplicationStructureVirtualAttributeMapping
{
    public int Id { get; set; }

    public short ApplicationId { get; set; }

    public int StructureVirtualAttributeId { get; set; }

    public string MappingName { get; set; } = null!;

    public byte? LanguageId { get; set; }

    public long CreatedByUserId { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual Application Application { get; set; } = null!;

    public virtual Language? Language { get; set; }

    public virtual StructureVirtualAttribute StructureVirtualAttribute { get; set; } = null!;
}
