public class ReturnDate
{
    public string Title { get; set; }
    public string DateTime { get; set; }

    public ReturnDate(string title, string dateTime)
    {
        Title = title;
        DateTime = dateTime;
    }
    public ReturnDate()
    {
    }
}