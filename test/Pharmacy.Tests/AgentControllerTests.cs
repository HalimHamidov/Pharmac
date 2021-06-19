using System;
using System.Collections.Generic;
using Moq;
using AutoMapper;
using Pharmacy.Models;
using Xunit;
using Pharmacy.Controllers;
using Pharmacy.Data;
using Pharmacy.Profiles;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.Dtos;
using Newtonsoft.Json.Serialization;

namespace Pharmacy.Tests
{
    public class AgentControllerTests : IDisposable
    {
        Mock<IAgentRepo> mockRepo;
        AgentsProfile realProfile;
        MapperConfiguration configuration;
        IMapper mapper;

        public AgentControllerTests()
        {
            mockRepo = new Mock<IAgentRepo>();
            realProfile = new AgentsProfile();
            configuration = new MapperConfiguration(cfg => cfg.AddProfile(realProfile));
            mapper = new Mapper(configuration);
        }

        public void Dispose()
        {
            mockRepo = null;
            mapper = null;
            configuration = null;
            realProfile = null;
        }   
        
        //**************************************************
        //*
        //GET   /api/agents Unit Tests
        //*
        //**************************************************

        //TEST 1.1
           [Fact]
        public void ShouldGetAllAgents_ReturnsZeroResources_WhenDBIsEmpty()
        {
            AgentFilterDto Agent = new AgentFilterDto();
            Agent.Name = "SuperAgent";
            //Arrange 
            mockRepo.Setup(repo =>
                repo.GetAllAgents(Agent)).Returns(GetAgents(0));

            var controller = new AgentsController(mockRepo.Object, mapper);

            //Act
            var result = controller.GetAllAgents(Agent);

            //Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }
                //TEST 1.2
        [Fact]
        public void GetAllCommands_ReturnsOneResource_WhenDBHasOneResource()
        {
            AgentFilterDto Agent = new AgentFilterDto();
            Agent.Name = "SuperAgent";
           
   
            //Arrange 
            mockRepo.Setup(repo =>
              repo.GetAllAgents(Agent)).Returns(GetAgents(1));

            var controller = new AgentsController(mockRepo.Object, mapper);

            //Act
            var result = controller.GetAllAgents(Agent);

            //Assert
            var okResult = result.Result as OkObjectResult;

            var ags = okResult.Value as List<AgentReadDto>;

            Assert.Single(ags);
        }

        private List<Agent> GetAgents(int num)
        {
            var agents = new List<Agent>();
            if (num > 0)
            {
                agents.Add(new Agent
                {
                    Name = "SuperAgent",
                });
            }
            return agents;
        }
    }
}

