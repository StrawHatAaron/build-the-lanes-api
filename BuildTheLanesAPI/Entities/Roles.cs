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
        public const string AllStaff = Staff+","+Admin+","+Engineer+","+StaffDonator+","+AdminDonator+","+EngineerDonator;
    }
}
