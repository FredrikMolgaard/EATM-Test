using Dapper;
using MySqlConnector;

public class AccountManager 
{
    DataBaseConnections db = new();
    Account activeAccount = new();
    
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
        var cashWithdrawal = db.connection.Query<Account>($"UPDATE account SET balance = '{activeAccount.Balance - cashWithdraw}' WHERE ID ='{activeAccount.ID}'");
    }

    public string WithdrawLimit()
    {
        int withdrawCount = 0;
        int cashWithdraw = 0;
        string returnMessage;
        while (true)
        {
            withdrawCount++;
            Console.Clear();
            if (withdrawCount > 5)
            {
                returnMessage = "Maximum withdrawal limit reached. Please contact your bank for more information.\n\nRedirecting to menu...";
                Thread.Sleep(5000);
                return returnMessage;
            }
            else
            {
                returnMessage = $"Insufficient funds. You have {activeAccount.Balance}kr in your account.\n\nPress any key to return to menu...";
                Console.ReadLine();
                return returnMessage;
            }

        }

    }
    public string MaxWithdrawAmount(int cashWithdraw)
    {
        string returnMessage;

        if (cashWithdraw > 5000)
        {
            returnMessage = "Your limit for each withdrawl is 5000.\nRedirecting...";
            Thread.Sleep(5000);
            return returnMessage;
        }
        else if (cashWithdraw <= 99)
        {
            returnMessage = "Your limit for minimum withdrawl is 100.\nRedirecting...";
            Thread.Sleep(5000);
            return returnMessage;
        }
        return null;
    }

    public void CurrenciesMenu(int cashChoice)
    {
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
                Withdraw(cashChoice);
                break;
        }
    }


}