using System.Net.Http.Json;
using BlazorAppFin.Client.Shared.DTOs;
using BlazorAppFin.Client.Shared.Interfaces;

namespace BlazorAppFin.Client.Services;

public class BlazorTransactionService : ITransactionService
{
    private readonly HttpClient _http;

    public BlazorTransactionService(HttpClient http)
    {
        _http = http;
    }

    public async Task<(bool IsSuccess, string ErrorMessage)> AddTransactionAsync(TransactionDto model)
    {
        var response = await _http.PostAsJsonAsync("api/transactions", model);

        if (response.IsSuccessStatusCode)
            return (true, string.Empty);
        
        return (false, await response.Content.ReadAsStringAsync());
    }

    public async Task<(bool IsSuccess, string ErrorMessage)> DeleteTransactionAsync(Guid id)
    {
        var response = await _http.DeleteAsync($"api/transactions/{id}");

        if (response.IsSuccessStatusCode)
            return (true, string.Empty);

        return (false, await response.Content.ReadAsStringAsync());
    }

    public async Task<List<TransactionDto>> GetTransactionsAsync()
    {
        var result = await _http.GetFromJsonAsync<List<TransactionDto>>("api/transactions");

        return result ?? new List<TransactionDto>();
    }
}
