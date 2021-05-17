using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventary.Core.Domain.DB;
using Inventary.Core.Domain.VM;
using Inventary.Data.Repositaries;
using Inventary.Services.Servies;
using Moq;
using Xunit;

namespace Inventary.Test.ServicesTest
{
    public class InventaryServiceTest
    {
        private Mock<IInventaryRepository> _inventaryRepositary;
        private InventaryService _inventaryService;

        public InventaryServiceTest()
        {
            _inventaryRepositary = new Mock<IInventaryRepository>();
            _inventaryService = new InventaryService(_inventaryRepositary.Object);
        }

        [Fact]
        public async void SaveInventary_Ok()
        {
            //Arrange
            InventaryDetail inventaryDetail = new InventaryDetail()
            {
                Name = "Sudev",
                Description = "Good Boy",
                PricePerUnit = 5,
                Quantity = 2
            };
            _inventaryRepositary.Setup(repo => repo.SaveInventary(It.IsAny<InventaryMaster>())).Returns(Task.FromResult(1));
            //Act
            var response = await _inventaryService.SaveInventrary(inventaryDetail);
            //Assert
            Assert.Equal(1, response);
        }
        [Fact]
        public async void SaveInventary_NotOk()
        {
            //Arrange
            InventaryDetail inventaryDetail = new InventaryDetail()
            {
                Name = "Sudev",
                Description = "Good Boy",
                PricePerUnit = 5,
                Quantity = 2
            };

            //Act
            var response = await _inventaryService.SaveInventrary(inventaryDetail);
            //Assert
            Assert.Equal(0, response);
        }

        [Fact]
        public async void UpdateInventary_Ok()
        {
            int id = 1;
            //Arrange
            InventaryDetail inventaryDetail = new InventaryDetail()
            {
                Name = "Sudev",
                Description = "Good Boy",
                PricePerUnit = 5,
                Quantity = 2
            };
            _inventaryRepositary.Setup(repo => repo.UpdateInventary(It.IsAny<InventaryMaster>(), id)).Returns(Task.FromResult(1));
            //Act
            var response = await _inventaryService.UpdateInventrary(inventaryDetail, id);
            //Assert
            Assert.Equal(1, response);
        }
        [Fact]
        public async void UpdateInventary_NotOk()
        {
            int id = 2;
            //Arrange
            InventaryDetail inventaryDetail = new InventaryDetail()
            {
                Name = "Sudev",
                Description = "Good Boy",
                PricePerUnit = 5,
                Quantity = 2
            };

            //Act
            var response = await _inventaryService.UpdateInventrary(inventaryDetail, id);
            //Assert
            Assert.Equal(0, response);
        }

        [Fact]
        public async void DeleteInventary_Ok()
        {
            int id = 3;
            //Arrange

            _inventaryRepositary.Setup(repo => repo.DeleteInventary(id)).Returns(Task.FromResult(1));
            //Act
            var response = await _inventaryService.DeleteInventrary(id);
            //Assert
            Assert.Equal(1, response);
        }
        [Fact]
        public async void DeleteInventary_NotOk()
        {
            //Arrange
            int id = 2;
            //Act
            var response = await _inventaryService.DeleteInventrary(id);
            //Assert
            Assert.Equal(0, response);
        }

        [Fact]
        public async void GetInventary_Ok()
        {
            //Arrange
            int id = 1;
            
            InventaryMaster inventaryMaster = new InventaryMaster()
            {
                Id = 1,
                Name = "Sudev",
                Description = "Good Boy",
                PricePerUnit = 5,
                Quantity = 2,
                CreatedOn = DateTime.Now,
                TotalPrice = 10
            };
            _inventaryRepositary.Setup(repo => repo.GetInventary(id)).Returns(Task.FromResult(inventaryMaster));
            //Act
            var response = await _inventaryService.GetInventrary(id);
            //Assert
            var res = Assert.IsType<SpecificInventary>(response);
            Assert.Equal(inventaryMaster.Name, res.Name);
            Assert.Equal(inventaryMaster.Description, res.Description);
            Assert.Equal(inventaryMaster.PricePerUnit, res.PricePerUnit);
            Assert.Equal(inventaryMaster.Quantity, res.Quantity);
        }

        [Fact]
        public async void GetInventary_NotOk()
        {
            //Arrange
            int id = 1;
            //Act
            var response = await _inventaryService.GetInventrary(id);
            //Assert
            Assert.Null(response);
        }

        [Fact]
        public async void GetAllInventrary_Ok()
        {
            //Arrange
            var inventaryMaster = new List<InventaryMaster>();
            inventaryMaster.Add(new InventaryMaster()
            {
                Id = 1,
                Name = "Sudev",
                Description = "Good Boy",
                PricePerUnit = 5,
                Quantity = 2,
                CreatedOn = DateTime.Now,
                TotalPrice = 10
            });

            _inventaryRepositary.Setup(repo => repo.GetAllInventary()).Returns(Task.FromResult(inventaryMaster.AsEnumerable()));
            //Act
            var response = await _inventaryService.GetAllInventrary();
            //Assert
            var res = Assert.IsType<List<InventaryDetailList>>(response);
            Assert.Equal(inventaryMaster[0].Name, res[0].Name);
        }

        [Fact]
        public async void GetAllInventrary_NotOk()
        {
            //Act
            var response = await _inventaryService.GetAllInventrary();
            //Assert
            var res = Assert.IsType<List<InventaryDetailList>>(response);
            Assert.Equal(0, res.Count);
        }

    }
}
