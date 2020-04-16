using System;
using System.Collections.Generic;
using System.Linq;
using BuildTheLanesAPI.Helpers;
using BuildTheLanesAPI.Models;
using BuildTheLanesAPI.Controllers;


namespace BuildTheLanesAPI.Services
{
    public interface IDonatorsService
    {
        // Users Authenticate(string email, string password);
        IEnumerable<Donators> GetAll();
        Donators GetByKey(Donators ec);
        Donators GetById(int id);
        void CheckValues(Donators a);
    }

    public class DonatorsService : IDonatorsService
    {
        private WebappContext _context;

        public DonatorsService(WebappContext context)
        {
            _context = context;
        }

        public IEnumerable<Donators> GetAll()
        {
            return _context.Donators; 
        }

        public Donators GetByKey(Donators ec)
        {
            CheckValues(ec);
            var targetDonator = _context.Donators.SingleOrDefault(x => x.Email == ec.Email);
            if (targetDonator == null)
                throw new AppException($"Donator with email:'{ec.Email} DNE.");
            return targetDonator;
        }

        public Donators GetById(int id){
            var targetDonator = _context.Donators.SingleOrDefault(x => x.Id == id);
            CheckValues(targetDonator);
            return targetDonator;
        }

        public void CheckValues(Donators a)
        {
            if (string.IsNullOrWhiteSpace(a.Email))
                throw new AppException("Email is required to finish inserting this record");
        }
    }
}

