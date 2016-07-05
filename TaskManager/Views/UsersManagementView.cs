namespace TaskManager.Views
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using TaskManager.Services;


    public class UsersManagementView
    {
        public void Show()
        {
            UserManagementEnum choice = RenderMenu();
        }

        private UserManagementEnum RenderMenu()
        {
            while (true)
            {
                Console.Clear();

                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("##############|User management|##############");
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
                            Console.WriteLine("Invalid choice.");
                            Console.ReadKey(true);
                            break;
                        }
                }
            }
        }
    }
}
