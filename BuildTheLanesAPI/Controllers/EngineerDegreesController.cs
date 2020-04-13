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
    public class EngineerDegreesController : ControllerBase
    {
        
        private IEngineeringDegreeService _engineerDegService;
        private readonly AppSettings _appSettings;

        public EngineerDegreesController(IEngineeringDegreeService ecService, IMapper mapper, IOptions<AppSettings> appSettings)
        {
            _engineerDegService = ecService;
            _appSettings = appSettings.Value;
        }

        [HttpGet]
        public IActionResult GetAll(){
            var allEngCerts = _engineerDegService.GetAll();
            return Ok(allEngCerts);
        }


        [HttpGet("{ProjectNumber}")]
        public IActionResult GetEngineerCertification([FromBody] EngineerDegrees ec){
            try{
                var targetEngCert = _engineerDegService.GetByKey(ec);
                return Ok(targetEngCert);
            }
            catch (AppException ex){
                return BadRequest(new { message = ex.Message });
            }
        }
        

        [HttpPost]
        public IActionResult Create([FromBody] EngineerDegrees ec){
            try 
            {
                var newEngCert = _engineerDegService.Create(ec);
                return Ok(newEngCert);
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        

        [HttpPut("{newDegVal}")]
        public IActionResult Update(string newDegVal, [FromBody] EngineerDegrees oldDeg){
            try{
                Console.WriteLine($"The newCertVal:{newDegVal}");
                _engineerDegService.Update(newDegVal, oldDeg);
                return Ok(new { message = "Updated." });
            }
            catch (AppException ex) {
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpDelete]
        public IActionResult Delete([FromBody] EngineerDegrees ec){
            try{
                _engineerDegService.Delete(ec);
                return Ok(ec);
            } 
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }   
        }
    }

}
