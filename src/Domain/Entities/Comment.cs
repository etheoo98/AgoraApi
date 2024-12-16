using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;

namespace Domain.Entities;

public class Comment : BaseAuditableEntity
{
    public string Content { get; set; }
    
    public Topic Topic { get; set; }
    public int TopicId { get; set; }
    
    [ForeignKey("AuthorId")]
    public User Author { get; set; }
    public int AuthorId { get; set; }
}