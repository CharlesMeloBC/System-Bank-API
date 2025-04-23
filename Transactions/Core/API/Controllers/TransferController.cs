using Microsoft.AspNetCore.Mvc;
using Transactions.Core.Aplication.Transfers.Commands;
using Transactions.Core.Aplication.Transfers.DTOs;
using Transactions.Core.Application.Transfers.Handlers;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransfersController : ControllerBase
{
    private readonly CreateTransferItemHandler _handler;

    public TransfersController(CreateTransferItemHandler handler)
    {
        _handler = handler;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateTransferItemDto dto)
    {
        var command = new CreateTransferItemCommand(dto);
        var id = await _handler.HandleAsync(command);

        return CreatedAtAction(nameof(GetById), new { id }, null);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(Guid id)
    {
        return Ok();
    }
}
