﻿using Dapper;
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

        Console.WriteLine("Enter pin");
        int checkPin = Convert.ToInt32(Console.ReadLine());
        // customerInfo.CheckNumberOfAttempts(checkPin);

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
        Console.WriteLine($"|_________________________________________");
        Console.ReadLine();


        bool menu = true;
        while (menu == true)
        {
            Console.Clear();
            Console.WriteLine("[1] - Show Balance\n[2] - Withdraw money\n[3] - Transaction history\n[4] - Exit");
            ConsoleKey menuKey = Console.ReadKey().Key;

            if (menuKey == ConsoleKey.D1)
            {
                accountManager.ShowBalance();
            }
            if (menuKey == ConsoleKey.D2)
            {

            }
            if (menuKey == ConsoleKey.D3)
            {

            }
            if (menuKey == ConsoleKey.D4)
            {
                Console.Clear();
                Console.WriteLine("Thank you.\n\nPlease take your card");
                Thread.Sleep(3000);
                Environment.Exit(0);
            }
        }
    }
}