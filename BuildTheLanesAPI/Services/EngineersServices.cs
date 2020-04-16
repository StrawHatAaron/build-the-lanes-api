using System;
using System.Collections.Generic;
using System.Linq;
using BuildTheLanesAPI.Helpers;
using BuildTheLanesAPI.Models;
using BuildTheLanesAPI.Controllers;


namespace BuildTheLanesAPI.Services
{
    public interface IEngineersService
    {
        // Users Authenticate(string email, string password);
        IEnumerable<Engineers> GetAll();
        Engineers GetByKey(Engineers ec);
        Engineers GetById(int id);
        void CheckValues(Engineers a);
    }

    public class EngineersService : IEngineersService
    {
        private WebappContext _context;

        public EngineersService(WebappContext context)
        {
            _context = context;
        }

        public IEnumerable<Engineers> GetAll()
        {
            return _context.Engineers; 
        }

        public Engineers GetByKey(Engineers ec)
        {
            CheckValues(ec);
            var targetEngineer = _context.Engineers.SingleOrDefault(x => x.Email == ec.Email);
            if (targetEngineer == null)
                throw new AppException($"Engineer with email:'{ec.Email} DNE.");
            return targetEngineer;
        }

        public Engineers GetById(int id){
            var targetEngineer = _context.Engineers.SingleOrDefault(x => x.Id == id);
            CheckValues(targetEngineer);
            return targetEngineer;
        }

        public void CheckValues(Engineers a)
        {
            if (string.IsNullOrWhiteSpace(a.Email))
                throw new AppException("Email is required to finish inserting this record");
        }
    }
}

