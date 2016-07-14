namespace TaskManager.Views
{
    using System;
    using TaskManager.Entites;
    using TaskManager.Repositories;
    using TaskManager.Services;
    using System.Collections.Generic;

    class UsersManagementView : BaseView
    {
        public override void GetAll()
        {
            Console.Clear();

            UsersRepository usersRepository = new UsersRepository("users.txt");
            List<UserEntity> users = usersRepository.GetAll();

            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("#################|Get ALL|##################");
            Console.ResetColor();

            foreach (UserEntity user in users)
            {
                Console.WriteLine("ID: " + user.Id);
                Console.WriteLine("Username: " + user.Username);
                Console.WriteLine("Password: " + user.Password);
                Console.WriteLine("First Name: " + user.FirstName);
                Console.WriteLine("Last Name: " + user.LastName);
                Console.WriteLine("IsAdmin: " + user.AdminStatus);

                Console.WriteLine("###########################################");
            }

            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("###########|Press key to return|###########");
            Console.ResetColor();

            Console.ReadKey(true);
        }

        public override void View()
        {
            Console.Clear();

            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("###############|View By ID|################");
            Console.ResetColor();

            Console.Write("Please enter user ID: ");
            int userId = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("###########################################");

            UsersRepository usersControllers = new UsersRepository("users.txt");

            UserEntity user = usersControllers.GetById(userId);
            if (user == null)
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine("User not found. Press Key To Return!");
                Console.ResetColor();

                Console.ReadKey(true);
                return;
            }

            Console.WriteLine("ID: " + user.Id);
            Console.WriteLine("Username: " + user.Username);
            Console.WriteLine("Password: " + user.Password);
            Console.WriteLine("First Name: " + user.FirstName);
            Console.WriteLine("Last Name: " + user.LastName);
            Console.WriteLine("Admin status: " + user.AdminStatus);

            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("User found. Press Key To Return!");
            Console.ResetColor();

            Console.ReadKey(true);
        }

        public override void Add()
        {
            Console.Clear();

            UserEntity user = new UserEntity();

            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("##############|Add New User:|################");
            Console.ResetColor();
            
            Console.Write("Username: ");
            user.Username = Console.ReadLine();
            Console.Write("Password: ");
            user.Password = Console.ReadLine();
            Console.Write("First Name: ");
            user.FirstName = Console.ReadLine();
            Console.Write("Last Name: ");
            user.LastName = Console.ReadLine();
            Console.Write("Is Admin: ");
            user.AdminStatus = Convert.ToBoolean(Console.ReadLine());

            UsersRepository usersRepository = new UsersRepository("users.txt");
            usersRepository.Save(user);

            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("User saved successfully. Press Key To Return!");
            Console.ResetColor();

            Console.ReadKey(true);
        }

        public override void Update()
        {
            Console.Clear();

            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("##############|Update User:|################");
            Console.ResetColor();

            Console.Write("Please enter user ID: ");
            int userId = Convert.ToInt32(Console.ReadLine());

            UsersRepository usersRepository = new UsersRepository("users.txt");
            UserEntity user = usersRepository.GetById(userId);

            if (user == null)
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine("User not found. Press Key To Return!");
                Console.ResetColor();

                Console.ReadKey(true);
                return;
            }

            Console.WriteLine("Editing User (" + user.Username + ")");
            Console.WriteLine("User ID: " + user.Id);

            Console.WriteLine("Username: " + user.Username);
            Console.Write("New Username: ");
            string username = Console.ReadLine();
            Console.WriteLine("Password: " + user.Password);
            Console.Write("New Password: ");
            string password = Console.ReadLine();

            Console.WriteLine("First Name: " + user.FirstName);
            Console.Write("New First Name: ");
            string firstName = Console.ReadLine();

            Console.WriteLine("Last Name: " + user.LastName);
            Console.Write("New Last Name: ");
            string lastName = Console.ReadLine();

            Console.WriteLine("Admin Status: " + user.AdminStatus);
            Console.Write("New Admin Status: ");
            string isAdmin = Console.ReadLine();

            if (!string.IsNullOrEmpty(username))
                user.Username = username;
            if (!string.IsNullOrEmpty(password))
                user.Password = password;
            if (!string.IsNullOrEmpty(firstName))
                user.FirstName = firstName;
            if (!string.IsNullOrEmpty(lastName))
                user.LastName = lastName;
            if (!string.IsNullOrEmpty(isAdmin))
                user.AdminStatus = Convert.ToBoolean(isAdmin);

            usersRepository.Save(user);

            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("User saved successfully. Press Key To Return!");
            Console.ResetColor();

            Console.ReadKey(true);
        }

        public override void Delete()
        {
            UsersRepository usersRepository = new UsersRepository("users.txt");

            Console.Clear();

            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("##############|Delete User:|################");
            Console.ResetColor();
            
            Console.Write("User ID: ");
            int userId = Convert.ToInt32(Console.ReadLine());

            UserEntity user = usersRepository.GetById(userId);
            if (user == null)
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine("User not found. Press Key To Return!");
                Console.ResetColor();
            }
            else
            {
                usersRepository.Delete(user);
                Console.BackgroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("User deleted successfully!");
                Console.ResetColor();
            }
            Console.ReadKey(true);
        }
    }
}
