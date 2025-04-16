using Microsoft.AspNetCore.Mvc;
using Transactions.Core.Aplication.Transfers.Commands;
using Transactions.Core.Aplication.Transfers.DTOs;
using Transactions.Core.Aplication.Transfers.Handlers;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransfersController : ControllerBase
{
    private readonly CreateTransferHandler _handler;

    public TransfersController(CreateTransferHandler handler)
    {
        _handler = handler;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateTransferDto dto)
    {
        var command = new CreateTransferCommand(dto);
        var id = await _handler.HandleAsync(command);

        return CreatedAtAction(nameof(GetById), new { id }, null);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(Guid id)
    {
        return Ok();
    }
}
