using BlazorAppFin.Client.Shared.DTOs;

namespace BlazorAppFin.Client.Shared.Interfaces;

public interface ITransactionService
{
    public Task<List<TransactionDto>> GetTransactionsAsync();

    public Task<(bool IsSuccess, string ErrorMessage)> AddTransactionAsync(TransactionDto model);

    public Task<(bool IsSuccess, string ErrorMessage)> DeleteTransactionAsync(Guid id);
}
