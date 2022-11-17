using Dapper;
using MySqlConnector;

public class DebitcardManager : Debitcard
{
    Debitcard debitcardHolder;
    Debitcard insertedDebitCard = null!;
    DataBaseConnections db = new(); /// S?
    public void CountUsers()
    {
        var numberOfUsers = db.connection.QuerySingle<Debitcard>("SELECT COUNT(ID) FROM debitcard;");
    }

    public Debitcard GetCustomerInfo(int search)
    {
        var searchResult = db.connection.Query<Debitcard>($"SELECT d.card_number, d.bank_name, d.expiration_date, d.cvc_number, d.pin_number, d.account_id, c.name FROM debitcard d INNER JOIN customer c ON d.customer_id = c.ID WHERE d.ID ='{search}'");

        foreach (Debitcard card in searchResult)
        {
            if (true)
            {
                debitcardHolder = card;
            }
        }
        return debitcardHolder;

    }
    public int GetAmountOfDebitcards()
    {
        var numberOfCards = db.connection.QuerySingle<int>("SELECT COUNT(ID) FROM debitcard;");
        return numberOfCards;
    }

    public void Debitcard()
    {
        
        string debitcardHolder;
        int maxTries = 4;
        int numberOfTries = 1;
        bool pinCorrect = false;

        while (pinCorrect == false)
        {
            Console.WriteLine($"\nWelcome {debitcardHolder}! Your card is valid. Please enter your pin: ");
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