using BlazorAppFin.Client.Shared.DTOs;
using BlazorAppFin.Data;
using BlazorAppFin.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorAppFin.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransactionsController : ControllerBase
{
    private readonly AppDbContext _context;

    public TransactionsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<TransactionDto>>> GetTransactions()
    {
        var transactionsFromDb = await _context.Transactions.ToListAsync();

        var dtos = transactionsFromDb.Select(t => t.ToDto()).ToList();

        return Ok(dtos);
    }

    [HttpPost]
    public async Task<IActionResult> AddTransaction([FromBody]TransactionDto dto)
    {
        if (dto == null) return BadRequest("Данные транзакции пусты");

        var entity = dto.ToEntity();

        _context.Transactions.Add(entity);
        await _context.SaveChangesAsync();

        var resultDto = entity.ToDto();

        return CreatedAtAction(nameof(GetTransactions), new {id = entity.Id}, resultDto);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteTransaction(Guid id)
    {
        var transaction = await _context.Transactions.FindAsync(id);

        if (transaction == null) return NotFound("Транзакция c ID: {id} не найдена");

        _context.Transactions.Remove(transaction);
        await _context.SaveChangesAsync();

        return Ok();
    }
}
