using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;

namespace Domain.Entities;

public class Comment : BaseAuditableEntity
{
    public string Content { get; set; }
    
    public Thread Thread { get; set; }
    public int ThreadId { get; set; }
    
    [ForeignKey("AuthorId")]
    public User Author { get; set; }
    public int AuthorId { get; set; }
}