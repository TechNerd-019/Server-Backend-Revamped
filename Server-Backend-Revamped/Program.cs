using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class Client
{
    public static void Main()
    {
        try
        {
            // Specifying the server's IP address and port.
            IPAddress serverIP = IPAddress.Parse("127.0.0.1");
            int serverPort = 27004;
            IPEndPoint serverEndPoint = new IPEndPoint(serverIP, serverPort);

            // Creating a socket for the client.
            Socket clientSocket = new Socket(serverIP.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            // Connecting to the server.
            clientSocket.Connect(serverEndPoint);
            Console.WriteLine("Connected to the server.");

            // Start sending and receiving messages.
            while (true)
            {
                //Some part of messaging feature
                // Sending a message to the server.
                Console.Write("Enter your message: ");
                string message = Console.ReadLine();
                byte[] messageBytes = Encoding.ASCII.GetBytes(message);
                clientSocket.Send(messageBytes);

                // Receiving a message from the server.
                byte[] buffer = new byte[1024];
                int bytesRead = clientSocket.Receive(buffer);
                string receivedMessage = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                Console.WriteLine("Server: " + receivedMessage);
            }

            // Closing the socket.
            clientSocket.Shutdown(SocketShutdown.Both);
            clientSocket.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: " + ex.Message);
        }
    }
}

