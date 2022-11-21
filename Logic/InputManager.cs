using Dapper;
using MySqlConnector;
public class InputManager
{
    public int InputError(int minValue, int maxValue, string errorMessage)
    {
        int parsedValue;
        while (!Int32.TryParse(Console.ReadLine(), out parsedValue) || parsedValue > maxValue || parsedValue < minValue)
            Console.WriteLine(errorMessage);
        return parsedValue;
    }
    public string ErrorMessage (string message)
    {
        Console.WriteLine(message);
        return message;
    }
    
}