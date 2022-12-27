using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace MamadousChatServer
{
    public class Server
    {
        public IPAddress ipAddress;//The server and the client both need to be using the same values for IP adress and port.
        public int port;
        public bool serverRunning = true;
        public TcpListener tcpListener;//tcpListener basically has functions to listen for and accept incomming connection requests. Either a 
        public Socket clientSocket;//TcpClient or a Socket can be used to connect to it.


        public NetworkStream networkStream;//Network stream provides methods for sending and receiving data. Sever and Client both need to use it.
        public StreamReader streamReader;//Has some functions to allow us to read what message a client has sent over.
        public StreamWriter streamWriter;//Lets us send messages.


        public Server(IPAddress ipAdress, int port)
        {
            this.ipAddress = ipAdress;
            this.port = port;
        }
        public void ListenForIncomingRequests()
        {
            try
            {
                //tcpListener has functions to listen for and accept incoming connection requests.
                tcpListener = new TcpListener(ipAddress, port);
                tcpListener.Start();
            }
            catch
            {
                Console.WriteLine("Start has failed, something is wrong with the ListenForIncomingRequests function");
            }
        }

        public void AcceptClient()
        {
            try
            {
                clientSocket = tcpListener.AcceptSocket();
            }
            catch
            {
                Console.WriteLine("Start has failed, something is wrong with the AcceptClient function");

            }
        }

        //Allows the server to commune with the client.
        public void ClientData()
        {
            networkStream = new NetworkStream(clientSocket);
            streamReader = new StreamReader(networkStream);
            streamWriter = new StreamWriter(networkStream);
        }

        public void Disconnect()
        {
            networkStream.Close();
            streamWriter.Close();
            streamReader.Close();
            clientSocket.Close();
        }
    }
}