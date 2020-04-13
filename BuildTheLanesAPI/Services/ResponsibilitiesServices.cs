using System;
using System.Collections.Generic;
using System.Linq;
using BuildTheLanesAPI.Helpers;
using BuildTheLanesAPI.Models;
using BuildTheLanesAPI.Controllers;


namespace BuildTheLanesAPI.Services
{
    public interface IResponsibilitiesService
    {
        // Users Authenticate(string StaffEmail, string password);
        IEnumerable<Responsibilities> GetAll();
        Responsibilities GetByKey(Responsibilities ec);
        public Responsibilities GetByNumber(int number);
        Responsibilities Create(Responsibilities ec);
        public void Update(int newProjNum, Responsibilities oldProj);
        public void Delete(Responsibilities ec);
    }

    public class ResponsibilitiesService : IResponsibilitiesService
    {
        private WebappContext _context;

        public ResponsibilitiesService(WebappContext context)
        {
            _context = context;
        }

        public IEnumerable<Responsibilities> GetAll()
        {
            return _context.Responsibilities; 
        }

        public Responsibilities GetByKey(Responsibilities ec)
        {
            CheckValues(ec);
            var targetResp = _context.Responsibilities.SingleOrDefault(x => 
                (x.ProjectNum == ec.ProjectNum && x.StaffEmail == ec.StaffEmail));
            if (targetResp == null)
                throw new AppException($"ProjectNum with StaffEmail:'{ec.StaffEmail}' and ProjectNum:'{ec.ProjectNum}' DNE.");
            return targetResp;
        }

        public Responsibilities GetByNumber(int number){
            var targetResp = _context.Responsibilities.SingleOrDefault(x => x.Number == number);
            if (targetResp == null)
                throw new AppException($"Responsibility {number}' DNE.");
            return targetResp;
        }

        public Responsibilities Create(Responsibilities ec)
        {
             this.CheckValues(ec);
            _context.Responsibilities.Add(ec);
            _context.SaveChanges();
            return ec;
        }


        public void Update(int newProjNumVal, Responsibilities oldProj)
        {
            Responsibilities newProjNum = new Responsibilities();
            newProjNum.StaffEmail = oldProj.StaffEmail;
            newProjNum.ProjectNum = newProjNumVal;
            Delete(oldProj);
            Create(newProjNum);
            _context.SaveChanges();
        }


        public void Delete(Responsibilities ec)
        {
            var targetResp = this.GetByKey(ec);
            _context.Responsibilities.Remove(targetResp);
            _context.SaveChanges();
        }

        public void CheckValues(Responsibilities ec)
        {
            if (string.IsNullOrWhiteSpace(ec.StaffEmail))
                throw new AppException("StaffEmail is required to finish inserting this record");

            if (string.IsNullOrWhiteSpace(ec.StaffEmail))
                throw new AppException("Please enter a value to insert " + ec.StaffEmail + "'s  ProjectNum in the record");
        }
    }
}

