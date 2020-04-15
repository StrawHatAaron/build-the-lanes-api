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
    public class ApplicableStandardsController : ControllerBase
    {
        
        private IApplicableStandardsService _aStandardService;
        private readonly AppSettings _appSettings;

        public ApplicableStandardsController(IApplicableStandardsService ecService, IMapper mapper, IOptions<AppSettings> appSettings)
        {
            _aStandardService = ecService;
            _appSettings = appSettings.Value;
        }

        [HttpGet]
        public IActionResult GetAll(){
            var allappStands = _aStandardService.GetAll();
            return Ok(allappStands);
        }


        [HttpGet("{ProjectNum}")]
        public IActionResult GetStandardsForProject(int ProjectNum){
            try{
                var targetappStands = _aStandardService.GetStandardsBy(ProjectNum);
                return Ok(targetappStands);
            }
            catch (AppException ex){
                return BadRequest(new { message = ex.Message });
            }
        }
        

        [HttpPost]
        public IActionResult Create([FromBody] ApplicableStandards ec){
            try 
            {
                var newappStand = _aStandardService.Create(ec);
                return Ok(newappStand);
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        

        [HttpPut("{newPhotoName}")]
        public IActionResult Update(string newPhotoName, [FromBody] ApplicableStandards oldappStand){
            try{
                _aStandardService.Update(newPhotoName, oldappStand);
                return Ok(new { message = "Updated." });
            }
            catch (AppException ex) {
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpDelete]
        public IActionResult Delete([FromBody] ApplicableStandards ec){
            try{
                _aStandardService.Delete(ec);
                return Ok(ec);
            } 
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }   
        }
    }

}
