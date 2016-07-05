namespace TaskManager.Views
{
    using System;
    using TaskManager.Repositories;
    using TaskManager.Entites;
    using TaskManager.Services;

    class LoginView
    {
        public void Show()
        {
            while (true)
            {
                Console.Clear();

                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("##############|LOGIN|##############");
                Console.ResetColor();

                Console.WriteLine("Enter Username:");
                string username = Console.ReadLine();

                Console.WriteLine("Enter password:");
                string password = Console.ReadLine();

                Auth.AuthenticateUser(username, password);

                if (Auth.LoggedUser != null)
                {
                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("Welcome " + Auth.LoggedUser.Username + "! Press Key to load Menu!");
                    Console.ResetColor();
                    Console.ReadKey(true);
                    break;
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.WriteLine("Wrong credentials, try again!");
                    Console.ResetColor();

                    Console.ReadKey(true);
                }
            }
        }
    }
}
