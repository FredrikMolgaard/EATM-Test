public class Debitcard:Customer
{
    public int ID {get; set;}
    public int PinNumber {get; set;}
    public int CardNumber {get; set;}
    public string ExpirationDate {get; set;}
    public int CvcNumber {get; set;}
    public string BankName {get; set;}


    public Debitcard()
    {

    }

}