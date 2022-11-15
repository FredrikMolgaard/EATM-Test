using Dapper;
using MySqlConnector;

public class Menu
{
    DataBaseConnections db = new();
    string debitCardHolder;
    public void InjectCard()
    {   
        // Console.WriteLine("Injecting card.......... Please Wait");
        // Thread.Sleep(5000);
        Console.WriteLine("Skriv in ID: ");
        string? search = Console.ReadLine();
        var searchResult = db.connection.Query<Debitcard>($"SELECT d.card_number, d.bank_name, d.expiration_date, d.cvc_number, d.pin_number, d.account_id, c.name FROM debitcard d INNER JOIN customer c ON d.customer_id = c.ID WHERE d.ID ='{search}'");
        Debitcard insertedDebitCard = null!;

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

            
            
            {bool menu = true;
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
                    Console.Clear();
                    Console.WriteLine("How much you want to withdraw?: ");
                    int moneyToWithdraw = Convert.ToInt32(Console.ReadLine());
                    int maxAmount = 5000;
                    var balanceResult = db.connection.QuerySingle<Account>($"SELECT balance FROM account WHERE ID ='{insertedDebitCard.account_id}'");
                    Console.WriteLine("Balance result:" + balanceResult.balance);
                   
                    if (moneyToWithdraw > maxAmount)
                    {
                        Console.WriteLine("Your limit for each withdrawl is 5000.");
                        Thread.Sleep(3000);
                    }

                    else if (balanceResult.balance >= moneyToWithdraw)
                    {
                        var withdrawResult = db.connection.Query<Account>($"UPDATE account SET balance = '{balanceResult.balance - moneyToWithdraw}' WHERE ID ='{insertedDebitCard.account_id}'");
                    }
                    else
                    {
                        Console.WriteLine($"Insufficient funds. You have {balanceResult.balance}kr in your account.\nPress any key to return to menu...");
                        Console.ReadLine();
                    }
                }
                if (menuKey == ConsoleKey.D4)
                {
                    Console.Clear();
                    Console.WriteLine("Ejecting card.......");
                    Thread.Sleep(2500);
                    Console.WriteLine("Din mamma är en mullvaden");
                    Thread.Sleep(2500);
                    Console.WriteLine("Hejdå sonen till mamma mullvad");
                    Thread.Sleep(2500);
                    Environment.Exit(0);
                }
            }
            }
        }
    }

}