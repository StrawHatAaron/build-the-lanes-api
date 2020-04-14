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
    public class DonatorsController : ControllerBase
    {
        
        private IDonatorsService _DonatorsService;
        private readonly AppSettings _appSettings;

        public DonatorsController(IDonatorsService ecService, IMapper mapper, IOptions<AppSettings> appSettings)
        {
            _DonatorsService = ecService;
            _appSettings = appSettings.Value;
        }

        [HttpGet]
        public IActionResult GetAll(){
            var allEngCerts = _DonatorsService.GetAll();
            return Ok(allEngCerts);
        }


        [HttpGet("{id}")]
        public IActionResult GetAnDonator(int id){
            try{
                var targetEngCert = _DonatorsService.GetById(id);
                return Ok(targetEngCert);
            }
            catch (AppException ex){
                return BadRequest(new { message = ex.Message });
            }
        }
        
    }

}
