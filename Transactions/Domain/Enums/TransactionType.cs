using System.Text.Json.Serialization;

namespace Transactions.Domain.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum TransactionType
    {
        CREDIT,
        DEBIT,
        AMOUNT_HOLD,
        AMOUNT_RELEASE
    }
}
