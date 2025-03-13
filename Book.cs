using System.ComponentModel;

public class Book
{
    public string Title { get; set; }
    public string Author { get; set; }
    public string Category { get; set; }
    public bool IsAvailable { get; set; }

    
    


    


    public Book(string title, string author, string category)
    {
        Title = title;
        Author = author;
        Category = category;
        IsAvailable = true;
    }
    public Book()
    {
    }

}