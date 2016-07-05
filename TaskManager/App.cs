namespace TaskManager
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class App
    {
        static void Main(string[] args)
        {
            List<string> asd = new List<string>();
            asd.Add("ivan");
            asd.Add("asen");
            asd.Add("ivan");
            Console.WriteLine(string.Join(",", asd));
        }
    }
}
