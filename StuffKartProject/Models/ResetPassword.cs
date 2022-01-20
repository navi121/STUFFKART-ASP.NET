namespace StuffKartProject.Models
{
  public class ResetPassword
  {
    public string Email { get; set; }
    public long MobileNumber { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
    public string SecurityAnswer { get; set; }
    public string SecurityQuestion { get; set; }
  }
}
