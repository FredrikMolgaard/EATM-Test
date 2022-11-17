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

    public Boolean CheckPin(int enterPin) // används bara vid inlogg. S
    {
        if (enterPin == pin_number)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void CheckNumberOfAttempts(int enteredPin)
    {
        string returnMessage;
        int maxTries = 4;
        int numberOfTries = 1;
        bool pinCorrect = false;

        while (pinCorrect == false)
        {
            if (insertedDebitCard.CheckPin(enteredPin))
            {
                pinCorrect = true;
            }
            else
            {
                returnMessage = "Wrong pin. Try again";
                numberOfTries++;
            }
            if (numberOfTries >= maxTries)
            {
                returnMessage = "ATTENTION! Your card has been seized. Contact your bank for more information\nPress any key to return to menu.";
                Console.ReadKey();
            }

        }

    }
}