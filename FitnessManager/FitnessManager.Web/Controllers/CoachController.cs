using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FintessManager.Data;
using FintessManager.Data.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FitnessManager.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class CoachController : ControllerBase
    {
        private IRepository<Coach> _coachRepository;
        public CoachController(IRepository<Coach> coachRepository)
        {
            _coachRepository = coachRepository;
        }
    
        [HttpGet]
        public ActionResult<IEnumerable<Coach>> Get()
        {
            var items = _coachRepository.Get();
            if(items.Any())
            {
                return Ok(items);
            }

            return NoContent();
            
        } 
    
        
        
        
        //[HttpDelete]
        //public ActionResult Delete (int id)
        //{
        //    var item = _coachRepository.Delete(id);
        //}
    
    }
}
