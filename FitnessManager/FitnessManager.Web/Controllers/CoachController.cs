using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FintessManager.Data;
using FintessManager.Data.Entity;
using FitnessManager.Web.Models;
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
        

        //[HttpPost]
        //public ActionResult Post(Coach coach)
        //{
        //    _coachRepository.Add(coach);

        //    return Created("",coach);
        //}
        
        
        //
        [HttpPost]
        public ActionResult Post(CoachModel coachModel)
        {
            _coachRepository.Add(coachModel);

            return Created("", coachModel);
        }
        //



        [HttpPut("{id}")]
        public ActionResult Put(Coach coach)
        {
            _coachRepository.Update(coach);

            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _coachRepository.Delete(id);

            return Ok();
        }

    }
}
