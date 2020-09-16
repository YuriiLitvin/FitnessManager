﻿using System;
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
        public ActionResult<IEnumerable<CoachModel>> Get()
        {
            var coaches = _coachRepository.Get();
            
            if(coaches.Any())
            {
                var coachModels =  new List<CoachModel>();
                foreach(var coach in coaches)
                {
                    var coachModel = CreateCoachModel(coach);
                    coachModels.Add(coachModel);
                }
                
                return Ok(coachModels);
            }
            return NoContent();
            
        }
        
        [HttpGet("{id}")]
        public ActionResult<CoachModel> Get(int id)
        {
            var coaches = _coachRepository.Get();

            if (coaches.Any())
            {
                var coach = coaches.FirstOrDefault(_ => _.Id == id);
                var coachModel = CreateCoachModel(coach);
                return Ok(coachModel);
            }
            return NotFound();

        }
        [HttpPost]
        public ActionResult Post(CoachModel coachModel)
        {
            var coach = CreateCoach(coachModel);

            _coachRepository.Add(coach);
            _fitnessDbContext.SaveChanges();
            
            return Created("", coach);
        }
        

        [HttpPut]
        public ActionResult Put(CoachModel coachModel)
        {
            var coach = CreateCoach(coachModel);
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
        private CoachModel CreateCoachModel(Coach coach)
        {
            var coachModel = new CoachModel
            {
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
