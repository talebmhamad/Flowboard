using System;
using System.Collections.Generic;

namespace Flowboard.Intalio.Context.Models;

public partial class IntegrationItemDataSourceProperty
{
    public int Id { get; set; }

    public short IntegrationItemId { get; set; }

    public int IntegrationDataSourcePropertyId { get; set; }

    public string Value { get; set; } = null!;

    public virtual IntegrationDataSourceProperty IntegrationDataSourceProperty { get; set; } = null!;

    public virtual IntegrationItem IntegrationItem { get; set; } = null!;
}
