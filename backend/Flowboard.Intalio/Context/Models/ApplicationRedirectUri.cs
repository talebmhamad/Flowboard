using System;
using System.Collections.Generic;

namespace Flowboard.Intalio.Context.Models;

public partial class ApplicationRedirectUri
{
    public short Id { get; set; }

    public string RedirectUri { get; set; } = null!;

    public short ApplicationId { get; set; }

    public virtual Application Application { get; set; } = null!;
}
