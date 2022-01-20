namespace StuffKartProject.Models
{
  public class UserDetails
  {
    public int UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public long MobileNumber { get; set; }
    public string SecurityAnswer { get; set; }
    public string SecurityQuestion { get; set; }
    public int isAdmin { get; set; }
  }
}
