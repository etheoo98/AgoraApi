using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;

namespace Domain.Entities;

public class Thread : BaseAuditableEntity
{
    public string Title { get; set; }
    public string Content { get; set; }
    public int ForumId { get; set; }
    public Forum Forum { get; set; }
    
    [ForeignKey("CreatorId")] 
    public User Creator { get; set; }
    public int CreatorId { get; set; }
}