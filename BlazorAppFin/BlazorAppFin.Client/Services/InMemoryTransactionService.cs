using BlazorAppFin.Client.Shared.DTOs;
using BlazorAppFin.Client.Shared.Interfaces;

namespace BlazorAppFin.Client.Services;

public class InMemoryTransactionService : ITransactionService
{
    private List<TransactionDto> _transactions = [];

    public InMemoryTransactionService()
    {
        _transactions.Add(new TransactionDto { Amount = 1500, Category = "Продуктовый", Type = TransactionDto.TransactionType.Expense, Description = "Покупка продуктов" });
        _transactions.Add(new TransactionDto { Amount = 35000, Category = "Зарплата", Type = TransactionDto.TransactionType.Income, Description = "Месячная зарплата", Date = DateTime.Parse("15.01.2026") });
        _transactions.Add(new TransactionDto { Amount = 1400, Category = "Перевод", Type = TransactionDto.TransactionType.Expense, Description = "Перевод за услугу" });
    }

    public async Task<(bool IsSuccess, string ErrorMessage)> AddTransactionAsync(TransactionDto model)
    {
        await Task.Delay(1000);

        _transactions.Add(model);
        return (true, string.Empty);
    }

    public async Task<(bool IsSuccess, string ErrorMessage)> DeleteTransactionAsync(Guid id)
    {
        await Task.Delay(100);

        var item = _transactions.Find(t => t.Id == id);

        if (item != null)
        {
            _transactions.Remove(item);
            return (true, string.Empty);
        }

        return (false, "Транзакция не была найдена");
    }

    public Task<List<TransactionDto>> GetTransactionsAsync()
    {
        return Task.FromResult(_transactions);
    }
}
