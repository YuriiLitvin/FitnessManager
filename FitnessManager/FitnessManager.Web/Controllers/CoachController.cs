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
        
        private FitnessDbContext _fitnessDbContext;

        public CoachController(IRepository<Coach> coachRepository, FitnessDbContext fitnessDbContext)
        {
            _coachRepository = coachRepository;

            _fitnessDbContext = fitnessDbContext;

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
        
        [HttpPost]
        public ActionResult Post(CoachModel coachModel)
        {
            var coach = CreateCoach(coachModel);

            _coachRepository.Add(coach);
            _fitnessDbContext.SaveChanges();
            
            return Created("", coach);
        }
        



        [HttpPut("{id}")]
        public ActionResult Put(Coach coach)
        {
            _coachRepository.Update(coach);
            _fitnessDbContext.SaveChanges();
            return Ok();
        }

        
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _coachRepository.Delete(id);
            _fitnessDbContext.SaveChanges();
            return Ok();
        }
        
        private Coach CreateCoach(CoachModel coachModel)
        {
            var coach = new Coach
            {
                FirstName = coachModel.FirstName,
                LastName = coachModel.LastName,
                Email = coachModel.Email,
                MobileNumber = coachModel.MobileNumber,
                TypeOfTraining = coachModel.TypeOfTraining
            };
            return coach;
        }
    }
}
