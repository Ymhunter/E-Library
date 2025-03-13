public class Review
{
    public int Rating { get; set; }
    public string Comment { get; set; }
    


    public Review(int rating, string comment)
    {
        Rating = rating;
        Comment = comment;
    }
    public Review()
    {
    }
}