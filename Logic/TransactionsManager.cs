using Dapper;
using MySqlConnector;
public class TransactionsManager
{
    
    DataBaseConnections db = new();
    public void InsertTransactions(int withdraw, int account_id)
    {
        DateTime localDate = DateTime.Now;
        var insertTransactions = db.connection.Query<Transaction>($"INSERT INTO transactions (withdraw, date, account_id) VALUES ('{withdraw}', '{localDate}', '{account_id}')");
    }
    public List<Transaction> GetTransactions(int account_id) 
    {
        var transactions = db.connection.Query<Transaction>($"SELECT * FROM transactions WHERE account_id ='{account_id}'");
        return new List<Transaction>(transactions); // skickar in en lista med transaktioner. 
    } 

}