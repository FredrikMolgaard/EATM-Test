using Dapper;
using MySqlConnector;

internal class Program
{
    private static void Main(string[] args)
    {
        DebitcardManager customerInfo = null!;
        Menu starmenu = new Menu();


        int cardCount = customerInfo.GetAmountOfDebitcards();  //Tar reda på hur många "kort" det finns i databasen.
        Console.WriteLine($"Enter Id between 1 and {cardCount}: ");  // Tar in ett kort (samma som Id) och skriver ut "cardCount" för att visa 
        int cardId = InputError(1, cardCount, "The debit card number you have enterered does not exist");  // Stoppar användaren från att skriva in ett id som inte finns i databasen.
        customerInfo.GetCustomerInfo(cardId);  // Skickar in det sökta värdet för att ta ut all information om kund och bankkort som vi behöver.


        Console.Clear();
        string cardNumberCensored = Convert.ToString(customerInfo.card_number);
        Console.WriteLine($" ________________________________________");
        Console.WriteLine($"|                               {customerInfo.bank_name}   ");
        Console.WriteLine($"|  {cardNumberCensored}                                   ");
        Console.WriteLine($"|                                        ");
        Console.WriteLine($"|  [##]>>))                              ");
        Console.WriteLine($"|            Exp Date        CVC         ");
        Console.WriteLine($"|              {customerInfo.expiration_date}          {customerInfo.cvc_number}         ");
        Console.WriteLine($"|                                        ");
        Console.WriteLine($"| {customerInfo.Name}                       ");
        Console.WriteLine($"|_________________________________________");
        cardNumberCensored = customerInfo.CensoreDebitCard(cardNumberCensored);



        static int InputError(int minValue, int maxValue, string errorMessage)
        {
            int parsedValue;
            while (!Int32.TryParse(Console.ReadLine(), out parsedValue) || parsedValue > maxValue || parsedValue < minValue)
                Console.WriteLine(errorMessage);
            return parsedValue;
        }
    }
}