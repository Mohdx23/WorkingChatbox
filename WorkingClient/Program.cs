using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Hasan_Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string myIp = "192.168.0.150";
            int port = 3010;

            Client client = new Client(myIp, port);

            client.ConnectToServer();
            Console.WriteLine("Connected to the server");

            Thread.Sleep(1000);
            Console.Clear();

            client.ServerData();

            try
            {
                string messageToServer = "";
                string messageFromServer = "";

                while (client.clientStatus)
                {
                    Console.WriteLine(DateTime.Now);
                    //The message will be whatever we type on the keyboard;
                    messageToServer = Console.ReadLine();
                        client.streamWriter.WriteLine(messageToServer);//Send to the server whatever the client wrote.
                        client.streamWriter.Flush();

                        messageFromServer = client.streamReader.ReadLine();
                        Console.WriteLine("Server Message: " + messageFromServer);

                    
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Not connected to the server anymore, check serverStatus");
            }

            client.Disconnect();
        }
    }
}

