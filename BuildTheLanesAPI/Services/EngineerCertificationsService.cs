using System;
using System.Collections.Generic;
using System.Linq;
using BuildTheLanesAPI.Helpers;
using BuildTheLanesAPI.Models;
using BuildTheLanesAPI.Controllers;


namespace BuildTheLanesAPI.Services
{
    public interface IEngineerCertificationsService
    {
        // Users Authenticate(string email, string password);
        IEnumerable<EngineerCertifications> GetAll();
        EngineerCertifications GetByKey(EngineerCertifications ec);
        EngineerCertifications Create(EngineerCertifications ec);
        public void Update(string newCert, EngineerCertifications oldCert);
        public void Delete(EngineerCertifications ec);
    }

    public class EngineerCertificationsService : IEngineerCertificationsService
    {
        private WebappContext _context;

        public EngineerCertificationsService(WebappContext context)
        {
            _context = context;
        }

        public IEnumerable<EngineerCertifications> GetAll()
        {
            return _context.EngineerCertifications; 
        }

        public EngineerCertifications GetByKey(EngineerCertifications ec)
        {
            CheckValues(ec);
            var targetEngCert = _context.EngineerCertifications.SingleOrDefault(x => 
                (x.Certification == ec.Certification && x.Email == ec.Email));
            if (targetEngCert == null)
                throw new AppException($"Certification with email:'{ec.Email}' and certification:'{ec.Certification}' DNE.");
            return targetEngCert;
        }

        public EngineerCertifications Create(EngineerCertifications ec)
        {
             this.CheckValues(ec);
            _context.EngineerCertifications.Add(ec);
            _context.SaveChanges();
            return ec;
        }


        public void Update(string newCertVal, EngineerCertifications oldCert)
        {
            EngineerCertifications newCert = new EngineerCertifications();
            newCert.Email = oldCert.Email;
            newCert.Certification = newCertVal;
            Delete(oldCert);
            Create(newCert);
            _context.SaveChanges();
        }


        public void Delete(EngineerCertifications ec)
        {
            var targetEngCert = this.GetByKey(ec);
            _context.EngineerCertifications.Remove(targetEngCert);
            _context.SaveChanges();
        }

        public void CheckValues(EngineerCertifications ec)
        {
            if (string.IsNullOrWhiteSpace(ec.Email))
                throw new AppException("Email is required to finish inserting this record");

            if (string.IsNullOrWhiteSpace(ec.Email))
                throw new AppException("Please enter a value to insert " + ec.Email + "'s  certification in the record");
        }
    }
}

