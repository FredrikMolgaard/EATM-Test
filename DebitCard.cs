using Dapper;
using MySqlConnector;
public class Debitcard : Customer
{
    public int Id { get; set; }
    public int PinNumber { get; set; }
    public long CardNumber { get; set; }
    public string? ExpirationDate { get; set; }
    public int CvcNumber { get; set; }
    public string? BankName { get; set; }
    public int AccountId { get; set; }


    public Debitcard()
    {

    }

}