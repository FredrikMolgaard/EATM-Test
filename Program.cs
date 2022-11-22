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
        Console.WriteLine($"INSERT YOUR DEBIT CARD (Id): ");  // Tar in ett kort (samma som Id) och skriver ut "cardCount" för att visa 
        int cardId = inputManager.InputError(1, cardCount, "DEBIT CARD IS NOT VALID");  // Stoppar användaren från att skriva in ett id som inte finns i databasen.
        Debitcard cardInfo = customerInfo.GetCustomerInfo(cardId);  // Skickar in det sökta värdet för att ta ut all information om kund och bankkort som vi behöver.
        // customerInfo.CheckDate(); // kollar så datumet på kortet inte har gått ut. 

        bool pinValidated = false;
        while (pinValidated == false)
        {
            customerInfo.CheckDate();
            Console.WriteLine("ENTER 4-DIGIT PIN");
            int checkPin = inputManager.InputError(0000, 9999, "PIN MUST BE A 4-DIGIT NUMBER");
            pinValidated = customerInfo.CheckPin(checkPin);

            Console.WriteLine("WRONG PIN");
            if (customerInfo.NumberOfMaxPinAttemptsReached())
            {
                Console.WriteLine("YOU HAVE EXCEEDED THE MAXIMUM NUMBER OF PIN ATTEMPTS, YOUR CARD HAS BEEN SEIZED\nPLEASE CONTACT YOUR BANK FOR MORE INFORMATION");
                Environment.Exit(0);
            }
        }

        Console.Clear();
        string ExpirationDateShort = customerInfo.ShortenDateTime(cardInfo.ExpirationDate);
        string cardNumberCensored = Convert.ToString(cardInfo.CardNumber);
        cardNumberCensored = customerInfo.CensoreDebitCard(cardNumberCensored);
        Console.WriteLine($" ________________________________________");
        Console.WriteLine($"|                        {cardInfo.BankName}   ");
        Console.WriteLine($"|  {cardNumberCensored}                                   ");
        Console.WriteLine($"|                                        ");
        Console.WriteLine($"|  [##]>>))                              ");
        Console.WriteLine($"|            Exp Date        CVC         ");
        Console.WriteLine($"|           {ExpirationDateShort}      {cardInfo.CvcNumber}         ");
        Console.WriteLine($"|                                        ");
        Console.WriteLine($"| {cardInfo.Name}                       ");
        Console.WriteLine($"|_________________________________________\n\n");
        Console.WriteLine($"             WELCOME {cardInfo.Name}\n\n");
        Console.WriteLine("_________________________________________________________\n");
        Console.WriteLine("PRESS ANY KEY TO CONTINUE");
        Console.ReadLine();

        bool menu = true;
        while (menu == true)
        {
            Console.Clear();
            // Läs ut Account för det isatta debitcard
            accountManager.SetActiveAccount(cardId);

            Console.WriteLine("[1] - SHOW BALANCE                    CASH WITHDRAWAL - [2]\n[3] - TRANSACTION HISTORY                       EXIT - [4]");
            ConsoleKey menuKey = Console.ReadKey().Key;

            if (menuKey == ConsoleKey.D1)
            {
                Console.Clear();
                int currentBalance = accountManager.GetBalance();
                Console.WriteLine("CURRENT BALANCE: " + currentBalance + "SEK");
                Thread.Sleep(3000);
            }
            if (menuKey == ConsoleKey.D2)
            {
                Console.Clear();
                Console.WriteLine(" ________________________________________");
                Console.WriteLine("PLEASE SELECT AMOUNT ");
                Console.WriteLine("[1]-[100SEK]                    [200SEK][2]-\n\n[3]-[500SEK]                    [1000SEK]-[4]\n          [5]ENTER AMOUNT:  ");
                Console.WriteLine(" ________________________________________");
                int cashChoice = inputManager.InputError(1, 5, "WRONG INPUT. PLEASE CHOOSE BETWEEN OPTION 1-5");
                accountManager.CurrenciesMenu(cashChoice);
                Console.WriteLine("PLEASE WAIT. COUNTING CASH...");
                Thread.Sleep(5000);
                Console.WriteLine("PLEASE TAKE YOUR CARD (Press any key)");
                Console.ReadLine();
                Console.Clear();
                Console.WriteLine("PLEASE COLLECT YOUR MONEY (Press any key)");
                Console.ReadLine();
                Console.Clear();
                Console.WriteLine("THANK YOU FOR USING E-ATM. WELCOME BACK");
                Thread.Sleep(3000);
                Environment.Exit(0);
            }
            if (menuKey == ConsoleKey.D3)
            {
                Console.Clear();
                int currentBalance = accountManager.GetBalance();
                Console.WriteLine("                CASH RECEIPT\n\nCARD NO                    " + cardNumberCensored);
                Console.WriteLine("AVAILABLE BALANCE                     " + currentBalance + "\n");
                foreach (Transaction t in accountManager.GetTransactions(cardId))
                {
                    Console.WriteLine(t.Date + "                 " + t.Withdraw + "SEK");
                }
                Console.ReadLine();
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