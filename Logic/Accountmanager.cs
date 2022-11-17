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
            // var currentBalance = db.connection.QuerySingle<Account>($"SELECT balance FROM account WHERE ID ='{newAccount.ID}'");
            // returnMessage = ("Balance result:" + currentBalance.balance);
            // else if (newAccount.balance >= cashWithdraw)                                                                                                                  // <----Den här delen borde inte ligga här. Kanske borde ligga i en egen metod?
            // {
            //     var withdrawResult = db.connection.Query<Account>($"UPDATE account SET balance = '{newAccount.balance - cashWithdraw}' WHERE ID ='{newAccount.ID}'");
            //     returnMessage = ""
            // }
            else
            {
                returnMessage = $"Insufficient funds. You have {newAccount.balance}kr in your account.\n\nPress any key to return to menu...";
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


}