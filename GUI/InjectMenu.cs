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
        Debitcard insertedDebitCard = null!;

        foreach (Debitcard d in searchResult)
        {
            Console.Clear();
            string cardNumberCensored = Convert.ToString(d.card_number);
            Console.WriteLine($" ________________________________________");
            Console.WriteLine($"|                               {d.bank_name}   ");
            Console.WriteLine($"|  {cardNumberCensored}                                   ");
            Console.WriteLine($"|                                        ");
            Console.WriteLine($"|  [##]>>))                              ");
            Console.WriteLine($"|            Exp Date        CVC         ");
            Console.WriteLine($"|              {d.expiration_date}          {d.cvc_number}         ");
            Console.WriteLine($"|                                        ");
            Console.WriteLine($"| {d.Name}                       ");
            Console.WriteLine($"|_________________________________________");
            insertedDebitCard = d;
            debitCardHolder = d.Name;
            cardNumberCensored = d.CensoreDebitCard(cardNumberCensored);
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