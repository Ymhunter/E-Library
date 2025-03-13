public class BooksId
{
    public int Id { get; set; }
    public string Title { get; set; }
    


    public BooksId(int id, string title)
    {
        Id = id;
       Title = title;
    }
    public BooksId()
    {
    }
}