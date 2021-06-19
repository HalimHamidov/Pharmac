using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.Data;
using Pharmacy.Data.Interfaces.Dtos;
using Pharmacy.Dtos;
using Pharmacy.Filters;
using Pharmacy.Models;

namespace Pharmacy.Controllers
{
    //api/agents
    [Authorize]
    [Route("api/agents")]
    [ApiController]
    public class AgentsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAgentRepo _repository;

        public AgentsController(IAgentRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        //GET api/agents
        [HttpGet]
        public ActionResult<PagedResponseDto<AgentReadDto>> GetAllAgents([FromQuery]AgentFilterDto filter)
        {
            var validFilter = new AgentFilterDto(filter.Page, filter.Limit);
            validFilter.BirthDate = filter.BirthDate;
            validFilter.CityId = filter.CityId;
            validFilter.ManagerId = filter.ManagerId;
            validFilter.Name = filter.Name;
            validFilter.Supplier = filter.Supplier;
            validFilter.Client = filter.Client;

            //filter
            var items = _repository.GetAllAgents(validFilter);
            var totalRecords = _repository.GetAllAgentsCount(validFilter);

            //get data for the current page  
            var result = new PagedResponseDto<AgentReadDto>();  
            result.Items = _mapper.Map<IEnumerable<AgentReadDto>>(items);

            result.TotalRecords = totalRecords;
            result.Page = validFilter.Page;
            result.Limit = validFilter.Limit;
            result.TotalPages = Convert.ToInt32(Math.Ceiling((double)totalRecords / (double)validFilter.Limit));

            //get next page URL string  
            AgentFilterDto nextFilter = validFilter.Clone() as AgentFilterDto;  
            nextFilter.Page += 1;  
            var nextProdItems = _repository.GetAllAgents(nextFilter);
            String nextUrl = nextProdItems.Count() <= 0 ? null : this.Url.Action("GetAllAgents", null, nextFilter, Request.Scheme);  
   
            //get previous page URL string  
            AgentFilterDto previousFilter = validFilter.Clone() as AgentFilterDto;  
            previousFilter.Page -= 1;  
            String previousUrl = previousFilter.Page <= 0 ? null : this.Url.Action("GetAllAgents", null, previousFilter, Request.Scheme);  
  
            result.NextPage = !String.IsNullOrWhiteSpace(nextUrl) ? new Uri(nextUrl) : null;  
            result.PreviousPage = !String.IsNullOrWhiteSpace(previousUrl) ? new Uri(previousUrl) : null;  
  
            return result;  

        }

        //GET api/agents/{id}
        [HttpGet("{id}", Name = "GetAgentById")]
        [ServiceFilter(typeof(ValidateEntityExistsAttribute<Agent>))]
        public ActionResult<AgentReadDto> GetAgentById(int id)
        {
            var agentItem = HttpContext.Items["entity"] as Agent;
            _repository.LoadRelatedEntities(agentItem);
            return Ok(_mapper.Map<AgentReadDto>(agentItem));
        }

        //POST api/agents
        [HttpPost]
        [ServiceFilter(typeof(ValidateAgentAttribute<Agent>))]
        public ActionResult<AgentReadDto> CreateAgent(AgentSaveDto dto)
        {
            var agentModel = _mapper.Map<Agent>(dto);
            _repository.CreateAgent(agentModel);
            _repository.SaveChanges();

            var agentReadDto = _mapper.Map<AgentReadDto>(agentModel);

            return CreatedAtRoute(nameof(GetAgentById), new {agentReadDto.Id}, agentReadDto);

            //return Ok(agentReadDto);
        }

        //PUT api/agents/{id}
        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidateEntityExistsAttribute<Agent>))]
        [ServiceFilter(typeof(ValidateAgentAttribute<Agent>))]
        public ActionResult UpdateAgent(int id, AgentSaveDto dto)
        {
            var agentModelFromRepo = HttpContext.Items["entity"] as Agent;

            _mapper.Map(dto, agentModelFromRepo);
            _repository.UpdateAgent(agentModelFromRepo);
            _repository.SaveChanges();
            return NoContent();
        }

        //PATCH api/agents/{id}
        [HttpPatch("{id}")]
        [ServiceFilter(typeof(ValidateEntityExistsAttribute<Agent>))]
        public ActionResult PartialAgentUpdate(int id, JsonPatchDocument<AgentSaveDto> patchDoc)
        {
            var agentModelFromRepo = HttpContext.Items["entity"] as Agent;
            var agentToPatch = _mapper.Map<AgentSaveDto>(agentModelFromRepo);
            patchDoc.ApplyTo(agentToPatch, ModelState);

            if (!TryValidateModel(agentToPatch))
                return ValidationProblem(ModelState);

            _mapper.Map(agentToPatch, agentModelFromRepo);
            _repository.UpdateAgent(agentModelFromRepo);
            _repository.SaveChanges();
            return NoContent();
        }

        //DELETE api/agents/{id}
        [HttpDelete("{id}")]
        [ServiceFilter(typeof(ValidateEntityExistsAttribute<Agent>))]
        public ActionResult DeleteAgent(int id)
        {
            var agentModelFromRepo = HttpContext.Items["entity"] as Agent;

            _repository.DeleteAgent(agentModelFromRepo);
            _repository.SaveChanges();
            return NoContent();
        }
    }
}