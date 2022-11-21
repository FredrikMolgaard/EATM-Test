using Dapper;
using MySqlConnector;

internal class Program
{
    private static void Main(string[] args)
    {
        DebitcardManager customerInfo = new();
        AccountManager accountManager = new();
        InputManager inputManager = new();


        int cardCount = customerInfo.GetAmountOfDebitcards();  //Tar reda på hur många "kort" det finns i databasen.
        Console.WriteLine($"Enter Id between 1 and {cardCount}: ");  // Tar in ett kort (samma som Id) och skriver ut "cardCount" för att visa 
        int cardId = inputManager.InputError(1, cardCount, "The debit card number you have enterered does not exist");  // Stoppar användaren från att skriva in ett id som inte finns i databasen.
        Debitcard cardInfo = customerInfo.GetCustomerInfo(cardId);  // Skickar in det sökta värdet för att ta ut all information om kund och bankkort som vi behöver.
        
        bool pinValidated = false;
        while(pinValidated == false)
        {
            Console.WriteLine("Enter pin");
            int checkPin = Convert.ToInt32(Console.ReadLine());
            pinValidated = customerInfo.CheckPin(checkPin);
            Console.WriteLine("Wrong Pin, Try Again");
            if(customerInfo.NumberOfMaxPinAttemptsReached())
            {
                Console.WriteLine("Max Pin Attempts Reached, Your Card Locked");
                Environment.Exit(0);
            }
        }

        Console.Clear();
        string cardNumberCensored = Convert.ToString(cardInfo.CardNumber);
        cardNumberCensored = customerInfo.CensoreDebitCard(cardNumberCensored);
        Console.WriteLine($" ________________________________________");
        Console.WriteLine($"|                               {cardInfo.BankName}   ");
        Console.WriteLine($"|  {cardNumberCensored}                                   ");
        Console.WriteLine($"|                                        ");
        Console.WriteLine($"|  [##]>>))                              ");
        Console.WriteLine($"|            Exp Date        CVC         ");
        Console.WriteLine($"|              {cardInfo.ExpirationDate}          {cardInfo.CvcNumber}         ");
        Console.WriteLine($"|                                        ");
        Console.WriteLine($"| {cardInfo.Name}                       ");
        Console.WriteLine($"|_________________________________________\n\n");
        Console.WriteLine($"             WELCOME {cardInfo.Name}\n\n");
        Console.WriteLine("_________________________________________________________\n");

        bool menu = true;
        while (menu == true)
        {
            // Läs ut Account för det isatta debitcard
            accountManager.SetActiveAccount(cardId);

            Console.WriteLine("[1] - SHOW BALANCE                    CASH WITHDRAWAL - [2]\n[3] - TRANSACTION HISTORY                       EXIT - [4]");
            ConsoleKey menuKey = Console.ReadKey().Key;

            if (menuKey == ConsoleKey.D1)
            {
                Console.Clear();
                int currentBalance = accountManager.GetBalance();
                Console.WriteLine("Current Balance: " +  currentBalance);
                Thread.Sleep(3000);
            }
            if (menuKey == ConsoleKey.D2)
            {
                Console.Clear();
                Console.WriteLine("PLEASE SELECT AMOUNT ");
                Console.WriteLine("[1]-[100kr]                    [200kr][2]-\n\n[3]-[500kr]                    [1000kr]-[4]\n          [5]ENTER AMOUNT:  ");
                int cashChoice = inputManager.InputError(1, 5, "WRONG INPUT. PLEASE CHOOSE BETWEEN OPTION 1-5");
                accountManager.CurrenciesMenu(cashChoice);
                Console.WriteLine("PLEASE WAIT. COUNTING CASH...");
                Thread.Sleep(5000);
                Console.Beep();
                Console.WriteLine("PLEASE TAKE YOUR CARD (Press any key)");
                Console.ReadLine();
                Console.Clear();
                Console.WriteLine("PLEASE COLLECT YOUR MONEY (Press any key)");
                Console.ReadLine();
                Console.Clear();
                Console.WriteLine("THANK YOU FOR USING E-ATM. WELCOME BACK");
                Thread.Sleep(10000);
            }
            if (menuKey == ConsoleKey.D3)
            {

            }
            if (menuKey == ConsoleKey.D4)
            {
                Console.Clear();
                Console.WriteLine("     THANK YOU.\n\nPLEASE TAKE YOUR CARD");
                Thread.Sleep(3000);
                Environment.Exit(0);
            }
        }
    }
}