using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using BuildTheLanesAPI.Helpers;
using BuildTheLanesAPI.Services;
using BuildTheLanesAPI.Models;

namespace BuildTheLanesAPI.Controllers
{
    
    [ApiController]
    [Route(Constants.api + "/[controller]")]    
    public class EngineerCertificationsController : ControllerBase
    {
        
        private IEngineerCertificationsService _engineerCertService;
        private readonly AppSettings _appSettings;

        public EngineerCertificationsController(IEngineerCertificationsService ecService, IMapper mapper, IOptions<AppSettings> appSettings)
        {
            _engineerCertService = ecService;
            _appSettings = appSettings.Value;
        }

        [HttpGet]
        public IActionResult GetAll(){
            var allEngCerts = _engineerCertService.GetAll();
            return Ok(allEngCerts);
        }


        [HttpGet("{ProjectNumber}")]
        public IActionResult GetEngineerCertification([FromBody] EngineerCertifications ec){
            try{
                var targetEngCert = _engineerCertService.GetByKey(ec);
                return Ok(targetEngCert);
            }
            catch (AppException ex){
                return BadRequest(new { message = ex.Message });
            }
        }
        

        [HttpPost]
        public IActionResult Create([FromBody] EngineerCertifications ec){
            try 
            {
                var newEngCert = _engineerCertService.Create(ec);
                return Ok(newEngCert);
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        

        [HttpPut("{newCertVal}")]
        public IActionResult Update(string newCertVal, [FromBody] EngineerCertifications oldCert){
            try{
                Console.WriteLine($"The newCertVal:{newCertVal}");
                _engineerCertService.Update(newCertVal, oldCert);
                return Ok(new { message = "Updated." });
            }
            catch (AppException ex) {
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpDelete]
        public IActionResult Delete([FromBody] EngineerCertifications ec){
            try{
                _engineerCertService.Delete(ec);
                return Ok(ec);
            } 
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }   
        }
    }

}
