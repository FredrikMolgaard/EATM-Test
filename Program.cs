using Dapper;
using MySqlConnector;

internal class Program
{
    private static void Main(string[] args)
    {
        DataBaseConnections db = new();

        Console.WriteLine("Skriv in ID: ");
        string search = Console.ReadLine();

        var searchResult = db.connection.Query<Debitcard>($"SELECT d.card_number, d.bank_name, d.expiration_date, d.cvc_number, c.name FROM debitcard d INNER JOIN customer c ON d.customer_id = c.ID WHERE d.ID ='{search}'");

        foreach (Debitcard d in searchResult)
        {
            if (Convert.ToString(d.ID).Contains(search))
            {
                Console.WriteLine(d.CardNumber + " " + d.ExpirationDate + " " + d.CvcNumber + "" + d.Name +  "" + d.BankName + "" );
            }
           

        }

    }

}