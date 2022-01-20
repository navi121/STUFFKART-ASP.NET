#nullable disable

namespace StuffKartProject.Models
{
  public class CartDetail
  {
    public string productName { get; set; }
    public string productDescription { get; set; }
    public int price { get; set; }
    public char size { get; set; }
    public int UserId { get; set; }
    public int Quantity { get; set; }
    public int Total { get; set; }
    public int Id { get; set; }
  }
}
