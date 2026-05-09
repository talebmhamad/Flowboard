using System;
using System.Collections.Generic;

namespace Flowboard.Intalio.Context.Models;

public partial class SecretQuestion
{
    public short Id { get; set; }

    public string Question { get; set; } = null!;

    public long CreatedByUserId { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual ICollection<SecretQuestionsUser> SecretQuestionsUsers { get; set; } = new List<SecretQuestionsUser>();
}
