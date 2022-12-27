using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Timers;


namespace MamadousChatServer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IPAddress ipAddress = IPAddress.Parse("192.168.0.150");
            int port = 3010;
       
            Server server = new Server(ipAddress, port);
            

            server.ListenForIncomingRequests();
            Console.WriteLine("JARVIS: The server has been initiated!");
            Thread.Sleep(1500);
            Console.Clear();
            Console.WriteLine("Awaiting incoming client requests");
            
            server.AcceptClient();

            Console.WriteLine("JARVIS: A Client has connected");

            string messageFromClient = "";
            string messageToClient = "";

            try
            {
                server.ClientData();

                while (server.serverRunning)
                {
                    //Only when a client is connected.
                    if (server.clientSocket.Connected)
                    {
                        Console.WriteLine(DateTime.Now);
                        
                        //The client has to be able to write a message...
                        messageFromClient = server.streamReader.ReadLine();
                        Console.WriteLine("Client : " + messageFromClient);

                        //After the client says something, I can now say something.
                        Console.WriteLine("Server: ");
                        messageToClient = Console.ReadLine();
                        //Send my message to the client.
                        server.streamWriter.WriteLine(messageToClient);
                        server.streamWriter.Flush();
                    }
                    
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("EXCEPTION MESSAGE: " + ex.Message);

            }
        }

   
    }
}