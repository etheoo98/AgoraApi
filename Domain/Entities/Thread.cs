using Domain.Common;

namespace Domain.Entities;

public class Thread : BaseAuditableEntity
{
    public string Title { get; set; }
    public string Content { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
}