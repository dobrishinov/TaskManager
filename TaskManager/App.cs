namespace TaskManager
{
    using TaskManager.Services;
    using TaskManager.Views;
    using System;

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
                Console.WriteLine("Not admin");
            }
        }
    }
}
