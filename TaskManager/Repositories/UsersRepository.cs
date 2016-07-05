namespace TaskManager.Repositories
{
    using System;
    using System.IO;
    using System.Collections.Generic;
    using TaskManager.Entites;

    public class UsersRepository : BaseController<UserEntity>
    {
        public UsersRepository(string pathToFile)
            : base(pathToFile)
        {
        }

        protected override void WriteItemToStream(StreamWriter sw, UserEntity item)
        {
            sw.WriteLine(item.FirstName);
            sw.WriteLine(item.LastName);
            sw.WriteLine(item.Username);
            sw.WriteLine(item.Password);
            sw.WriteLine(item.AdminStatus);
        }

        protected override void ReadItemFromStream(StreamReader sr, UserEntity item)
        {
            item.FirstName = sr.ReadLine();
            item.LastName = sr.ReadLine();
            item.Username = sr.ReadLine();
            item.Password = sr.ReadLine();
            item.AdminStatus = Convert.ToBoolean(sr.ReadLine());
        }

        public UserEntity GetByUsernameAndPassword(string username, string password)
        {
            return GetAll().Find(u => u.Username == username && u.Password == password);
        }
    }
}
