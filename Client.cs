using System;
using System.Text;
using System.Linq;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;


namespace AsyncClient
{
    class Program
    {
        static void Main()
        {
            Prompt();
            Client.GetClientInfo();
            Client.StartClient();
            Console.ReadLine();
        }

        static void Prompt()
        {
            Console.WriteLine(@"
            ////////////////////////////////////////////////////////////`
            ///                                                      ///`
            ///   |||||||  ||      ||  ||||||| |\\   || ||||||||||   ///`
            ///   ||       ||      ||  ||      ||\\  ||     ||       ///`
            ///   ||       ||      ||  |||||   || \\ ||     ||       ///`
            ///   ||       ||      ||  ||      ||  \\||     ||       ///`
            ///   |||||||  ||||||  ||  ||||||| ||   \\|     ||       ///`
            ///                                                      ///`
            ////////////////////////////////////////////////////////////`
            ````````````````````````````````````````````````````````````` "+"\n");
            Console.WriteLine("Waiting for connection...\n");
            Console.Write("Please enter a port number: ");
        }
    }
    public class Client
    {
        public static string host;
        public static IPAddress ip;
        public static int port;
        public static TcpClient client;
        public static Stream stream;
        public static StreamReader reader;
        public static StreamWriter writer;
        public static string input;

        public static void GetClientInfo()
        {
            host = Dns.GetHostName();
            IPHostEntry ipList = Dns.GetHostEntry(host);
            ip = ipList.AddressList[6];
            port = Int32.Parse(Console.ReadLine());
        }
        public static async void StartClient()
        {
            using (client = new TcpClient("10.0.0.125", port))
            {
                using (stream = client.GetStream())
                {
                    using (reader = new StreamReader(stream))
                    {
                        using (writer = new StreamWriter(stream))
                        {
                            writer.AutoFlush = true;
                            if (client.Connected)
                                Console.WriteLine("\nConnected!\n");
                            while (true)
                            {
                                Task _ = ReceiveMessage();

                                // Write
                                Console.Write("=>  ");
                                input = Console.ReadLine();
                                await writer.WriteLineAsync(input);

                                // Read
                                
                            }
                        }
                    }
                }
            }
        }
        
        public static async Task ReceiveMessage()
        {
            while (true)
            {
                Console.Write($"{await reader.ReadLineAsync()}\n=>  ");
            }
        }
        
    }
}
