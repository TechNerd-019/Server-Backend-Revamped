class globalCredentials
{
    public string username;
    public string password;

    // Constructor:
    public globalCredentials(string username, string password)
    {
        this.username = username;
        this.password = password;
    }
}


class accountInfrastructure : globalCredentials
{
    /*
    * This class implements common attributes
    * between all types of users in our system.
    */

    public string fName;
    public string lName;

    // Inherited constructors require the user to derive
    // the base value from the parent class. Otherwise, you'll
    // get an error saying that there is no initialized
    // attribute for each of the attributes in the parent class.

    public accountInfrastructure(string username, string password, string fName, string lName)
        : base(username, password)
    {
        this.fName = fName;
        this.lName = lName;
    }
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

    public sellers(string username, string password, string fName, string lName,
                   string businessAddress, string province, string zipCode,
                   int phoneNumber, string licenseNumber, bool vaccinationStatus, bool felineType)
        : base(username, password, fName, lName)
    {
        this.businessAddress = businessAddress;
        this.province = province;
        this.zipCode = zipCode;
        this.phoneNumber = phoneNumber;
        this.licenseNumber = licenseNumber;
        this.vaccinationStatus = vaccinationStatus;
        this.felineType = felineType;
    }
}


class sellersPets : sellers
{
    public string petNames;
    public int[] petAges;

    public byte[] imageBuffer;

    public sellersPets(string username, string password, string fName, string lName,
                       string businessAddress, string province, string zipCode,
                       int phoneNumber, string licenseNumber, bool vaccinationStatus, bool felineType,
                       string petNames, int[] petAges, byte[] imageBuffer)
        : base(username, password, fName, lName, businessAddress, province, zipCode, phoneNumber, licenseNumber, vaccinationStatus, felineType)
    {
        this.petNames = petNames;
        this.petAges = petAges;
        this.imageBuffer = imageBuffer;
    }

}


