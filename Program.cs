
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

namespace AsyncListenerTcp
{
    public class Program
    {
        static void Main()
        {
            Prompt();
            Host.server.Start();
            Client.StartClient();
            Console.ReadLine();
        }
        static void Prompt()
        {
            Console.WriteLine(@"
            ///////////////////////////////////////////////////////////////////////`
            ///                                                                 ///`
            ///   ||     || ||||||| |||||||| ||||||| |\\   || ||||||| |||||||   ///`
            ///   ||     || ||         ||    ||      ||\\  || ||      ||   ||   ///`
            ///   ||     || |||||||    ||    |||||   || \\ || |||||   |||||||   ///`
            ///   ||     ||      ||    ||    ||      ||  \\|| ||      ||  \\    ///`
            ///   |||||| || |||||||    ||    ||||||| ||   \\| ||||||| ||   \\   ///`
            ///                                                                 ///`
            ///////////////////////////////////////////////////////////////////////`
            ````````````````````````````````````````````````````````````````````````
                                                                            " + " \n");
            Console.Write("Please enter a port to listen to: ");
            Host.GetHostInfo();
            Console.WriteLine("\nListening on...");
            Console.WriteLine($"\n{Host.host} {Host.ip} {Host.port}\n");
        }
    }
    public static class Host
    {
        public static TcpListener server;
        public static IPAddress ip;
        public static int port;
        public static string host;

        public static void GetHostInfo()
        {
            host = Dns.GetHostName();
            IPHostEntry ipList = Dns.GetHostEntry(host);
            ip = ipList.AddressList[6];
            port = Int32.Parse(Console.ReadLine());
            server = new TcpListener(ip, port);
        }
        

    }
    public class Client
    {
        public static string input;
        public static StreamReader reader;
        public static StreamWriter writer;
        public static Stream stream;
        public static TcpClient client;

        public static async void StartClient()
        {
            while (true)
            {
                using (client = Host.server.AcceptTcpClient())
                {
                    using (stream = client.GetStream())
                    {
                        using (reader = new StreamReader(stream))
                        {
                            using (writer = new StreamWriter(stream))
                            {
                                writer.AutoFlush = true;

                                if (client.Connected)
                                {
                                    Console.WriteLine("Connected!\n"); 
                                }
                                while (true)
                                {
                                    Task _ = ReceiveMessage();

                                    // Send
                                    Console.Write($"=>  ");
                                    input = Console.ReadLine();
                                    await writer.WriteLineAsync(input);

                                    // Receive
                                    
                                }
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
