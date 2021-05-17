using System;
using Inventary.Services.Servies;
using InventaryManagement.Controllers;
using Xunit;
using Moq;
using Inventary.Core.Domain.VM;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Inventary.Test.ControllerTest
{
    public class InventaryControllerTest
    {
        private Mock<IInventaryService> _inventaryService;
        private InventaryController _controller;

        public InventaryControllerTest()
        {
            _inventaryService = new Mock<IInventaryService>();
            _controller = new InventaryController(_inventaryService.Object);
        }

        [Fact]
        public async void InventaryDetailList_Ok()
        {
            // Arrange
            var response = new List<InventaryDetailList>();
            response.Add(new InventaryDetailList()
            {
                Id = 1,
                Name = "Sudev",
            });

            _inventaryService.Setup(ser => ser.GetAllInventrary()).Returns(Task.FromResult(response.AsEnumerable()));
            // Act
            var result = await _controller.GetInventries();
            // Assert
            var res = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, res.StatusCode);

        }

        [Fact]
        public async void InventaryDetailList_Error()
        {
            // Arrange

            _inventaryService.Setup(ser => ser.GetAllInventrary()).Throws(new Exception("Error"));
            // Act
            var result = await _controller.GetInventries();
            // Assert
            var res = Assert.IsType<StatusCodeResult>(result);
            Assert.Equal(500, res.StatusCode);

        }

        [Fact]
        public async void InventaryDetail_GetInventary_Ok()
        {
            int id = 1;
            // Arrange
            SpecificInventary inventaryDetail = new SpecificInventary()
            {
                Id = 1,
                Name = "Sudev",
                Description = "Good Boy",
                PricePerUnit = 5,
                Quantity = 2
            };

            _inventaryService.Setup(ser => ser.GetInventrary(id)).Returns(Task.FromResult(inventaryDetail));
            // Act
            var result = await _controller.GetInventrary(id);
            // Assert
            var res = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, res.StatusCode);

        }
        [Fact]
        public async void InventaryDetail_GetInventary_NotOk()
        {
            // Arrange
            int id = 2;

            // Act
            var result = await _controller.GetInventrary(id);
            // Assert
            var res = Assert.IsType<NotFoundResult>(result);
            Assert.Equal(404, res.StatusCode);

        }

        [Fact]
        public async void InventaryDetail_DeleteInventary_Ok()
        {
            int id = 3;

            _inventaryService.Setup(ser => ser.DeleteInventrary(id)).Returns(Task.FromResult(1));
            // Act
            var result = await _controller.DeleteInventrary(id);
            // Assert
            var res = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, res.StatusCode);

        }
        [Fact]
        public async void InventaryDetail_DeleteInventary_NotOk()
        {
            // Arrange
            int id = 100;
            // Act
            var result = await _controller.DeleteInventrary(id);
            // Assert
            var res = Assert.IsType<NotFoundResult>(result);
            Assert.Equal(404, res.StatusCode);

        }

        [Fact]
        public async void SaveInventrary_Ok()
        {
            // Arrange
            InventaryDetail inventaryDetail = new InventaryDetail()
            {
                Name = "Sudev",
                Description = "Good Boy",
                PricePerUnit = 5,
                Quantity = 2
            };
            _inventaryService.Setup(ser => ser.SaveInventrary(It.IsAny<InventaryDetail>())).Returns(Task.FromResult(1));
            // Act
            var response = await _controller.SaveInventrary(inventaryDetail);
            // Assert
            var res = Assert.IsType<OkObjectResult>(response);
            Assert.Equal(200, res.StatusCode);
            Assert.Equal("Inventary is added successfully", res.Value);
        }

        [Fact]
        public async void SaveInventrary_NotOk()
        {
            // Arrange
            InventaryDetail inventaryDetail = new InventaryDetail()
            {
                Name = "Sudev",
                Description = "Good Boy",
                PricePerUnit = 5,
                Quantity = 2
            };

            // Act
            var response = await _controller.SaveInventrary(inventaryDetail);
            // Assert
            var res = Assert.IsType<BadRequestObjectResult>(response);
            Assert.Equal(400, res.StatusCode);
            Assert.Equal("Inventary is not added successfully", res.Value);
        }

        [Fact]
        public async void SaveInventrary_Error()
        {
            // Arrange
            InventaryDetail inventaryDetail = new InventaryDetail()
            {
                Name = "Sudev",
            };

            _inventaryService.Setup(ser => ser.SaveInventrary(inventaryDetail)).Throws(new Exception("Error"));
            // Act
            var response = await _controller.SaveInventrary(inventaryDetail);
            // Assert
            var res = Assert.IsType<StatusCodeResult>(response);
            Assert.Equal(500, res.StatusCode);
        }

        [Fact]
        public async void UpdateInventrary_Ok()
        {
            int id = 2;
            // Arrange
            InventaryDetail inventaryDetail = new InventaryDetail()
            {
                Name = "Sudev",
                Description = "Good Boy",
                PricePerUnit = 5,
                Quantity = 2
            };
            _inventaryService.Setup(ser => ser.UpdateInventrary(It.IsAny<InventaryDetail>(), id)).Returns(Task.FromResult(1));
            // Act
            var response = await _controller.UpdateInventray(inventaryDetail, id);
            // Assert
            var res = Assert.IsType<OkObjectResult>(response);
            Assert.Equal(200, res.StatusCode);
            Assert.Equal("Inventary is updated successfully", res.Value);
        }

        [Fact]
        public async void UpdateInventrary_NotOk()
        {
            int id = 2;
            // Arrange
            InventaryDetail inventaryDetail = new InventaryDetail()
            {
                Name = "Sudev",
                Description = "Good Boy",
                PricePerUnit = 5,
                Quantity = 2
            };

            // Act
            var response = await _controller.UpdateInventray(inventaryDetail, id);
            // Assert
            var res = Assert.IsType<NotFoundResult>(response);
            Assert.Equal(404, res.StatusCode);
        }

        [Fact]
        public async void UpdateInventrary_Error()
        {
            int id = 2;
            // Arrange
            InventaryDetail inventaryDetail = new InventaryDetail()
            {
                Name = "Sudev",
            };

            _inventaryService.Setup(ser => ser.UpdateInventrary(inventaryDetail, id)).Throws(new Exception("Error"));
            // Act
            var response = await _controller.UpdateInventray(inventaryDetail, id);
            // Assert
            var res = Assert.IsType<StatusCodeResult>(response);
            Assert.Equal(500, res.StatusCode);
        }

    }
}
