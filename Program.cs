using System;
using System.Collections.Generic;

public class Book
{
    public string Title { get; set; }
    public string Author { get; set; }
    public bool IsAvailable { get; set; }

    public Book(string title, string author)
    {
        Title = title;
        Author = author;
        IsAvailable = true; 
    }

    public void DisplayBookInfo()
    {
        Console.WriteLine($"Title: {Title}, Author: {Author}, Available: {IsAvailable}");
    }
}

public class Library
{
    private List<Book> books;

    public Library()
    {
        books = new List<Book>();
    }

    public void AddBook(Book book)
    {
        books.Add(book);
        Console.WriteLine($"Book '{book.Title}' added successfully.");
    }

  
public void BorrowBook(string title)
{
  
    foreach (var oneBook in books)
    {
        
        if (oneBook.Title.Equals(title))
        {
            oneBook.IsAvailable = false; 
            Console.WriteLine($"You have borrowed '{oneBook.Title}'.");
        }else{
Console.WriteLine($"Sorry, '{title}' is either unavailable or not found.");
        }
    }

    
}


    public void ReturnBook(string title)
    {
       
    foreach (var oneBook in books)
    {
        
        if (oneBook.Title.Equals(title))
        {
            oneBook.IsAvailable = true; 
            Console.WriteLine($"You have returned '{oneBook.Title}'.");
        }else
        {
            Console.WriteLine($"Sorry, '{title}' is either unavailable or not found.");
        }
    }

    
    }


    public void DisplayAllBooks()
    {
        if (books.Count == 0)
        {
            Console.WriteLine("No books available in the library.");
        }

        foreach (var book in books)
        {
            if(book.IsAvailable.Equals(true))
            {
                Console.WriteLine($"Title: {book.Title}, Author: {book.Author}, is Available");
            }else if(book.IsAvailable.Equals(false))
            {
                Console.WriteLine($"Title: {book.Title}, Author: {book.Author}, is not Available");
            }
        }
    }
}

public class Program
{
    static void Main(string[] args)
    {
        Library library = new Library();

        while (true)
        {
            Console.Clear();
            Console.WriteLine("Library Management System");
            Console.WriteLine("1. Add Book");
            Console.WriteLine("2. Borrow Book");
            Console.WriteLine("3. Return Book");
            Console.WriteLine("4. View All Books");
            Console.WriteLine("5. Exit");
            Console.Write("Choose an option: ");
            string option = Console.ReadLine();

            if (option == "1")
            {
                Console.Write("Enter book title: ");
                string title = Console.ReadLine();
                Console.Write("Enter author name: ");
                string author = Console.ReadLine();
                Book book = new Book(title, author);
                library.AddBook(book);
                Console.ReadKey();
            }
            else if (option == "2")
            {
                Console.Write("Enter the title of the book to borrow: ");
                string title = Console.ReadLine();
                library.BorrowBook(title);
                Console.ReadKey();
            }
            else if (option == "3")
            {
                Console.Write("Enter the title of the book to return: ");
                string title = Console.ReadLine();
                library.ReturnBook(title);
                Console.ReadKey();
            }
            else if (option == "4")
            {
                library.DisplayAllBooks();
                Console.WriteLine("Press Enter to continue...");
                Console.ReadLine();
            }
            else if (option == "5")
            {
                Console.WriteLine("Exiting Library System...");
                break;
            }
            else
            {
                Console.WriteLine("Invalid option. Please choose again.");
                Console.ReadLine();
            }
        }
    }
}
