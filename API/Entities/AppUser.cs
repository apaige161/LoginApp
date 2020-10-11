namespace API.Entities
{
    //expands on AppUser from the DataContext.cs file
    public class AppUser
    {
        //primary key of DB
            //everytime a new user is created it incrementally add new user with a new Id
        public int Id { get; set; }
        public string UserName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

    }
}