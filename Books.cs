class Books
{
public string Title {get; set;}
public string Author {get; set;}
public bool IsAvialable {get; set;}

public Books(string title, string author, int iSBN, bool isAvialable){
    Title = title;
    Author = author;
    IsAvialable = isAvialable;
    
}

    public Books()
    {
    }
}