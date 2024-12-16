using Domain.Common;

namespace Domain.Entities;

public class Forum : BaseAuditableEntity
{
    public string Name { get; set; }
    public string? Description { get; set; }
    
    public Forum? ParentForum { get; set; }
    public List<Forum> SubForums { get; set; } = [];
    public List<Topic> Topics { get; set; } = [];
}