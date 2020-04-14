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
    public class EngineersController : ControllerBase
    {
        
        private IEngineersService _EngineersService;
        private readonly AppSettings _appSettings;

        public EngineersController(IEngineersService ecService, IMapper mapper, IOptions<AppSettings> appSettings)
        {
            _EngineersService = ecService;
            _appSettings = appSettings.Value;
        }

        [HttpGet]
        public IActionResult GetAll(){
            var allEngCerts = _EngineersService.GetAll();
            return Ok(allEngCerts);
        }


        [HttpGet("{id}")]
        public IActionResult GetAnEngineer(int id){
            try{
                var targetEngCert = _EngineersService.GetById(id);
                return Ok(targetEngCert);
            }
            catch (AppException ex){
                return BadRequest(new { message = ex.Message });
            }
        }
        
    }

}
