using System;

namespace ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length > 1)
            {
                //e.g. var headers = headerGenerator.GenerateHeaders("https://d-{url}/applications/1700000001", "GET");
                
                var url = args[0];
                var method = args[1];
                var headerGenerator = new HMACAuthenticationHeaderGenerator();
                var headers = headerGenerator.GenerateHeaders(url, method);

                var AuthHeader = headers["Authorization"];
                var DateHeader = headers["Date"];

                NewLine();
                Console.WriteLine("URL: " + args[0]);
                Console.WriteLine("METHOD: " + args[1]);
                NewLine();
                Console.WriteLine("Authorization: " + AuthHeader);
                Console.WriteLine("Date: " + DateHeader);
                NewLine();
            }

            else
            {
                Console.WriteLine("Need a URL and a method to generate HMAC key!");
                NewLine();                
            }
        }

        private static void NewLine()
        {
            Console.WriteLine();
        }
    }
}
