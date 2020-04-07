using System;
namespace BuildTheLanesAPI.Entities
{
    public static class Roles
    {
        public const string User = "User"; /*in the database User is Users*/
        public const string Donators = "d";
        public const string Staff = "s";
        public const string Admin = "a";
        public const string Engineer = "e";
        public const string StaffDonator = "ed";
        public const string AdminDonator = "ad";
        public const string EngineerDonator = "ed";
        //Groups
        
        public const string AllStaff = Staff + "," + StaffDonator;
        public const string AllAdmin = Admin + "," + AdminDonator;
        public const string AllEngineer = Engineer + "," + EngineerDonator;
        public const string AllDonator = Donators + "," + StaffDonator + "," +
            AdminDonator + "," + EngineerDonator;
        public const string AllUser = Staff + "," + Admin + "," + Engineer + "," +
            StaffDonator + "," + AdminDonator + "," + EngineerDonator;

    }
}
