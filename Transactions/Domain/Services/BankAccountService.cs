using Newtonsoft.Json;
using System.Text;
using System.Text.Json;
using Transactions.Domain.DTOs;
using Transactions.Domain.Models;

public class BankAccountService
{
    private readonly HttpClient _httpClient;
    private readonly string _bankAccountApiUrl = "https://localhost:"; // Substitua pela URL do seu BanckAccount 

    public BankAccountService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    // Consulta o saldo da conta
    public async Task<TransactionDto?> GetBalanceAsync(int accountId)
    {
        var response = await _httpClient.GetAsync($"{_bankAccountApiUrl}/Balance/{accountId}");

        if (!response.IsSuccessStatusCode) return null;

        var content = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<TransactionDto>(content);
    }

    // Atualiza o saldo da conta
    public async Task<bool> UpdateBalanceAsync(TransactionModel transaction)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = System.Text.Json.JsonSerializer.Serialize(transaction, options);

        var jsonContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

    // Enviando a requisição para atualizar o saldo
        var response = await _httpClient.PutAsync($"{_bankAccountApiUrl}/Balance/update-balance", jsonContent);

        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Erro ao chamar API: {errorContent}");
            return false;
        }

        return true;
    }

}
