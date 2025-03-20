namespace AccountBank.Domain.Models
{
    public class BalanceModel
    {
        public int Id { get; private set; }
        public decimal AvaliableAmount { get; private set; }
        public decimal BlokedAmount { get; private set; }

        private BalanceModel() { }

        public BalanceModel(int id)
        {
            Id = id;
            AvaliableAmount = 0;
            BlokedAmount = 0;
        }

        public void AddAmount(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Valor adicionado precisa ser maior que 0");
            AvaliableAmount += amount;
        }

        public void SubAmount(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Valor descontado precisa ser maior que 0");
            AvaliableAmount -= amount;
            if (AvaliableAmount < 0)
                throw new ArgumentException("Saldo insuficiente");
        }
    }
}
