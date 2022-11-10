using Dapper;
using MySqlConnector;

internal class Program
{
    public static string sqlString = "Server=localhost;Database=eatm;Uid=Fredrik;Pwd=123;";
    private static void Main(string[] args)
    {

        void ValidateCustomer()
        {
            using (var connection = new MySqlConnection(sqlString))
            {
                var searchResult = connection.Query<Customer>($"SELECT ID FROM Customer WHERE ID=1");
                foreach (Customer c in searchResult)
                {
                    if (Convert.ToString(c.Id).Contains("1"))
                    {
                        Console.WriteLine(c.Id);
                    }

                }

            }
        }
    }

}