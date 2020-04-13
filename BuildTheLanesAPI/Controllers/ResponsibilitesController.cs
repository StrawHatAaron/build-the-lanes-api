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
    public class ResponsibilitiesController : ControllerBase
    {
        
        private IResponsibilitiesService _responsibilitiesService;
        private readonly AppSettings _appSettings;

        public ResponsibilitiesController(IResponsibilitiesService ecService, IMapper mapper, IOptions<AppSettings> appSettings)
        {
            _responsibilitiesService = ecService;
            _appSettings = appSettings.Value;
        }

        [HttpGet]
        public IActionResult GetAll(){
            var allResps = _responsibilitiesService.GetAll();
            return Ok(allResps);
        }


        [HttpGet("{number}")]
        public IActionResult GetResponsibilities(int number){
            try{
                var targetResp = _responsibilitiesService.GetByNumber(number);
                return Ok(targetResp);
            }
            catch (AppException ex){
                return BadRequest(new { message = ex.Message });
            }
        }
        

        [HttpPost]
        public IActionResult Create([FromBody] Responsibilities ec){
            try 
            {
                var newResp = _responsibilitiesService.Create(ec);
                return Ok(newResp);
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        

        [HttpPut("{newProjNum}")]
        public IActionResult Update(int newProjNumVal, [FromBody] Responsibilities oldRes){
            try{
                Console.WriteLine($"The newProjNum:{newProjNumVal}");
                _responsibilitiesService.Update(newProjNumVal, oldRes);
                return Ok(new { message = "Updated." });
            }
            catch (AppException ex) {
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpDelete]
        public IActionResult Delete([FromBody] Responsibilities ec){
            try{
                _responsibilitiesService.Delete(ec);
                return Ok(ec);
            } 
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }   
        }
    }

}
