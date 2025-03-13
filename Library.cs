class Library
{

    static DatabaseRepository repo = new DatabaseRepository();

    private List<Book> books = new List<Book>();

    private List<Borrower> borrowers = new List<Borrower>();

    private List<Review> reviews = new List<Review>();

    private List<ReturnDate> returnDates = new List<ReturnDate>();


    IEnumerable<Author> authors = repo.GetIdAuthor();

    IEnumerable<Category> categories = repo.GetIdCategory();

    IEnumerable<BooksId> booksIds = repo.GetIdBook();

    IEnumerable<BorrowId> borrowIds = repo.GetIdBorrow();

    string dateNow = DateTime.Now.ToString("yyyy-MM-dd");



    public void UserLibrary()
    {


        while (true)
        {
            Console.Clear();
            Console.WriteLine("Welcome to E-Library");
            Console.WriteLine("1. Search a book ");
            Console.WriteLine("2. Borrow a book");
            Console.WriteLine("3. Return a book");
            Console.WriteLine("4. View all books");
            Console.WriteLine("5. Back");
            Console.Write("Choose an option: ");
            string option = Console.ReadLine();

            if (option == "1")
            {
                SearchBook();
                Console.ReadKey();
            }
            else if (option == "2")
            {
                Console.Write("Enter the title of the book to borrow: ");
                string title = Console.ReadLine();
                BorrowBook(title);

                Console.ReadKey();
            }
            else if (option == "3")
            {
                Console.Write("Enter the title of the book to return: ");
                string title = Console.ReadLine();
                ReturnBook(title);
                Console.ReadKey();
            }
            else if (option == "4")
            {
                DisplayAllBooks();
                Console.WriteLine("Press Enter to continue...");
                Console.ReadLine();
            }
            else if (option == "5")
            {
                Console.WriteLine("back Library System...");
                break;
            }
            else
            {
                Console.WriteLine("Invalid option. Please choose again.");
                Console.ReadLine();
            }
        }
    }
    public void AdministratorLibrary()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Wlecome to Administrator Library Management System");
            Console.WriteLine("1. Add a book");
            Console.WriteLine("2. Remove a book");
            Console.WriteLine("3. View all books");
            Console.WriteLine("4. Exit");
            Console.Write("Choose an option: ");
            string option = Console.ReadLine();

            if (option == "1")
            {
                AddBook();
                Console.ReadKey();
            }
            else if (option == "2")
            {
                RemoveBook();
            }
            else if (option == "3")
            {
                DisplayAllBooks();
                Console.WriteLine("Press Enter to continue...");
                Console.ReadKey();
            }
            else if (option == "4")
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



    public void AddBook()
    {


        Console.Write("Enter book title: ");
        string title = CHelp.ReadString();
        Console.Write("Enter author name: ");
        string author = CHelp.ReadString();
        Console.Write("Enter book category: ");
        string category = CHelp.ReadString();
        Book objbooks = new Book(title, author, category);
        books.Add(objbooks);

        repo.InsertAuthor(author);

        repo.InsertCategory(category);
        bool hasInserted = false;

        foreach (Book b in books)
        {
            foreach (Author a in authors)
            {
                foreach (Category c in categories)
                {
                    if (a.Name == b.Author && c.Name == b.Category)
                    {
                        repo.InsertBook(b.Title, a.Id, c.Id, true);
                        hasInserted = true;
                        break;
                    }
                }

                if (hasInserted) break;
            }

            if (hasInserted) break;
        }



    }

    public void RemoveBook()
    {

        Console.Write("Enter book title: ");
        string title = CHelp.ReadString();
        Console.Write("Enter author name: ");
        string author = CHelp.ReadString();
        Console.Write("Enter book category: ");
        string category = CHelp.ReadString();
        Book objbooks = new Book(title, author, category);
        books.Add(objbooks);
        repo.DeleteBook(title);
        foreach (Book b in books)
        {
            if (b.Equals(title))
                Console.WriteLine($"Book '{b.Title}' added successfully.");
        }
    }


    public void BorrowBook(string title)
{
    IEnumerable<Book> books = repo.GetAllBooks();

    bool bookFound = false;

    foreach (Book b in books)
    {
        if (b.Title.Equals(title, StringComparison.OrdinalIgnoreCase) && b.IsAvailable)
        {
            Console.WriteLine("What is your name?");
            string userName = CHelp.ReadString();

            Console.WriteLine("What is your email?");
            string userEmail = CHelp.ReadString();

            Borrower borrower = new Borrower(userName, userEmail);
            borrowers.Add(borrower);
            b.IsAvailable = false;
            bookFound = true;
            repo.UpdateBookAvailable(title, false);


            bool hasInserted2 = false;

            if(bookFound == true)
            {
            foreach (BooksId bId in booksIds)
            {
                    foreach (BorrowId brId in borrowIds)
                    {
                        if (bId.Title == title && b.Title == bId.Title && brId.Name == userName)
                        {
                            repo.InsertLoans(bId.Id, brId.Id, dateNow,"null");
                            repo.InsertBorrowers(userName, userEmail);
                            hasInserted2 = true;
                            Console.WriteLine($"You have borrowed '{b.Title}'.");
                            bookFound = true;
                            break;
                        }
                        if (hasInserted2) break;
                    }
                    if (hasInserted2) break;
        }

        if (bookFound) break;
    }
    if (!bookFound)
    {
        Console.WriteLine($"Sorry, the book '{title}' is not available.");
        break;
    }
    }
    }
}




   public void ReturnBook(string title)
{
    IEnumerable<Book> books = repo.GetAllBooks();
    IEnumerable<BooksId> booksIds = repo.GetIdBook();
    IEnumerable<BorrowId> borrowIds = repo.GetIdBorrow();

    Console.WriteLine("What's your name?");
    string name = CHelp.ReadString();

    bool hasInserted = false;

    foreach (Book b in books)
    {
        if (b.Title.Equals(title))
        {
            b.IsAvailable = true;
            Console.WriteLine($"You have returned '{b.Title}'.");
            repo.UpdateBookAvailable(title, true);
            hasInserted = true;
            break;
        }
    }

    if (!hasInserted)
    {
        Console.WriteLine("Book not found or already available.");
        return;
    }
    Console.WriteLine("What would you rate the book from 1-5?");
    int rating = CHelp.ReadInt();
    Console.WriteLine("Any comment?");
    string comment = CHelp.ReadString();
    Review review = new Review(rating, comment);
    reviews.Add(review);

    bool hasInserted2 = false;

    if (hasInserted)
    {
        foreach (Book b1 in books)
        {
            foreach (BooksId bId in booksIds)
            {
                foreach (BorrowId br in borrowIds)
                {
                    if (bId.Title == title && b1.Title == bId.Title && br.Name == name)
                    {
                        
                        repo.InsertReviews(bId.Id, br.Id, rating, comment, dateNow);
                        repo.UpdateLoans(dateNow, bId.Id);
                        Console.WriteLine("The book is returned.");
                        hasInserted2 = true;
                        break;
                    }

                    if (hasInserted2) break;
                }

                if (hasInserted2) break;
            }

            if (hasInserted2) break;
        }
    }

    if (!hasInserted2)
    {
        Console.WriteLine("The operation could not be completed. Please check the details and try again.");
    }
}








    public void SearchBook()
    {
        IEnumerable<Book> Allbooks = repo.GetAllBooks();
        Console.WriteLine("What is the title of the book you're looking for?");
        string searchBook = CHelp.ReadString();

        bool found = false;

        foreach (Book b in Allbooks)
        {
            if (b.Title.Equals(searchBook))
            {
                Console.WriteLine($"Title: {b.Title} Author: {b.Author}, Category: {b.Category}, Is Available: {b.IsAvailable}");
                found = true;
                break;
            }
        }

        if (!found)
        {
            Console.WriteLine($"The book titled \"{searchBook}\" is not available.");
        }
    }



    public void DisplayAllBooks()
    {
        IEnumerable<Book> allbooks = repo.GetAllBooks();
        foreach (Book b in allbooks)
        {
            Console.WriteLine($"Title: {b.Title} Author: {b.Author},Category: {b.Category}, IsAvailable: {b.IsAvailable}");
        }


    }
}