using Dapper;
using MySqlConnector;

public class DebitCardManager : Debitcard
{ 
    Debitcard value;
    DataBaseConnections db = new(); /// S?
    public void CountUser()
    {
        var numberOfUsers = db.connection.QuerySingle<Debitcard>("SELECT COUNT(ID) FROM debitcard;");
    }

    public Debitcard GetCustomerInfo(string search)   
    {
        var searchResult = db.connection.Query<Debitcard>($"SELECT d.card_number, d.bank_name, d.expiration_date, d.cvc_number, d.pin_number, d.account_id, c.name FROM debitcard d INNER JOIN customer c ON d.customer_id = c.ID WHERE d.ID ='{search}'");
        
        // Debitcard value;
        
         foreach (Debitcard d in searchResult)
        {
          if (true)
          {
            value = d;
          }
        }
        return value; 
            
    }
    public int GetAmountOfDebitCards()
    {
        var numberOfCards = db.connection.QuerySingle<int>("SELECT COUNT(ID) FROM debitcard;");
        return numberOfCards;
    }

}