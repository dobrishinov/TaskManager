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
            UserManagmentEnum choice = RenderMenu();
        }

        private UserManagmentEnum RenderMenu()
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
                            return UserManagmentEnum.Select;
                        }
                    case "V":
                        {
                            return UserManagmentEnum.View;
                        }
                    case "A":
                        {
                            return UserManagmentEnum.Insert;
                        }
                    case "E":
                        {
                            return UserManagmentEnum.Update;
                        }
                    case "D":
                        {
                            return UserManagmentEnum.Delete;
                        }
                    case "X":
                        {
                            return UserManagmentEnum.Exit;
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
