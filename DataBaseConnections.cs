using Dapper;
using MySqlConnector;

public class DataBaseConnections
{

    private string connectionstring = "Server=localhost;Database=eatm;Uid=eatm;";
    public MySqlConnection? connection;
    public DataBaseConnections()
    {
        SqlConnect();
    }

    public void SqlConnect()
    {
        connection = new MySqlConnection(connectionstring);
    }
    // S 
}