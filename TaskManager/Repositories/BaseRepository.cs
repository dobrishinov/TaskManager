namespace TaskManager.Repositories
{
    using TaskManager.Entites;
    using System;
    using System.Collections.Generic;
    using System.IO;

    public abstract class BaseController<T> where T : BaseEntity, new()
    {
        private readonly string pathToFile;

        public BaseController(string pathToFile)
        {
            this.pathToFile = pathToFile;
        }

        protected abstract void WriteItemToStream(StreamWriter sw, T item);
        protected abstract void ReadItemFromStream(StreamReader sr, T item);

        private int GetNextId()
        {
            FileStream fs = new FileStream(this.pathToFile, FileMode.OpenOrCreate);
            StreamReader sr = new StreamReader(fs);

            int id = 0;
            try
            {
                while (!sr.EndOfStream)
                {
                    T item = new T();
                    item.Id = Convert.ToInt32(sr.ReadLine());
                    ReadItemFromStream(sr, item);

                    if (id < item.Id)
                    {
                        id = item.Id;
                    }
                }
            }
            finally
            {
                sr.Close();
                fs.Close();
            }
            return id + 1;
        }

        private void Insert(T item)
        {
            item.Id = GetNextId();

            FileStream fs = new FileStream(pathToFile, FileMode.Append);
            StreamWriter sw = new StreamWriter(fs);

            try
            {
                sw.WriteLine(item.Id);
                WriteItemToStream(sw, item);
            }
            finally
            {
                sw.Close();
                fs.Close();
            }
        }

        private void Update(T item)
        {
            string pathToTemp = "team." + pathToFile;

            FileStream ifs = new FileStream(pathToFile, FileMode.OpenOrCreate);
            StreamReader sr = new StreamReader(ifs);

            FileStream ofs = new FileStream(pathToTemp, FileMode.OpenOrCreate);
            StreamWriter sw = new StreamWriter(ofs);

            try
            {
                while (!sr.EndOfStream)
                {
                    T tempItem = new T();
                    tempItem.Id = Convert.ToInt32(sr.ReadLine());
                    ReadItemFromStream(sr, tempItem);

                    if (tempItem.Id != item.Id)
                    {
                        sw.WriteLine(tempItem.Id);
                        WriteItemToStream(sw, tempItem);
                    }
                    else
                    {
                        sw.WriteLine(item.Id);
                        WriteItemToStream(sw, item);
                    }
                }
            }
            finally
            {
                sw.Close();
                ofs.Close();
                sr.Close();
                ifs.Close();
            }

            File.Delete(pathToFile);
            File.Move(pathToTemp, pathToFile);
        }

        public T GetById(int id)
        {
            FileStream fs = new FileStream(this.pathToFile, FileMode.OpenOrCreate);
            StreamReader sr = new StreamReader(fs);

            try
            {
                while (!sr.EndOfStream)
                {
                    T item = new T();
                    item.Id = Convert.ToInt32(sr.ReadLine());
                    ReadItemFromStream(sr, item);

                    if (item.Id == id)
                    {
                        return item;
                    }
                }
            }
            finally
            {
                sr.Close();
                fs.Close();
            }

            return null;
        }

        public List<T> GetAll()
        {
            List<T> results = new List<T>();


            FileStream fs = new FileStream(this.pathToFile, FileMode.OpenOrCreate);
            StreamReader sr = new StreamReader(fs);

            try
            {
                while (!sr.EndOfStream)
                {
                    T item = new T();
                    item.Id = Convert.ToInt32(sr.ReadLine());
                    ReadItemFromStream(sr, item);

                    results.Add(item);
                }
            }
            finally
            {
                sr.Close();
                fs.Close();
            }

            return results;
        }

        public void Delete(T item)
        {
            string pathToTemp = "temp." + pathToFile;

            FileStream ifs = new FileStream(pathToFile, FileMode.OpenOrCreate);
            StreamReader sr = new StreamReader(ifs);

            FileStream ofs = new FileStream(pathToTemp, FileMode.OpenOrCreate);
            StreamWriter sw = new StreamWriter(ofs);

            try
            {
                while (!sr.EndOfStream)
                {
                    T tempItem = new T();
                    tempItem.Id = Convert.ToInt32(sr.ReadLine());
                    ReadItemFromStream(sr, tempItem);

                    if (tempItem.Id != item.Id)
                    {
                        sw.WriteLine(tempItem.Id);
                        WriteItemToStream(sw, tempItem);
                    }
                }
            }
            finally
            {
                sw.Close();
                ofs.Close();
                sr.Close();
                ifs.Close();
            }

            File.Delete(pathToFile);
            File.Move(pathToTemp, pathToFile);
        }

        public void Save(T item)
        {
            if (item.Id > 0)
            {
                Update(item);
            }
            else
            {
                Insert(item);
            }
        }
    }
}

