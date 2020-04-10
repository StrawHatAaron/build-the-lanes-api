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
using Newtonsoft.Json.Linq;

namespace BuildTheLanesAPI.Controllers
{
    
    [ApiController]
    [Route(Constants.api + "/[controller]")]    
    public class EngineerCertificationsController : ControllerBase
    {
        
        private IEngineeringCertificationService _engineerCertService;
        private readonly AppSettings _appSettings;

        public EngineerCertificationsController(IEngineeringCertificationService ecService, IMapper mapper, IOptions<AppSettings> appSettings)
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
        

        [HttpPut]
        public IActionResult Update([FromBody] JObject oanVal){
            try{
                _engineerCertService.Update(oanVal);
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

    public class OldAndNewEngineerCertifications{
        public EngineerCertifications OldCert;
        public EngineerCertifications NewCert;
    }

}
