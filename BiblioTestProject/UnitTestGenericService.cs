using Service.Models;
using Service.Services;

namespace BiblioTestProject
{
    public class UnitTestGenericService
    {
        //test GetAllAsync method of GenericService
        [Fact]
        public async Task Test_GetAllAsync_ReturnsListOfEntities()
        {
            // Arrange
            var service= new GenericService<Libro>();
            // Act
            var result = await service.GetAllAsync();
            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<Libro>>(result);
            Assert.True(result.Count > 0);
        }
        //test GetAllAsync method of GenericService
        [Fact]
        public async Task Test_GetAllAsync_WithFilter()
        {
            // Arrange
            var service = new GenericService<Libro>();
            // Act
            var result = await service.GetAllAsync("catedral");
            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<Libro>>(result);
            Assert.True(result.Count == 1);
            Assert.Equal("Conversación en La Catedral", result[0].Titulo);
        }

        //test GetAllAsync method of GenericService with invalid endpoint
        [Fact]
        public async Task Test_GetAllAsync_InvalidEndpoint_ThrowsException()
        {
            // Arrange
            var service = new GenericService<Random>();
            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () => await service.GetAllAsync());
        }


    }
}
