namespace TaskManager
{
    using TaskManager.Services;
    using TaskManager.Views;
    using System;
    using System.Globalization;
    using System.Threading;
    class App
    {
        static void Main(string[] args)
        {
            //List<string> asd = new List<string>();
            //asd.Add("ivan");
            //asd.Add("asen");
            //asd.Add("ivan");
            //Console.WriteLine(string.Join(",", asd));

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
                tasksManagementView.Show();
            }

            //DateTime date = DateTime.Now;
            //string asdasd = date.ToString(@"dd-MM-yyyy hh:mm:ss", CultureInfo.InvariantCulture);
            //DateTime date2 = Convert.ToDateTime(asdasd);
            //Console.WriteLine(date2);

            //DateTime dat1e = Convert.ToDateTime(asdasd);
            //Console.WriteLine(dat1e);
            //Console.Read();


        }
    }
}
