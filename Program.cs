using Dapper;
using MySqlConnector;

internal class Program
{
    private static void Main(string[] args)
    {
        DebitcardManager customerInfo = new();


        int cardCount = customerInfo.GetAmountOfDebitcards();  //Tar reda på hur många "kort" det finns i databasen.
        Console.WriteLine($"Enter Id between 1 and {cardCount}: ");  // Tar in ett kort (samma som Id) och skriver ut "cardCount" för att visa 
        int cardId = InputError(1, cardCount, "The debit card number you have enterered does not exist");  // Stoppar användaren från att skriva in ett id som inte finns i databasen.
        Debitcard cardInfo = customerInfo.GetCustomerInfo(cardId);  // Skickar in det sökta värdet för att ta ut all information om kund och bankkort som vi behöver.

        Console.WriteLine("Enter pin");
        int checkPin = Convert.ToInt32(Console.ReadLine());
        // customerInfo.CheckPin(checkPin);


        Console.Clear();
        string cardNumberCensored = Convert.ToString(cardInfo.card_number);
        cardNumberCensored = customerInfo.CensoreDebitCard(cardNumberCensored);
        Console.WriteLine($" ________________________________________");
        Console.WriteLine($"|                               {cardInfo.bank_name}   ");
        Console.WriteLine($"|  {cardNumberCensored}                                   ");
        Console.WriteLine($"|                                        ");
        Console.WriteLine($"|  [##]>>))                              ");
        Console.WriteLine($"|            Exp Date        CVC         ");
        Console.WriteLine($"|              {cardInfo.expiration_date}          {cardInfo.cvc_number}         ");
        Console.WriteLine($"|                                        ");
        Console.WriteLine($"| {cardInfo.Name}                       ");
        Console.WriteLine($"|_________________________________________");



        static int InputError(int minValue, int maxValue, string errorMessage)
        {
            int parsedValue;
            while (!Int32.TryParse(Console.ReadLine(), out parsedValue) || parsedValue > maxValue || parsedValue < minValue)
                Console.WriteLine(errorMessage);
            return parsedValue;
        }
    }
}