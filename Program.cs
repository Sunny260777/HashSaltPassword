// See https://aka.ms/new-console-template for more information

using HashSaltPassword;
using System.Data;
using System.Security.Cryptography;
using System.Text;

Console.WriteLine("Hello, Welcome to the our application!");
Prompt();


void Prompt()
{
    Console.Clear();
    Console.WriteLine("[R] Register [L] Login");


    while (true)
    {
        var input = Console.ReadLine().ToUpper()[0];

        switch (input)
        {
         case 'L': Login(); break;
         case 'R': Register(); break;
            default:
                break;
        }
    }
}

void Register()
{
    Console.Clear();

    Console.WriteLine("============Registration==============");
    Console.Write("User Name");
    var name= Console.ReadLine();
    Console.Write("Password");
    var password = Console.ReadLine();

    using AppDataContext appContext = new AppDataContext();

    var salt = DateTime.Now.ToString();
    var HashedPW = HashPassword($"{password}{salt}");

    appContext.Users.Add(new User() { Name = name, Password = HashedPW, Salt = salt });
    appContext.SaveChanges();

    while (true)
    {
        Console.Clear();
        Console.WriteLine("Registration Complete");
        Console.WriteLine("[B] Back");
        if (Console.ReadKey().Key == ConsoleKey.B)
        {
            Prompt();
        }

    }
}

string HashPassword(string password)
{
    SHA256 hash = SHA256.Create();

    var passwordBytes = Encoding.Default.GetBytes(password);

    var hashedpassword = hash.ComputeHash(passwordBytes);

    return Convert.ToHexString(hashedpassword);

}
void Login()
{
    Console.Clear();
    Console.WriteLine("============Login========");
    Console.Write("User Name ");
    var name = Console.ReadLine();
    Console.Write("Password ");
    var password = Console.ReadLine();

    using AppDataContext context = new AppDataContext();
    var userfound = context.Users.Any(u => u.Name == name);

    if (userfound)
    {
        var loginuser = context.Users.FirstOrDefault(u => u.Name == name);


        if (HashPassword($"{password}{loginuser.Salt}") == loginuser.Password)
        {

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Login Successful");
            Console.ReadLine();

        }
        else
        {

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Login Failed");
            Console.ReadLine();
        }
    }
}