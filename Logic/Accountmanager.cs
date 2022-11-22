using Dapper;
using MySqlConnector;

public class AccountManager
{
    DataBaseConnections db = new();
    Account activeAccount = new();
    InputManager inputManager = new();
    TransactionsManager transactionsManager = new();

    // Metod för att uppdatera vilket account som är aktivt
    public void SetActiveAccount(int id)
    {
        activeAccount.ID = id;
    }

    // Plocka ut balance för att aktiva accountet. Alltså det debitcard som är isatt och kopplat till ett visst account
    public int GetBalance()
    {
        int balanceResult = db.connection.QuerySingle<int>($"SELECT balance FROM account WHERE ID ='{activeAccount.ID}'");
        return balanceResult;
    }


    public void Withdraw(int cashWithdraw)
    {
        int myBalance = GetBalance();
        var cashWithdrawal = db.connection.Query<Account>($"UPDATE account SET balance = '{myBalance - cashWithdraw}' WHERE ID ='{activeAccount.ID}'");
        transactionsManager.InsertTransactions(cashWithdraw, activeAccount.ID);
    }
    public bool CurrenciesMenu(int cashChoice)
    {
        int myBalance = GetBalance();
        if (myBalance <= 0)
        {
            inputManager.ErrorMessage("INSUFFICIENT FUNDS. RETURNING TO MENU...");
            Thread.Sleep(2000);
            return false;
        }
        switch (cashChoice)
        {
            case 1:
                Withdraw(100);
                break;
            case 2:
                Withdraw(200);
                break;
            case 3:
                Withdraw(500);
                break;
            case 4:
                Withdraw(1000);
                break;
            case 5:
                Withdraw(inputManager.InputError(100, 5000, "INVALID WITHDRAWAL AMOUNT. AMOUNT MUST BE BETWEEN 100SEK - 5000SEK"));
                break;
        }
        return true;
    }
}