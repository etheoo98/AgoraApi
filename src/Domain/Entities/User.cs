using Domain.Common;

namespace Domain.Entities;

public class User : BaseAuditableEntity
{
    public string Email { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public DateTimeOffset? LastLogin { get; set; }
    public List<Topic> Topics { get; set; } = [];
}