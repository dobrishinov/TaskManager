namespace TaskManager
{
    using TaskManager.Services;
    using TaskManager.Views;
    class App
    {
        static void Main(string[] args)
        {
            LoginView loginView = new LoginView();
            loginView.Show();

            if (Auth.LoggedUser.AdminStatus)
            {
                AdministratorView administratorView = new AdministratorView();
                administratorView.Show();
            }
            else
            {
                TasksManagementView tasksManagementView = new TasksManagementView();
                tasksManagementView.ShowTaskManagement();
            }
        }
    }
}
