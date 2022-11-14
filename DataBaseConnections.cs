using Dapper;
using MySqlConnector;

public class DataBaseConnections
{

    private string connectionstring = "Server=localhost;Database=eatm;Uid=eatm;Pwd=;";
    public MySqlConnection? connection;
    public DataBaseConnections()
    {
        SqlConnect();
    }

    public void SqlConnect()
    {
        connection = new MySqlConnection(connectionstring);
    }

}