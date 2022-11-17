using Dapper;
using MySqlConnector;

public class DebitcardManager : Debitcard
{
    Debitcard customerInfo;
    Debitcard insertedDebitCard = null!;
    DataBaseConnections db = new(); /// S?
    public void CountUsers()
    {
        var numberOfUsers = db.connection.QuerySingle<Debitcard>("SELECT COUNT(ID) FROM debitcard;");
    }

    public Debitcard GetCustomerInfo(int cardId)
    {
        var searchCustomerInfo = db.connection.Query<Debitcard>($"SELECT d.card_number, d.bank_name, d.expiration_date, d.cvc_number, d.pin_number, d.account_id, c.name FROM debitcard d INNER JOIN customer c ON d.customer_id = c.ID WHERE d.ID ='{cardId}'");

        foreach (Debitcard card in searchCustomerInfo)
        {
            if (true)
            {
                customerInfo = card;
            }
        }
        return customerInfo;

    }
    public int GetAmountOfDebitcards()
    {
        var numberOfCards = db.connection.QuerySingle<int>("SELECT COUNT(ID) FROM debitcard;");
        return numberOfCards;
    }

    public void Debitcard()
    {

        // string debitcardHolder;
        int maxTries = 4;
        int numberOfTries = 1;
        bool pinCorrect = false;

        while (pinCorrect == false)
        {
            Console.WriteLine($"\nWelcome {customerInfo}! Your card is valid. Please enter your pin: ");
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