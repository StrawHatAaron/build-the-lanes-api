using System;
using System.Collections.Generic;
using System.Linq;
using BuildTheLanesAPI.Helpers;
using BuildTheLanesAPI.Models;
using BuildTheLanesAPI.Controllers;


namespace BuildTheLanesAPI.Services
{
    public interface IEngineeringDegreeService
    {
        // Users Authenticate(string email, string password);
        IEnumerable<EngineerDegrees> GetAll();
        EngineerDegrees GetByKey(EngineerDegrees ec);
        EngineerDegrees Create(EngineerDegrees ec);
        public void Update(string newDegVal, EngineerDegrees oldDeg);
        public void Delete(EngineerDegrees ec);
    }

    public class EngineerDegreesService : IEngineeringDegreeService
    {
        private WebappContext _context;

        public EngineerDegreesService(WebappContext context)
        {
            _context = context;
        }

        public IEnumerable<EngineerDegrees> GetAll()
        {
            return _context.EngineerDegrees; 
        }

        public EngineerDegrees GetByKey(EngineerDegrees ec)
        {
            CheckValues(ec);
            var targetEngCert = _context.EngineerDegrees.SingleOrDefault(x => 
                (x.Degree == ec.Degree && x.Email == ec.Email));
            if (targetEngCert == null)
                throw new AppException($"Degree with email:'{ec.Email}' and Degree:'{ec.Degree}' DNE.");
            return targetEngCert;
        }

        public EngineerDegrees Create(EngineerDegrees ec)
        {
            var checkObj = _context.EngineerDegrees.SingleOrDefault(x => x.Email==ec.Email && x.Degree==ec.Degree);
            if(checkObj != null)
                throw new AppException($"Engineer Degree already exists for Email:{ec.Email} and Degree:{ec.Degree}");
            
             this.CheckValues(ec);
            _context.EngineerDegrees.Add(ec);
            _context.SaveChanges();
            return ec;
        }


        public void Update(string newDegVal, EngineerDegrees oldDeg)
        {
            EngineerDegrees newDeg = new EngineerDegrees();
            newDeg.Email = oldDeg.Email;
            newDeg.Degree = newDegVal;
            Delete(oldDeg);
            Create(newDeg);
            _context.SaveChanges();
        }


        public void Delete(EngineerDegrees ec)
        {
            var targetEngCert = this.GetByKey(ec);
            _context.EngineerDegrees.Remove(targetEngCert);
            _context.SaveChanges();
        }

        public void CheckValues(EngineerDegrees ec)
        {
            if (string.IsNullOrWhiteSpace(ec.Email))
                throw new AppException("Email is required to finish inserting this record.");

            if (string.IsNullOrWhiteSpace(ec.Degree))
                throw new AppException("Degree is required to be entered into this field.");
        }
    }
}

