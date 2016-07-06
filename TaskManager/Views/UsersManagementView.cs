namespace TaskManager.Views
{
    using System;
    using TaskManager.Entites;
    using TaskManager.Repositories;
    using TaskManager.Services;
    using System.Collections.Generic;

    public class UsersManagementView
    {
        public void Show()
        {
            while (true)
            {
                UserManagementEnum choice = RenderMenu();

                try
                {
                    switch (choice)
                    {
                        case UserManagementEnum.Select:
                            {
                                GetAll();
                                break;
                            }
                        case UserManagementEnum.View:
                            {
                                View();
                                break;
                            }
                        case UserManagementEnum.Insert:
                            {
                                Add();
                                break;
                            }
                        case UserManagementEnum.Update:
                            {
                                Update();
                                break;
                            }
                        case UserManagementEnum.Delete:
                            {
                                Delete();
                                break;
                            }
                        case UserManagementEnum.Exit:
                            {
                                return;
                            }
                    }
                }
                catch (Exception ex)
                {
                    Console.Clear();
                    Console.WriteLine(ex.Message);
                    Console.ReadKey(true);
                }
            }
        }

        private UserManagementEnum RenderMenu()
        {
            while (true)
            {
                Console.Clear();

                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("#############|User management|##############");
                Console.ResetColor();

                Console.WriteLine("[G]et all Users");
                Console.WriteLine("[V]iew User");
                Console.WriteLine("[A]dd User");
                Console.WriteLine("[E]dit User");
                Console.WriteLine("[D]elete User");
                Console.WriteLine("E[x]it");

                string choice = Console.ReadLine();
                switch (choice.ToUpper())
                {
                    case "G":
                        {
                            return UserManagementEnum.Select;
                        }
                    case "V":
                        {
                            return UserManagementEnum.View;
                        }
                    case "A":
                        {
                            return UserManagementEnum.Insert;
                        }
                    case "E":
                        {
                            return UserManagementEnum.Update;
                        }
                    case "D":
                        {
                            return UserManagementEnum.Delete;
                        }
                    case "X":
                        {
                            return UserManagementEnum.Exit;
                        }
                    default:
                        {
                            Console.BackgroundColor = ConsoleColor.Red;
                            Console.WriteLine("Invalid Choice");
                            Console.ReadKey(true);
                            Console.ResetColor();
                            break;
                        }
                }
            }
        }

        private void GetAll()
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

        private void View()
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

        private void Add()
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

        private void Update()
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

        private void Delete()
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
