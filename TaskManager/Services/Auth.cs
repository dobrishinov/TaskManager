namespace TaskManager.Services
{
    using System;
    using TaskManager.Repositories;
    using TaskManager.Entites;

    public static class Auth
    {
        public static UserEntity LoggedUser { get; private set; }

        public static void AuthenticateUser(string username, string password)
        {
            UsersRepository usersRepository = new UsersRepository("users.txt");
            Auth.LoggedUser = usersRepository.GetByUsernameAndPassword(username, password);
        }
    }
}
