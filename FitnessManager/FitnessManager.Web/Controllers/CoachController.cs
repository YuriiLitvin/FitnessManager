using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitnessManager.Data;
using FitnessManager.Data.Entity;
using FitnessManager.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

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
        public ActionResult<IEnumerable<CoachModel>> Get()
        {
            var coaches = _coachRepository.Get();

            if (!coaches.Any())
            {
                return NoContent();
            }
            return Ok(coaches.Select(Map));
        }

        [HttpGet("{id}")]
        public ActionResult<CoachModel> Get(int id)
        {
            var coach = _coachRepository.Get(id);

            if (coach == null)
            {
                return NotFound();
            }

            return Ok(Map(coach));
        }


        [HttpPost]
        public ActionResult Post(CoachModel coachModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var coach = Map(coachModel);
            _coachRepository.Add(coach);
            _fitnessDbContext.SaveChanges();

            return Created("", coach);
        }


        [HttpPut("{id}")]
        public ActionResult Put(CoachModel coachModel, int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            

            var coach = Map(coachModel);
            coach.Id = id;
            _coachRepository.Update(coach);
            _fitnessDbContext.SaveChanges();

            return Ok();
        }


        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var coach = _coachRepository.Get(id);

            if (coach == null)
            {
                return NotFound();
            }

            _coachRepository.Delete(id);
            _fitnessDbContext.SaveChanges();
            return Ok();
        }

        private Coach Map(CoachModel coachModel)
        {
            var coach = new Coach
            {
                Id = coachModel.Id,
                FirstName = coachModel.FirstName,
                LastName = coachModel.LastName,
                Email = coachModel.Email,
                MobileNumber = coachModel.MobileNumber,
                TypeOfTraining = coachModel.TypeOfTraining
            };
            return coach;
        }
        private CoachModel Map(Coach coach)
        {
            var coachModel = new CoachModel
            {
                Id = coach.Id,
                FirstName = coach.FirstName,
                LastName = coach.LastName,
                Email = coach.Email,
                MobileNumber = coach.MobileNumber,
                TypeOfTraining = coach.TypeOfTraining
            };
            return coachModel;
        }

    }
}
