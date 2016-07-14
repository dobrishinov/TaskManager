namespace TaskManager.Views
{
    using System;
    using TaskManager.Services;


    public class AdministratorView
    {
        public void Show()
        {
           while (true)
            {
                AdministratorNum choice = RenderMenu();
                try
                {
                    switch (choice)
                    {
                        case AdministratorNum.UsersManagement:
                            {
                                UsersManagementView userManagementView = new UsersManagementView();
                                userManagementView.ShowUserManagement();
                                break;
                            }
                        case AdministratorNum.TasksManagement:
                            {
                                TasksManagementView taskManagementView = new TasksManagementView();
                                taskManagementView.ShowTaskManagement();
                                break;
                            }
                        case AdministratorNum.Exit:
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

        private AdministratorNum RenderMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("##############|Administration|##############");
                Console.ResetColor();
                Console.WriteLine("Manage [U]sers");
                Console.WriteLine("Manage [T]asks");
                Console.WriteLine("E[x]it");

                string choice = Console.ReadLine();
                switch (choice.ToUpper())
                {
                    case "U":
                        {
                            return AdministratorNum.UsersManagement;
                        }
                    case "T":
                        {
                            return AdministratorNum.TasksManagement;
                        }
                    case "X":
                        {
                            return AdministratorNum.Exit;
                        }
                    default:
                        {
                            Console.BackgroundColor = ConsoleColor.Red;
                            Console.WriteLine("Invalid choice.");
                            Console.ResetColor();
                            Console.ReadKey(true);
                            break;
                        }
                }
            }
        }
    }
}
