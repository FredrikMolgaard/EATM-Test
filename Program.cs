using Dapper;
using MySqlConnector;

internal class Program
{
    private static void Main(string[] args)
    {

        DataBaseConnections db = new();

        Console.WriteLine("Skriv in ID: ");
        string search = Console.ReadLine();

        var searchResult = db.connection.Query<Customer>($"SELECT ID, name, addres, social_security_number FROM Customer WHERE ID='{search}'");

        foreach (Customer c in searchResult)
        {
            if (Convert.ToString(c.Id).Contains(search))
            {
                Console.WriteLine(c.Id + " " + c.Name + " " + c.Addres + "" + c.SocialSecurityNumber);
            }

        }

    }

}