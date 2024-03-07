class globalCredentials
{
    public string username;
    public string password;
}


class accountInfrastructure : globalCredentials
{
    /*
    * This class implements common attributes
    * between all types of users in our system.
    */

    public string fName;
    public string lName;
}


class sellers : accountInfrastructure
{
    public string businessAddress;
    public string province;
    public string zipCode;
    public int phoneNumber;
    public string licenseNumber;
    public bool vaccinationStatus; // 1 - vaxxed, 0 - not vaxxed.
    public bool felineType;        // 1 - cat,    0, - dog.
}


class sellersPets : sellers
{
    public string petNames;
    public int[] petAges;

    public byte[] imageBuffer;
}
