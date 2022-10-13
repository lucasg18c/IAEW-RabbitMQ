using Microsoft.AspNetCore.Mvc;
using Producer.Data;
using Producer.Dto;
using Producer.Model;
using Producer.RabbitMQ;

namespace Producer.Controllers;

[ApiController]
[Route("[controller]")]
public class OrdersController : ControllerBase
{
  private readonly MockDB _context;
  private readonly IMessageProducer _messagePublisher;
  public OrdersController(IMessageProducer messagePublisher)
  {
    _context = new();
    _messagePublisher = messagePublisher;
  }

  [HttpPost]
  public async Task<IActionResult> CreateOrder(OrderDto orderDto)
  {
    Order order = new()
    {
      ProductName = orderDto.ProductName,
      Price = orderDto.Price,
      Quantity = orderDto.Quantity
    };

    _context.Order.Add(order);
    //await _context.SaveChangesAsync();

    _messagePublisher.SendMessage(order);

    return Ok(new { id = order.Id });
  }
}