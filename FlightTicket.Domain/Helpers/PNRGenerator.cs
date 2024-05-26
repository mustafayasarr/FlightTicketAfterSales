namespace FlightTicket.Domain.Helpers;

public static class PNRGenerator
{
    private static Random random = new Random();

    public static string Generate()
    {
        int length = 8;
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return $"TK-{new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray())}"; 
    }
}
