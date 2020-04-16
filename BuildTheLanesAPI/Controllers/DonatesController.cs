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
    public class DonatesController : ControllerBase
    {
        
        private IDonatesService _aDonateService;
        private readonly AppSettings _appSettings;

        public DonatesController(IDonatesService ecService, IMapper mapper, IOptions<AppSettings> appSettings)
        {
            _aDonateService = ecService;
            _appSettings = appSettings.Value;
        }

        [HttpGet]
        public IActionResult GetAll(){
            var allDonatess = _aDonateService.GetAll();
            return Ok(allDonatess);
        }


        [HttpGet("{ProjectNum}")]
        public IActionResult GetDonatesForProject(int ProjectNum){
            try{
                var targetDonatess = _aDonateService.GetDonatesBy(ProjectNum);
                return Ok(targetDonatess);
            }
            catch (AppException ex){
                return BadRequest(new { message = ex.Message });
            }
        }
        

        [HttpPost]
        public IActionResult Create([FromBody] Donates ec){
            try 
            {
                var newDonates = _aDonateService.Create(ec);
                return Ok(newDonates);
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        

        [HttpPut("{newDataLink}")]
        public IActionResult Update(string newDataLink, [FromBody] Donates oldDonates){
            try{
                _aDonateService.Update(newDataLink, oldDonates);
                return Ok(new { message = "Updated." });
            }
            catch (AppException ex) {
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpDelete]
        public IActionResult Delete([FromBody] Donates ec){
            try{
                _aDonateService.Delete(ec);
                return Ok(ec);
            } 
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }   
        }
    }

}
