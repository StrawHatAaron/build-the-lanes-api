using System;
using System.Collections.Generic;
using System.Linq;
using BuildTheLanesAPI.Helpers;
using BuildTheLanesAPI.Models;
using BuildTheLanesAPI.Controllers;


namespace BuildTheLanesAPI.Services
{
    public interface IApplicableStandardsService
    {
        // Users Authenticate(string DataLink, string password);
        IEnumerable<ApplicableStandards> GetAll();
        IEnumerable<ApplicableStandards> GetStandardsBy(int ProjectNum);
        ApplicableStandards GetByKey(ApplicableStandards ec);
        ApplicableStandards Create(ApplicableStandards ec);
        public void Update(string newAppStand, ApplicableStandards oldAppStand);
        public void Delete(ApplicableStandards ec);
    }

    public class ApplicableStandardsService : IApplicableStandardsService
    {
        private WebappContext _context;

        public ApplicableStandardsService(WebappContext context)
        {
            _context = context;
        }

        public IEnumerable<ApplicableStandards> GetAll()
        {
            return _context.ApplicableStandards; 
        }

        public IEnumerable<ApplicableStandards> GetStandardsBy(int ProjectNum){
            return  _context.ApplicableStandards.Where(x => x.ProjectNum==ProjectNum);   
        }

        public ApplicableStandards GetByKey(ApplicableStandards ec)
        {
            CheckValues(ec);
            var targetAppStand = _context.ApplicableStandards.SingleOrDefault(x => 
                (x.ProjectNum == ec.ProjectNum && x.DataLink == ec.DataLink));
            if (targetAppStand == null)
                throw new AppException($"Applicable Standard with Data Link:'{ec.DataLink}' and Project Number:'{ec.ProjectNum}' DNE.");
            return targetAppStand;
        }


        public ApplicableStandards Create(ApplicableStandards ec)
        {
            var checkObj = _context.ApplicableStandards.SingleOrDefault(x => x.ProjectNum==ec.ProjectNum && x.DataLink==ec.DataLink);
            if(checkObj != null)
                throw new AppException($"Applicable Standard already exists for Link:{ec.DataLink} and Project Nubmer:{ec.ProjectNum}");

             this.CheckValues(ec);
            _context.ApplicableStandards.Add(ec);
            _context.SaveChanges();
            return ec;
        }


        public void Update(string newPhotoName, ApplicableStandards oldAppStand)
        {
            var targetAppStand = this.GetByKey(oldAppStand);
            targetAppStand.PhotoName = newPhotoName;
            _context.SaveChanges();
        }


        public void Delete(ApplicableStandards ec)
        {
            var targetAppStand = this.GetByKey(ec);
            _context.ApplicableStandards.Remove(targetAppStand);
            _context.SaveChanges();
        }

        public void CheckValues(ApplicableStandards ec)
        {
            if (string.IsNullOrWhiteSpace(ec.DataLink))
                throw new AppException("DataLink is required to finish inserting this record.");

            if (string.IsNullOrEmpty(ec.ProjectNum.ToString()))
                throw new AppException("Project Number is required to finish inserting this record.");
        }
    }
}

