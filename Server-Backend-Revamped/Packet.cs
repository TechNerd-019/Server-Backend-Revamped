using System;
using System.Text;

/*
* Packet header for use with server and client.
*/

/*
* A packet needs to have a header. Body and tail are optional.
* Header tells us everything we need to know to read the data.
*/

enum ServerMessage { LOGIN, SIGNUP_SELLER, SIGNUP_BUSINESS, POST_THREAD }

struct PacketHeader
{
    public byte sourceAddress;
    public byte destinationAddress;
    public byte messageLength;
}

class Packet
{
    private PacketHeader PktHeader;
    private byte[] dataField; // Body of Packet. Will be used in constructor when received by client.
    private ushort CRC;      // Tail of Packet.

    int headerSize = sizeof(byte) * 3;

    private byte[] sentData;

    public void ProcessMessage(ServerMessage messageType)
    {
        switch (messageType)
        {
            case ServerMessage.LOGIN:
                // Add function call;
                break;
            case ServerMessage.SIGNUP_SELLER:
                // Add function call;
                break;
            case ServerMessage.SIGNUP_BUSINESS:
                // Add function call;
                break;

            case ServerMessage.POST_THREAD:
                // Add function call;
                break;
        }
    }

    // To be called by client.
    public void setData(byte[] receivedData)
    {
        PktHeader.sourceAddress = receivedData[0];
        PktHeader.destinationAddress = receivedData[1];
        PktHeader.messageLength = receivedData[2];

        // Calculate the length of the dataField
        int dataFieldLength = receivedData.Length - headerSize - sizeof(ushort);

        // Copy the dataField from the receivedData
        dataField = new byte[dataFieldLength];
        Array.Copy(receivedData, headerSize, dataField, 0, dataFieldLength);

        // Copy the CRC from the receivedData
        byte[] crcBytes = new byte[sizeof(ushort)];
        Array.Copy(receivedData, receivedData.Length - sizeof(ushort), crcBytes, 0, sizeof(ushort));
        CRC = BitConverter.ToUInt16(crcBytes, 0);
    }

    public byte[] SerializeDataForLogin(ref int totalSize, globalCredentials credentials)
    {
        if (sentData != null)
        {
            Array.Clear(sentData, 0, sentData.Length);
        }

        // Calculate the total size of the packet
        totalSize = headerSize + credentials.username.Length + credentials.password.Length + sizeof(ushort);
                                                                                             // CRC.

        sentData = new byte[totalSize];

        // Copy the packet header fields
        sentData[0] = PktHeader.sourceAddress;
        sentData[1] = PktHeader.destinationAddress;
        sentData[2] = PktHeader.messageLength;

        // Calculate the offset for copying fields
        int offset = 4;

        // Copy the username and password strings
        Encoding.ASCII.GetBytes(credentials.username).CopyTo(sentData, offset);
        offset += credentials.username.Length;
        Encoding.ASCII.GetBytes(credentials.password).CopyTo(sentData, offset);

        // Copy the CRC field
        offset += credentials.password.Length;
        Array.Copy(BitConverter.GetBytes(CRC), 0, sentData, offset, sizeof(ushort));

        return sentData;
    }

    public byte[] SerializeDataForSignUpForSellers(ref int totalSize, accountInfrastructure credentials, sellers sellerCreds, sellersPets sellerPets)
    {
        if (sentData != null)
        {
            Array.Clear(sentData, 0, sentData.Length);
        }

        // Calculate the total size of the packet.
        /*
        * This is done by manually adding up the bytes of each attribute
        * of the class.
        *
        *
        * sizeof(class) does not work since an error is thrown stating
        * that the class does not have a predefined size, therefore
        * "sizeof" can only be used in an unsafe context.
        */
        totalSize = headerSize +
                    Encoding.ASCII.GetByteCount(credentials.username) +
                    Encoding.ASCII.GetByteCount(credentials.password) +
                    Encoding.ASCII.GetByteCount(credentials.fName) +
                    Encoding.ASCII.GetByteCount(credentials.lName) +
                    Encoding.ASCII.GetByteCount(sellerCreds.businessAddress) +
                    Encoding.ASCII.GetByteCount(sellerCreds.province) +
                    Encoding.ASCII.GetByteCount(sellerPets.petNames) +
                    (sizeof(int) * sellerPets.petAges.Length) +
                    sellerPets.imageBuffer.Length +
                    sizeof(ushort); // CRC

        sentData = new byte[totalSize];

        // Copy the packet header fields
        sentData[0] = PktHeader.sourceAddress;
        sentData[1] = PktHeader.destinationAddress;
        sentData[2] = PktHeader.messageLength;

        // Calculate the offset for copying fields
        int offset = 4;

        /*
        * Here, each of the attributes is manually copied to the sentData buffer.
        */
        Encoding.ASCII.GetBytes(credentials.username).CopyTo(sentData, offset);
        offset += credentials.username.Length;
        Encoding.ASCII.GetBytes(credentials.password).CopyTo(sentData, offset);
        offset += credentials.password.Length;

        
        Encoding.ASCII.GetBytes(credentials.fName).CopyTo(sentData, offset);
        offset += credentials.fName.Length;
        Encoding.ASCII.GetBytes(credentials.lName).CopyTo(sentData, offset);
        offset += credentials.lName.Length;

        // Copy the business address and province
        Encoding.ASCII.GetBytes(sellerCreds.businessAddress).CopyTo(sentData, offset);
        offset += sellerCreds.businessAddress.Length;
        Encoding.ASCII.GetBytes(sellerCreds.province).CopyTo(sentData, offset);
        offset += sellerCreds.province.Length;

        // Copy the pet names and ages
        Encoding.ASCII.GetBytes(sellerPets.petNames).CopyTo(sentData, offset);
        offset += sellerPets.petNames.Length;

        for (int i = 0; i < sellerPets.petAges.Length; i++)
        {
            Array.Copy(BitConverter.GetBytes(sellerPets.petAges[i]), 0, sentData, offset, sizeof(int));
            offset += sizeof(int);
        }

        // Copy the image buffer
        Array.Copy(sellerPets.imageBuffer, 0, sentData, offset, sellerPets.imageBuffer.Length);
        offset += sellerPets.imageBuffer.Length;

        // Copy the CRC field
        Array.Copy(BitConverter.GetBytes(CRC), 0, sentData, offset, sizeof(ushort));

        return sentData;
    }      


}
