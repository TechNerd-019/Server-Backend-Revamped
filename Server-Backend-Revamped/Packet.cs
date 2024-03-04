using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

public class Packet
{

/*
* These enumerators are implemented client and server side to determine
* what will be serialized and sent to my client and server.
*/
    enum contentType
    {
        LOGIN,
        SIGNUP,
        POST
    }

    struct Header
    {
        char Source;
        char DestAddress;
        int NumOfBytes;
        int flag;

    }

    struct BPacket
    {
        Header header;
       // char* DataField; This is not possible in C#.
       byte[] DataField; // Used in its place.

       ushort CRC;


    }

    struct SignIn
    {
        string username;
        string password;

    }

    struct SignUp
    {
        string email;
        string username;
        string password;
    }

    struct POST
    {
        string postTitle;
        string postContent;

        byte[] ImageBuffer;
        
    }





}