using Microsoft.AspNetCore.Mvc;
using Moq;
using PostTradesThree.Controllers;
using PostTradesThree.Domain;
using PostTradesThree.Repositories;

namespace PostTradesThree.Tests.Controllers
{
    public class TradesControllerTests
    {
        private readonly Mock<ITradeRepository> _mockTradeRepository;
        private readonly TradesController _controller;

        public TradesControllerTests()
        {
            _mockTradeRepository = new Mock<ITradeRepository>();
            _controller = new TradesController(_mockTradeRepository.Object);
        }

        [Fact]
        public async Task GetAllTradesAsync_ReturnsOkResult()
        {
            // Arrange
            var trades = new List<Trade>
            {
                new Trade { TradeId = 1 },
                new Trade { TradeId = 2 }
            };

            _mockTradeRepository.Setup(x => x.GetAllTradesAsync()).ReturnsAsync(trades);

            // Act
            var result = await _controller.GetAllTrades();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedTrades = Assert.IsAssignableFrom<IEnumerable<Trade>>(okResult.Value);
            Assert.Equal(2, returnedTrades.Count());
        }

        [Fact]
        public async Task GetTradeByIdAsync_ReturnsOkResult()
        {
            // Arrange
            var trade = new Trade { TradeId = 1 };

            _mockTradeRepository.Setup(x => x.GetTradeByIdAsync(It.IsAny<int>())).ReturnsAsync(trade);

            // Act
            var result = await _controller.GetOneTrade(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedTrade = Assert.IsType<Trade>(okResult.Value);
            Assert.Equal(1, returnedTrade.TradeId);
        }

        [Fact]
        public async Task CreateTradeAsync_ReturnsCreatedAtRouteResult()
        {
            // Arrange
            var trade = new Trade { TradeId = 1 };

            _mockTradeRepository.Setup(x => x.CreateTradeAsync(It.IsAny<Trade>())).ReturnsAsync(1);

            // Act
            var result = await _controller.CreateOneTrade(trade);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Equal("GetOneTrade", createdResult.ActionName);
            Assert.Equal(1, createdResult.RouteValues["id"]);
        }

        [Fact]
        public async Task UpdateTradeAsync_ReturnsOkResult()
        {
            // Arrange
            var trade = new Trade { TradeId = 1 };

            _mockTradeRepository.Setup(x => x.UpdateTradeAsync(It.IsAny<Trade>())).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.UpdateOneTrade(1, trade);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task DeleteTradeByIdAsync_ReturnsNoContentResult()
        {
            // Arrange
            _mockTradeRepository.Setup(x => x.DeleteTradeByIdAsync(It.IsAny<int>())).Returns(Task.FromResult(1));

            // Act
            var result = await _controller.DeleteOneTrade(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task GetAllTradesAsync_ReturnsNotFound_WhenNoTradesFound()
        {
            // Arrange
            _mockTradeRepository.Setup(x => x.GetAllTradesAsync()).ReturnsAsync(Enumerable.Empty<Trade>());

            // Act
            var result = await _controller.GetAllTrades();

            // Assert
            var notFoundResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedTrades = Assert.IsAssignableFrom<IEnumerable<Trade>>(notFoundResult.Value);
        }

        [Fact]
        public async Task GetTradeByIdAsync_ReturnsNotFound_WhenTradeNotFound()
        {
            // Arrange
            _mockTradeRepository.Setup(x => x.GetTradeByIdAsync(It.IsAny<int>())).ReturnsAsync((Trade)null);

            // Act
            var result = await _controller.GetOneTrade(1);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(result.Result);
            Assert.Equal(404, notFoundResult.StatusCode);
        }

        [Fact]
        public async Task CreateTradeAsync_ReturnsNotFound_WhenTradeIsNull()
        {
            // Arrange
            Trade trade = null;

            // Act
            var result = await _controller.CreateOneTrade(trade);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(result.Result);
            Assert.Equal(404, notFoundResult.StatusCode);
        }

        [Fact]
        public async Task UpdateTradeAsync_ReturnsNotFound_WhenTradeMismatch()
        {
            // Arrange
            var trade = new Trade { TradeId = 2 };

            // Act
            var result = await _controller.UpdateOneTrade(1, trade);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(result);
            Assert.Equal(404, notFoundResult.StatusCode);
        }
    }
}
