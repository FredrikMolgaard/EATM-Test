using Dapper;
using MySqlConnector;

public class Menu
{
    DataBaseConnections db = new();
    public void InjectCard()

    {

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
                    // Flyttad till accountmanager
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
                    // Console.WriteLine("Ejecting card");
                    // Console.WriteLine("...............")
                    // Thread.Sleep(2500);
                    // Environment.Exit(0);
                }
            }
        }
    }
}
}