namespace Clases;

public class BankAccount
{
    private static int s_accountNumberSeed = 1234567890;
    public string Number { get; }
    public string Owner { get; set; }
    public decimal Balance 
    {
        get
        {
            decimal balance = 0;
            foreach (var item in _allTransactions)
            {
                balance += item.Amount;
            }
            return balance;
        }
    }


    public void MakeDeposit(decimal amount, DateTime date, string note)
    {
        if (amount < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "Amount of deposit must be positive");
        }
        var deposit = new Transaction(amount, date, note);
        _allTransactions.Add(deposit);
    }

    public void MakeWithdrawal(decimal amount, DateTime date, string note)
    {
        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "Amount of withdrawal must be positive");
        }
        if (Balance - amount < 0)
        {
            throw new InvalidOperationException("Not suffiecient funds for this withdrawal");
        }
        var withdrawal = new Transaction(-amount, date, note);
        _allTransactions.Add(withdrawal);
    }

    public BankAccount(string name, decimal initialBalance)
    {
        this.Number = s_accountNumberSeed.ToString();
        s_accountNumberSeed++;

        this.Owner = name;
        MakeDeposit(initialBalance, DateTime.Now, "Initial balance");
        
    }

    private List<Transaction> _allTransactions = new List<Transaction>();

}





