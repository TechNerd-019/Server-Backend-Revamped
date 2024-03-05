using System;
using System.Text;

/*
* Packet header for use with server and client.
*/

/*
* A packet needs to have a header. Body and tail are optional.
* Header tells us everything we need to know to read the data.
*/

enum ServerMessage { LOGIN, SIGNUP, POST_THREAD }

struct PacketHeader
{
    public byte sourceAddress;
    public byte destinationAddress;
    public byte messageLength;
    public int sizeString1;
}


class Packet
{
    private PacketHeader PktHeader;
    private byte[] dataField; // Body of Packet.
    private ushort CRC;      // Tail of Packet.

    int headerSize = sizeof(byte) * 3 + sizeof(int);

    private byte[] sentData;

    private void LogInParameters()
    {
        string username;
        string password;
    }

    private void SignUpParameters()
    {
        string email;
        string username;
        string password;
    }

    private void PostParameters()
    {
        string title;
        string content;
        byte[] imageBuffer;
    }

    public void ProcessMessage(ServerMessage messageType)
    {
        switch (messageType)
        {
            case ServerMessage.LOGIN:
                // Add function call;
                break;
            case ServerMessage.SIGNUP:
                // Add function call;
                break;
            case ServerMessage.POST_THREAD:
                // Add function call;
                break;
        }
    }

    public byte[] SerializeDataForLogin(ref int totalSize, string username, string password)
    {
        if (sentData != null)
        {
            Array.Clear(sentData, 0, sentData.Length);
        }

        // Calculate the total size of the packet
        totalSize = headerSize + username.Length + password.Length + sizeof(ushort);

        sentData = new byte[totalSize];

        // Copy the packet header fields
        sentData[0] = PktHeader.sourceAddress;
        sentData[1] = PktHeader.destinationAddress;
        sentData[2] = PktHeader.messageLength;
        sentData[3] = (byte)PktHeader.sizeString1;

        // Copy the username and password strings
        Encoding.ASCII.GetBytes(username).CopyTo(sentData, 4);
        Encoding.ASCII.GetBytes(password).CopyTo(sentData, 4 + username.Length);

        // Copy the CRC field
        Array.Copy(BitConverter.GetBytes(CRC), 0, sentData, 4 + username.Length + password.Length, sizeof(ushort));

        return sentData;
    }
}
