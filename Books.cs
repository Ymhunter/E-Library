class Books
{
public string Title {get; set;}
public string Auther {get; set;}
public int ISBN {get; set;}
public bool IsAvialable {get; set;}

public Books(string title, string auther, int iSBN, bool isAvialable){
    Title = title;
    Auther = auther;
    ISBN = iSBN;
    IsAvialable = isAvialable;
    
}
}