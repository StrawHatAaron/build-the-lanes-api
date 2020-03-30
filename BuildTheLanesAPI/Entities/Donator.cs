using System;
namespace BuildTheLanesAPI.Entities
{
    public class Donator : User
    {
        public decimal AmountDonated { get; set; }
    }
}
