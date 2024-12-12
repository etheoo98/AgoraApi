namespace Domain.Entities;

public class RefreshToken
{
    public int Id { get; set; }
    public string Token { get; set; }
    public DateTime ExpiresOnUtc { get; set; }
    
    // Foreign Keys
    public int UserId { get; set; }
    
    // Navigational Properties
    public User User { get; set; }
}