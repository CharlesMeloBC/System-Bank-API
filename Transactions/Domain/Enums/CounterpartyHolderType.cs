using System.Text.Json.Serialization;

namespace Transactions.Domain.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum CounterpartyHolderType
    {
        NATURAL,
        LEGAL
    }
}
