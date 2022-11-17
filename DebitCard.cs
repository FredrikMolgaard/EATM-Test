public class Debitcard : Customer
{
    public int Id { get; set; }
    public int pin_number { get; set; }
    public long card_number { get; set; }
    public string? expiration_date { get; set; }
    public int cvc_number { get; set; }
    public string? bank_name { get; set; }
    public int account_id { get; set; }


    public Debitcard()
    {

    }

    public Boolean CheckPin(int enterPin) // används bara vid inlogg. S
    {
        if (enterPin == pin_number)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public string CensoreDebitCard(string card_number) // Hides numbers S
    {
        card_number = $"{string.Concat(Enumerable.Repeat("*", 10))}{card_number.Substring(10)}";
        return card_number;
    }

}