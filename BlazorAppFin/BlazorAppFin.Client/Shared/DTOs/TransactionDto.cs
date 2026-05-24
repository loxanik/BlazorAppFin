using System.ComponentModel.DataAnnotations;

namespace BlazorAppFin.Client.Shared.DTOs;

public class TransactionDto
{
    public enum TransactionType 
    { 
        Income,
        Expense
    }

    public Guid Id { get; set; } = Guid.NewGuid();

    [Required(ErrorMessage = "Введите сумму")]
    [Range(0.01, 1000000, ErrorMessage = "Сумма должна быть больше нуля")]
    public decimal Amount { get; set; }

    [Required(ErrorMessage = "Выберите тип транзакции")]
    public TransactionType? Type { get; set; }

    [Required(ErrorMessage = "Введите категорию")]
    [StringLength(30, ErrorMessage = "Длина категории до 30 символов")]
    public string Category { get; set; } = string.Empty;

    [Required(ErrorMessage = "Введите дату")]
    public DateTime Date { get; set; } = DateTime.Now;

    public string Description { get; set; } = string.Empty;
}
