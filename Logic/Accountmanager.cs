using Dapper;
using MySqlConnector;

public class AccountManager : Account
{
    DataBaseConnections db = new();
    Account newAccount = new();

    public int ShowBalance()
    {
        var balanceResult = db.connection.QuerySingle<Account>($"SELECT a.balance FROM account a WHERE a.ID ='{newAccount.ID}'");  // Första menyvalet
        return newAccount.balance;
    }

    public int WithdrawLimit()
    {
        int withDrawMax = 5;
        int withDrawCount = 0;
        int moneyToWithdraw = 0;
        string maxMessage;
        string returnMessage;
       
        while (true)
        {
            withDrawCount++;
            Console.Clear();
            if (withDrawCount > withDrawMax)
            {
                maxMessage ="Max times of withdrawls reached. Please contact your bank for more information. You will go back to menu.";
                break;
            }
            // var balanceResult = db.connection.QuerySingle<Account>($"SELECT balance FROM account WHERE ID ='{newAccount.ID}'");
            // returnMessage = ("Balance result:" + balanceResult.balance);
            else if (balanceResult.balance >= moneyToWithdraw)
            {
                var withdrawResult = db.connection.Query<Account>($"UPDATE account SET balance = '{balanceResult.balance - moneyToWithdraw}' WHERE ID ='{newAccount.ID}'");
                break;
            }
            else
            {
                returnMessage = $"Insufficient funds. You have {balanceResult.balance}kr in your account.\nPress any key to return to menu...";
                Console.ReadLine();
            }
            
        }

        public int MaxWithdrawAmount()
        {
             int maxAmount = 5000;

            if (moneyToWithdraw > maxAmount)
            {
                returnMessage ="Your limit for each withdrawl is 5000.\nRedirecting...";
                Thread.Sleep(3000);
            }
            if (moneyToWithdraw <= 99)
            {
                returnMessage = "Your limit for minimum withdrawl is 100.\nRedirecting...";
                Thread.Sleep(3000);
            }
        }  
    }


}