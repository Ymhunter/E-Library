public class BorrowId
{
    public int Id {get; set;}
    public string Name { get; set; }
    public string Email { get; set; }

    


    public BorrowId( string name, string email)
    {
       Name = name;
       Email = email;

    }
    public BorrowId()
    {
    }
}