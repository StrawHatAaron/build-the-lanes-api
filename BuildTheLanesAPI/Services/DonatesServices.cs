using System;
using System.Collections.Generic;
using System.Linq;
using BuildTheLanesAPI.Helpers;
using BuildTheLanesAPI.Models;
using BuildTheLanesAPI.Controllers;


namespace BuildTheLanesAPI.Services
{
    public interface IDonatesService
    {
        // Users Authenticate(string Link, string password);
        IEnumerable<Donates> GetAll();
        IEnumerable<Donates> GetDonatesBy(int ProjectNum);
        Donates GetByKey(Donates ec);
        Donates Create(Donates ec);
        public void Update(string newDonates, Donates oldDonates);
        public void Delete(Donates ec);
    }

    public class DonatesService : IDonatesService
    {
        private WebappContext _context;

        public DonatesService(WebappContext context)
        {
            _context = context;
        }

        public IEnumerable<Donates> GetAll()
        {
            return _context.Donates; 
        }

        public IEnumerable<Donates> GetDonatesBy(int ProjectNum){
            return  _context.Donates.Where(x => x.ProjectNum==ProjectNum);   
        }

        public Donates GetByKey(Donates ec)
        {
            CheckValues(ec);
            var targetDonates = _context.Donates.SingleOrDefault(x => 
                (x.ProjectNum == ec.ProjectNum && x.DonatorsEmail == ec.DonatorsEmail));
            if (targetDonates == null)
                throw new AppException($"Donates with Link:'{ec.Link}' and Project Number:'{ec.ProjectNum}' DNE.");
            return targetDonates;
        }


        public Donates Create(Donates ec)
        {
            //should check for duplicates on all them tho!
            var checkObj = _context.Donates.SingleOrDefault(x => 
                x.ProjectNum==ec.ProjectNum && x.DonatorsEmail==ec.DonatorsEmail);
            if(checkObj != null)
                throw new AppException($"Donates already exists for Email:{ec.DonatorsEmail} and Project Nubmer:{ec.ProjectNum}");

             this.CheckValues(ec);
            _context.Donates.Add(ec);
            _context.SaveChanges();
            return ec;
        }


        public void Update(string newLink, Donates oldDonates)
        {
            var targetDonates = this.GetByKey(oldDonates);
            targetDonates.Link = newLink;
            _context.SaveChanges();
        }


        public void Delete(Donates ec)
        {
            var targetDonates = this.GetByKey(ec);
            _context.Donates.Remove(targetDonates);
            _context.SaveChanges();
        }

        public void CheckValues(Donates ec)
        {
            if (string.IsNullOrWhiteSpace(ec.Link))
                throw new AppException("Link is required to finish inserting this record");

            if (string.IsNullOrWhiteSpace(ec.Link))
                throw new AppException("Please enter a value to insert " + ec.Link + "'s  Donates in the record");
        }
    }
}

