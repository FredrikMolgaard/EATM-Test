using Dapper;
using MySqlConnector;

public class Menu
{
    DataBaseConnections db = new();
    string debitCardHolder;
    public void InjectCard()
    {
        Console.WriteLine("Skriv in ID: ");
        string? search = Console.ReadLine();
        var searchResult = db.connection.Query<Debitcard>($"SELECT d.card_number, d.bank_name, d.expiration_date, d.cvc_number, d.pin_number, d.account_id, c.name FROM debitcard d INNER JOIN customer c ON d.customer_id = c.ID WHERE d.ID ='{search}'");
        Debitcard insertedDebitCard = null;

        foreach (Debitcard d in searchResult)
        {
            Console.Clear();
            Console.WriteLine("|--------------------------|");
            Console.WriteLine(Convert.ToString(" " + d.bank_name + " " + "\n" + " " + d.card_number + " " + d.expiration_date + " " + d.cvc_number + "\n" + " " + d.Name)); // show card
            Console.WriteLine("|--------------------------|");
            insertedDebitCard = d;
            debitCardHolder = d.Name;
        }
       
        int maxTries = 4;
        int numberOfTries = 1;
        bool pinCorrect = false;
        while (pinCorrect == false)
        {
            Console.WriteLine($"\nWelcome {debitCardHolder}! Your card is valid. Please enter your pin: ");
            int enterPin = Convert.ToInt32(Console.ReadLine());
            if (insertedDebitCard.CheckPin(enterPin))
            {
                pinCorrect = true;
            }
            else
            {
                Console.WriteLine($"Wrong pin. Please try again\nNumber of tries: {numberOfTries}");
                numberOfTries++;
            }

            if (numberOfTries >= maxTries)
            {
                Console.WriteLine($"Too many tries. Your debitcard has been locked. Please contact your bank for more information.");
                Console.ReadKey();
                Environment.Exit(0);
            }

        }
    }
}    