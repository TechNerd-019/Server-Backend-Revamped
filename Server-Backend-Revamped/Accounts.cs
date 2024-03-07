class accountInfrastructure
{
        /*
        * This class implements common attributes
        * between all types of users in our system.
        */

        string fName;
        string lName;
        string username;
        string password;

}

class sellers : accountInfrastructure
{
        string businessAddress;
        string province;
        string zipCode;
        int phoneNumber;
        string licenseNumber;
        bool vaccinationStatus; // 1 - vaxxed, 0 - not vaxxed.
        bool felineType;        // 1 - cat,    0, - dog.
}

class sellersPets : sellers
{
        string petNames;
        int[] petAges;

}