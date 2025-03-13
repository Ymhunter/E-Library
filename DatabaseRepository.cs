using System.Data;
using System.Data.SqlClient;
using Dapper;
class DatabaseRepository
{
  private readonly string _connectionString = "";

    private IDbConnection Connect()
    {
        return new SqlConnection(_connectionString);
    }
    public void InsertBorrowers(string name, string email)
    {
        // Spara i datbasen
        string query = $"INSERT INTO Borrowers (Name, Email) VALUES ('{name}', '{email}' )";
        // Console.WriteLine(query);
        // anslut till databasen med hjälp av connection string
        using IDbConnection connection = Connect();
        connection.Execute(query);
    }
    public void InsertReviews(int idBook,int idborrower, int rating, string comment, string reviewDate)
    {
        // Spara i datbasen
        string query = $"INSERT INTO Reviews (IdBook ,IdBorrower, Rating, Comment, ReviewDate) VALUES ('{idBook}','{idborrower}','{rating}', '{comment}', '{reviewDate}')";
        // Console.WriteLine(query);
        // anslut till databasen med hjälp av connection string
        using IDbConnection connection = Connect();
        connection.Execute(query);
    }

   public void InsertLoans(int idBook,int idBorrower,string loanDate, string returnDate)
    {
        // Spara i datbasen
        string query = $"INSERT INTO Loans (IdBook ,IdBorrower, LoanDate, ReturnDate) VALUES ('{idBook}','{idBorrower}', '{loanDate}', '{returnDate}')";
        // Console.WriteLine(query);
        // anslut till databasen med hjälp av connection string
        using IDbConnection connection = Connect();
        connection.Execute(query);
    }

     public List<BooksId> GetIdBook()
    {
        string sql = "SELECT * FROM Books";
        using IDbConnection conn = Connect();
        return conn.Query<BooksId>(sql).AsList();
    }
       public List<BorrowId> GetIdBorrow()
    {
        string sql = "SELECT * FROM Borrowers";
        using IDbConnection conn = Connect();
        return conn.Query<BorrowId>(sql).AsList();
    }
    
    
     public List<Author> GetIdAuthor()
    {
        string sql = "SELECT * FROM Author";
        using IDbConnection conn = Connect();
        return conn.Query<Author>(sql).AsList();
    }
     public List<Category> GetIdCategory()
    {
        string sql = "SELECT * FROM Category";
        using IDbConnection conn = Connect();
        return conn.Query<Category>(sql).AsList();
    }
    public List<Book> GetAllBooks()
    {
        string sql = "SELECT Title, Author.Name AS Author, Category.Name AS Category, Books.IsAvailable FROM Books INNER JOIN Author ON Books.idAuthor = Author.id INNER JOIN Category ON Books.idCategory = Category.id";
        using IDbConnection conn = Connect();
        return conn.Query<Book>(sql).AsList();
    }
public void DeleteBook(string title)
{
    using IDbConnection connection = Connect();

    // First, delete loans associated with the book
    string deleteLoansQuery = @"
        DELETE FROM Loans 
        WHERE IdBook IN (
            SELECT Id 
            FROM Books 
            WHERE Title = @Title
        );";

    // Then, delete the book itself
    string deleteBookQuery = "DELETE FROM Books WHERE Title = @Title;";

    // Execute the queries in sequence
    connection.Execute(deleteLoansQuery, new { Title = title });
    connection.Execute(deleteBookQuery, new { Title = title });
}


     public void InsertAuthor(string author)
    {
        // Spara i datbasen
        string query = $"INSERT INTO Author ( Name) VALUES ( '{author}')";
        // Console.WriteLine(query);
        // anslut till databasen med hjälp av connection string
        using IDbConnection connection = Connect();
        connection.Execute(query);
    }

    
  public void InsertCategory(string category)
    {
        // Spara i datbasen
        string query = $"INSERT INTO Category (Name) VALUES ( '{category}')";
        // Console.WriteLine(query);
        // anslut till databasen med hjälp av connection string
        using IDbConnection connection = Connect();
        connection.Execute(query);
    }

     public void InsertBook(string title,int idAuthor,int idCategory, bool isAvailable)
    {
        // Spara i datbasen
        string query = $"INSERT INTO Books (title,idAuthor,idCategory, isAvailable) VALUES ('{title}','{idAuthor}', '{idCategory}', '{isAvailable}')";
        // Console.WriteLine(query);
        // anslut till databasen med hjälp av connection string
        using IDbConnection connection = Connect();
        connection.Execute(query);
    }
     public void UpdateBookAvailable(string title, bool isAvailable)
    {
    
        // Spara i datbase
        string query = $"UPDATE Books SET IsAvailable = {Convert.ToInt32(isAvailable)} WHERE IsAvailable = {Convert.ToInt32(!isAvailable)} AND Title = '{title}'";
        // Console.WriteLine(query);
        // anslut till databasen med hjälp av connection string
        using IDbConnection connection = Connect();
        connection.Execute(query);
    }

     public void UpdateLoans(string dateReturn, int idBook)
{
    string query = "UPDATE Loans SET ReturnDate = @ReturnDate WHERE IdBook = @IdBook";


    using IDbConnection connection = Connect();

    connection.Execute(query, new { ReturnDate = dateReturn, IdBook = idBook });
}

}