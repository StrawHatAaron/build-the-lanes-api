using System;
using System.Collections.Generic;
using System.Linq;
using BuildTheLanesAPI.Helpers;
using BuildTheLanesAPI.Models;
using BuildTheLanesAPI.Controllers;


namespace BuildTheLanesAPI.Services
{
    public interface IStaffsService
    {
        // Users Authenticate(string email, string password);
        IEnumerable<Staffs> GetAll();
        Staffs GetByKey(Staffs ec);
        Staffs GetById(int id);
        void CheckValues(Staffs a);
    }

    public class StaffsService : IStaffsService
    {
        private WebappContext _context;

        public StaffsService(WebappContext context)
        {
            _context = context;
        }

        public IEnumerable<Staffs> GetAll()
        {
            return _context.Staffs; 
        }

        public Staffs GetByKey(Staffs ec)
        {
            CheckValues(ec);
            var targetAdmin = _context.Staffs.SingleOrDefault(x => x.Email == ec.Email);
            if (targetAdmin == null)
                throw new AppException($"Admin with email:'{ec.Email} DNE.");
            return targetAdmin;
        }

        public Staffs GetById(int id){
            var targetAdmin = _context.Staffs.SingleOrDefault(x => x.Id == id);
            CheckValues(targetAdmin);
            return targetAdmin;
        }

        public void CheckValues(Staffs a)
        {
            if (string.IsNullOrWhiteSpace(a.Email))
                throw new AppException("Email is required to finish inserting this record");

            if (string.IsNullOrWhiteSpace(a.Email))
                throw new AppException("Please enter a value to insert " + a.Email + "'s  certification in the record");
        }
    }
}

