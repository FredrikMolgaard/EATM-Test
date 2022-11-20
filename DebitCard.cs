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

    public string CensoreDebitCard(string card_number) // Hides numbers S
    {
        card_number = $"{string.Concat(Enumerable.Repeat("*", 10))}{card_number.Substring(10)}";
        return card_number;
    }

}