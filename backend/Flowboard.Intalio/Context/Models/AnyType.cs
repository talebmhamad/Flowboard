using System;
using System.Collections.Generic;

namespace Flowboard.Intalio.Context.Models;

public partial class AnyType
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public short AnyTypeObjectId { get; set; }

    public long? CreatedByUserId { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual ICollection<AnyTypeAttributeValue> AnyTypeAttributeValues { get; set; } = new List<AnyTypeAttributeValue>();

    public virtual AnyTypeObject AnyTypeObject { get; set; } = null!;

    public virtual ICollection<Identity> Identities { get; set; } = new List<Identity>();

    public virtual ICollection<UsersAnyType> UsersAnyTypes { get; set; } = new List<UsersAnyType>();
}
