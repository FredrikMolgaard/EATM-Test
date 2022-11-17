using Dapper;
using MySqlConnector;

internal class Program
{
    private static void Main(string[] args)
    {
        DebitcardManager debitcardHolder = null!;
        Menu starmenu = new Menu();
        // starmenu.InjectCard();


        int cardCount = debitcardHolder.GetAmountOfDebitcards();  //Tar reda på hur många "kort" det finns i databasen.
        Console.WriteLine($"Enter Id between 1 and {cardCount}: ");  // Tar in ett kort (samma som Id) och skriver ut "cardCount" för att visa 
        int search = InputError(1, cardCount, "The debit card number you have enterered does not exist");  // Stoppar användaren från att skriva in ett id som inte finns i databasen.
        debitcardHolder.GetCustomerInfo(search);   // Skickar in det sökta värdet för att ta ut all information om kund och bankkort som vi behöver.




        static int InputError(int minValue, int maxValue, string errorMessage)
        {
            int parsedValue;
            while (!Int32.TryParse(Console.ReadLine(), out parsedValue) || parsedValue > maxValue || parsedValue < minValue)
                Console.WriteLine(errorMessage);
            return parsedValue;
        }
    }
}