using Dapper;
using MySqlConnector;

public class Menu
{
    DataBaseConnections db = new();
    Debitcard insertedDebitCard = null!;
    string? debitCardHolder;
    public void InjectCard()

    {

        Console.Clear();
        // Console.WriteLine("Injecting card.......... Please Wait");
        // Thread.Sleep(5000);
        Console.WriteLine("Skriv in ID: "); // UI
        string? search = Console.ReadLine(); // UI
        
        // DebitCardManager test = new();
        // Debitcard x = test.GetCustomerInfo(search);
        // Console.WriteLine(x.bank_name);
        // Console.ReadLine();
        
        var numberOfUsers = db.connection.QuerySingle<int>("SELECT COUNT(ID) FROM debitcard;");   // HÄMTAR UT ANTALET BANKKORT I DB. Använd resultat för att se till att man inte kan skriva in ID på ej-existerande bankkort.
         var searchResult = db.connection.Query<Debitcard>($"SELECT d.card_number, d.bank_name, d.expiration_date, d.cvc_number, d.pin_number, d.account_id, c.name FROM debitcard d INNER JOIN customer c ON d.customer_id = c.ID WHERE d.ID ='{search}'");
        
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

            {
                int withDrawMax = 5;
                int withDrawCount = 0;
                bool menu = true;
                while (menu == true)
                {
                    Console.Clear();
                    Console.WriteLine("[1] - Show Balance\n[2] - Withdraw money\n[3] - Transaction history\n[4] - Exit");
                    ConsoleKey menuKey = Console.ReadKey().Key;

                    if (menuKey == ConsoleKey.D1)
                    {
                        Console.Clear();
                        var balanceResult = db.connection.QuerySingle<Account>($"SELECT a.balance FROM account a WHERE a.ID ='{insertedDebitCard.account_id}'");
                        Console.WriteLine("Balance result:" + balanceResult.balance);
                        Console.ReadLine();
                    }
                    if (menuKey == ConsoleKey.D2)
                    {
                        while (true)
                        {
                            withDrawCount++;
                            Console.Clear();
                            if (withDrawCount >= withDrawMax)
                            {
                                Console.WriteLine("Max times of withdrawls reached. Please contact your bank for more information. You will go back to menu.  ");
                                break;
                            }
                            Console.WriteLine("How much you want to withdraw?: ");
                            int moneyToWithdraw = Convert.ToInt32(Console.ReadLine());
                            int maxAmount = 5000;
                            var balanceResult = db.connection.QuerySingle<Account>($"SELECT balance FROM account WHERE ID ='{insertedDebitCard.account_id}'");
                            Console.WriteLine("Balance result:" + balanceResult.balance);
                            if (moneyToWithdraw > maxAmount)

                            {
                                Console.WriteLine("Your limit for each withdrawl is 5000.\nRedirecting...");
                                Thread.Sleep(3000);
                            }
                            if (moneyToWithdraw <= 99)
                            {
                                Console.WriteLine("Your limit for minimum withdrawl is 100.\nRedirecting...");
                                Thread.Sleep(3000);
                            }

                            else if (balanceResult.balance >= moneyToWithdraw)
                            {
                                var withdrawResult = db.connection.Query<Account>($"UPDATE account SET balance = '{balanceResult.balance - moneyToWithdraw}' WHERE ID ='{insertedDebitCard.account_id}'");
                                break;
                            }
                            else
                            {
                                Console.WriteLine($"Insufficient funds. You have {balanceResult.balance}kr in your account.\nPress any key to return to menu...");
                                Console.ReadLine();
                            }
                        }
                    }
                    if (menuKey == ConsoleKey.D4)
                    {
             
                        
                        
                        
                        Console.WriteLine("Ejecting card");
                        Console.WriteLine("...............");
                        Thread.Sleep(2500);
                        Environment.Exit(0);
                    }
                }
            }
        }
    }
}