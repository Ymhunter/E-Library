using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;


public class Program
{
    static void Main(string[] args)
    {
        Library library = new Library();
        bool huvudMeny = true;
        while (huvudMeny)
        {
            Console.Clear();
            Console.WriteLine("Library Management System");
            Console.WriteLine("1. User");
            Console.WriteLine("2. Administrators");
            Console.WriteLine("3. Exit");
            Console.Write("Choose an option: ");
            string option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        library.UserLibrary();
                        break;

                    case "2":
                        library.AdministratorLibrary();
                        break;
    

                    case "3":
                        Console.WriteLine("Programmet avslutas...");
                        Thread.Sleep(2000);
                        huvudMeny = false;
                        break;
                    default:
                        Console.WriteLine("Ogiltlig inmatning! ");
                        break;
                }
        }
    }
}
