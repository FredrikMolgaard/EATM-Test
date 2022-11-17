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


}