using BlazorAppFin.Entities;
using BlazorAppFin.Client.Shared.DTOs;

namespace BlazorAppFin.Extensions;

public static class MappingExtensions
{
    public static TransactionDto ToDto(this Transaction entity)
    {
        return new TransactionDto
        {
            Id = entity.Id,
            Amount = entity.Amount,
            Type = (TransactionDto.TransactionType?) entity.Type,
            Category = entity.Category,
            Date = entity.Date,
            Description = entity.Description
        };
    }

    public static Transaction ToEntity(this TransactionDto dto)
    {
        return new Transaction
        {
            Id = dto.Id == Guid.Empty ? Guid.NewGuid() : dto.Id,
            Amount = dto.Amount,
            Type = dto.Type == TransactionDto.TransactionType.Income
                 ? Transaction.TransactionType.Income
                 : Transaction.TransactionType.Expense,
            Category = dto.Category,
            Date = dto.Date,
            Description = dto.Description
        };
    }
}
