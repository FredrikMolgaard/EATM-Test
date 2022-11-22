using Dapper;
using MySqlConnector;
public class TransactionsManager
{
    DataBaseConnections db = new();
    public void insertTransactions(int withdraw, int AccountId)
    {
        DateTime localDate = DateTime.Now;
        var insertTransactions = db.connection.Query<Transaction>($"INSERT INTO transactions (withdraw, date, account_id) VALUES ('{withdraw}', '{localDate}', '{AccountId}')");
    }
    public IEnumerable<Transaction> GetTransactions(int AccountId)
    {
        IEnumerable<Transaction> transactions = db.connection.Query<Transaction>($"SELECT * FROM transactions WHERE account_id ='{AccountId}'");
        return transactions;

    }

}