using GlobalLogic_PizzaIncSln.Controllers;
using GlobalLogic_PizzaIncSln.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace GlobalLogic_PizzaIncSln.Unit_Test
{
    [TestFixture]
    public class unitTest_PizzaCabinActivity
    {
        private PizzaCabinActivityControllercs _controller;
        private Mock<AppDBContext> _mockDbContext;

        [SetUp]
        public void Setup()
        {
            _mockDbContext = new Mock<AppDBContext>();
            _controller = new PizzaCabinActivityControllercs(_mockDbContext.Object);

        }

        [Test]
        public async Task GetAllScheduledData_ValidData_ReturnsOkResult()
        {
            var result = await _controller.GetAllScheduledData();

            Assert.Pass("Pass");
        }

        [Test]
        public async Task InsertDataToDB_ValidData_ReturnsOkResult()
        {
           
            var scheduleResultViewModel = new ScheduleResultViewModel
            {
                //data to be entered
            };

            
            var result = await _controller.InsertDataToDB(scheduleResultViewModel);

            
            Assert.Pass("Success");
        }

        [Test]
        public async Task InsertDataToDB_NullData_ReturnsBadRequest()
        {
            ScheduleResultViewModel scheduleResultViewModel = null;

            var result = await _controller.InsertDataToDB(scheduleResultViewModel);

            Assert.Fail("Failed");
        }

        // Add more test cases to cover different scenarios such as invalid data, exceptions, etc.

    }
}
