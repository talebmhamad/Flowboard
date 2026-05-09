using System;
using System.Collections.Generic;

namespace Flowboard.Intalio.Context.Models;

public partial class SecretQuestionsUser
{
    public long Id { get; set; }

    public short SecretQuestionId { get; set; }

    public long UserId { get; set; }

    public string Answer { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual SecretQuestion SecretQuestion { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
