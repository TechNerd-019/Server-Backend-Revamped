// Libraries needed to create a server socket in C#
using System;
using System.Net;
using System.Net.Sockets;


public class ServerListenerBackend
{
    static void Main(string[] args)
    {

        // Specify the server port and IP address to be used.
        Int32 port = 27004;
        IPAddress localAddr = IPAddress.Parse("127.0.0.1");
        IPEndPoint localEndPoint = new IPEndPoint(localAddr, port);

        try
        {
            // The socket is declared and initialized with the required parameters.
            Socket server = new Socket(localAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            // We bind the socket to the port on the operating system.
            server.Bind(localEndPoint);
            // How many requests at a time am I processing?
            server.Listen(5);

            Console.WriteLine("The server is now waiting for incoming connections...");
            Socket connectionSocket = server.Accept();

            /*
             * TO-DO: Add packet header and serialize data.
             */
        }
        catch (Exception exc)
        {
            Console.WriteLine(exc.ToString());
        }
    }
}
