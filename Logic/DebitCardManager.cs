using Dapper;
using MySqlConnector;



public class DebitcardManager
{
    Debitcard? insertedDebitCard;
    DataBaseConnections db = new(); /// S?
    InputManager error = new();
    DateTime localDate = DateTime.Now;
    int numberOfPinAttempts = 0;

    public Debitcard GetCustomerInfo(int cardId)
    {
        var searchCustomerInfo = db.connection.Query<Debitcard>($"SELECT d.card_number AS CardNumber, d.bank_name AS BankName, d.expiration_date AS ExpirationDate, d.cvc_number AS CvcNumber, d.pin_number AS PinNumber, d.account_id AS AccountId, c.name FROM debitcard d INNER JOIN customer c ON d.customer_id = c.ID WHERE d.ID ='{cardId}'");
        while (true)
        {
            foreach (Debitcard card in searchCustomerInfo)
            {
                if (true)
                {
                    insertedDebitCard = card;
                }
            }
            return insertedDebitCard;
        }
    }

    public int GetAmountOfDebitcards()
    {
        var numberOfCards = db.connection.QuerySingle<int>("SELECT COUNT(ID) FROM debitcard;");
        return numberOfCards;
    }

    public bool CheckPin(int enterPin) // används bara vid inlogg. S
    {
        numberOfPinAttempts++;
        if (enterPin == insertedDebitCard.PinNumber)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool NumberOfMaxPinAttemptsReached()
    {
        if (numberOfPinAttempts >= 4)
        {
            return true;
        }

        return false;
    }

    public string CensoreDebitCard(string card_number) // Hides numbers S
    {
        card_number = $"{string.Concat(Enumerable.Repeat("*", 10))}{card_number.Substring(10)}";
        return card_number;
    }
    public void CheckDate()
    {
        if (localDate > insertedDebitCard.ExpirationDate)
        {
            Console.Clear();
            error.ErrorMessage("YOUR CARD HAS EXPIRED. PLEASE CONTACT YOUR BANK FOR MORE INFORMATION. \nPRESS ANY KEY TO EXIT");
            Console.ReadLine();
            Environment.Exit(0);
        }
    }

    public string ShortenDateTime(DateTime ExpirationDate)
    {
        string ExpirationDateLong = ExpirationDate.ToString();
        char[] Shorten = { '0', ':', '0', ':', '0' };
        string ExpirationDateShort = ExpirationDateLong.TrimEnd(Shorten);
        return ExpirationDateShort;
    }
}