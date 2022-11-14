using Dapper;
using MySqlConnector;

internal class Program
{
    private static void Main(string[] args)
    {
        DataBaseConnections db = new();

        Console.WriteLine("Skriv in ID: ");
        string search = Console.ReadLine();
        var searchResult = db.connection.Query<Debitcard>($"SELECT d.card_number, d.bank_name, d.expiration_date, d.cvc_number, d.pin_number, d.account_id, c.name FROM debitcard d INNER JOIN customer c ON d.customer_id = c.ID WHERE d.ID ='{search}'");
        Debitcard insertedDebitCard = null;

        foreach (Debitcard d in searchResult)
        {
            Console.WriteLine("|--------------------------|");
            Console.WriteLine(Convert.ToString(" " + d.bank_name + " " + "\n" + " " + d.card_number + " " + d.expiration_date + " " + d.cvc_number + "\n" + " " + d.Name)); // show card
            Console.WriteLine("|--------------------------|");
            insertedDebitCard = d;
        }



    }
}