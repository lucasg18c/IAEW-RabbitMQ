using Producer.Model;

namespace Producer.Data;

public class MockDB
{
  public List<Order> Order { get; set; } = new();
}