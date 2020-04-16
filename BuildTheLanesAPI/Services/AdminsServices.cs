using System;
using System.Collections.Generic;
using System.Linq;
using BuildTheLanesAPI.Helpers;
using BuildTheLanesAPI.Models;
using BuildTheLanesAPI.Controllers;


namespace BuildTheLanesAPI.Services
{
    public interface IAdminsService
    {
        // Users Authenticate(string email, string password);
        IEnumerable<Admins> GetAll();
        Admins GetByKey(Admins ec);
        Admins GetById(int id);
        void CheckValues(Admins a);
    }

    public class AdminsService : IAdminsService
    {
        private WebappContext _context;

        public AdminsService(WebappContext context)
        {
            _context = context;
        }

        public IEnumerable<Admins> GetAll()
        {
            return _context.Admins; 
        }

        public Admins GetByKey(Admins ec)
        {
            CheckValues(ec);
            var targetAdmin = _context.Admins.SingleOrDefault(x => x.Email == ec.Email);
            if (targetAdmin == null)
                throw new AppException($"Admin with email:'{ec.Email} DNE.");
            return targetAdmin;
        }

        public Admins GetById(int id){
            var targetAdmin = _context.Admins.SingleOrDefault(x => x.Id == id);
            CheckValues(targetAdmin);
            return targetAdmin;
        }

        public void CheckValues(Admins a)
        {
            if (string.IsNullOrWhiteSpace(a.Email))
                throw new AppException("Email is required to finish inserting this record");
        }
    }
}

