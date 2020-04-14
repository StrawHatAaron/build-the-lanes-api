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
    public class AdminsController : ControllerBase
    {
        
        private IAdminsService _adminsService;
        private readonly AppSettings _appSettings;

        public AdminsController(IAdminsService ecService, IMapper mapper, IOptions<AppSettings> appSettings)
        {
            _adminsService = ecService;
            _appSettings = appSettings.Value;
        }

        [HttpGet]
        public IActionResult GetAll(){
            var allEngCerts = _adminsService.GetAll();
            return Ok(allEngCerts);
        }


        [HttpGet("{id}")]
        public IActionResult GetAnAdmin(int id){
            try{
                var targetEngCert = _adminsService.GetById(id);
                return Ok(targetEngCert);
            }
            catch (AppException ex){
                return BadRequest(new { message = ex.Message });
            }
        }
        
    }

}
