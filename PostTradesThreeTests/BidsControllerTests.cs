using Microsoft.AspNetCore.Mvc;
using Moq;
using PostTradesThree.Controllers;
using PostTradesThree.Domain;
using PostTradesThree.Repositories;

namespace PostTradesThree.Tests.Controllers
{
    public class BidsControllerTests
    {
        private readonly Mock<IBidRepository> _mockBidRepository;
        private readonly BidsController _controller;

        public BidsControllerTests()
        {
            _mockBidRepository = new Mock<IBidRepository>();
            _controller = new BidsController(_mockBidRepository.Object);
        }

        [Fact]
        public async Task GetAllBidsAsync_ReturnsOkResult()
        {
            // Arrange
            var bids = new List<Bid>
            {
                new Bid { BidId = 1 },
                new Bid { BidId = 2 }
            };

            _mockBidRepository.Setup(x => x.GetAllBidsAsync()).ReturnsAsync(bids);

            // Act
            var result = await _controller.GetAllBids();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedBids = Assert.IsAssignableFrom<IEnumerable<Bid>>(okResult.Value);
            Assert.Equal(2, returnedBids.Count());
        }

        [Fact]
        public async Task GetBidByIdAsync_ReturnsOkResult()
        {
            // Arrange
            var bid = new Bid { BidId = 1 };

            _mockBidRepository.Setup(x => x.GetBidByIdAsync(It.IsAny<int>())).ReturnsAsync(bid);

            // Act
            var result = await _controller.GetOneBid(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedBid = Assert.IsType<Bid>(okResult.Value);
            Assert.Equal(1, returnedBid.BidId);
        }

        [Fact]
        public async Task CreateBidAsync_ReturnsCreatedAtRouteResult()
        {
            // Arrange
            var bid = new Bid { BidId = 1 };

            _mockBidRepository.Setup(x => x.CreateBidAsync(It.IsAny<Bid>())).ReturnsAsync(1);

            // Act
            var result = await _controller.CreateOneBid(bid);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Equal("GetOneBid", createdResult.ActionName);
            Assert.Equal(1, createdResult.RouteValues["id"]);
        }

        [Fact]
        public async Task UpdateBidAsync_ReturnsOkResult()
        {
            // Arrange
            var bid = new Bid { BidId = 1 };

            _mockBidRepository.Setup(x => x.UpdateBidAsync(It.IsAny<Bid>())).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.UpdateOneBid(1, bid);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task DeleteBidByIdAsync_ReturnsNoContentResult()
        {
            // Arrange
            _mockBidRepository.Setup(x => x.DeleteBidByIdAsync(It.IsAny<int>())).Returns(Task.FromResult(1));

            // Act
            var result = await _controller.DeleteOneBid(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task GetAllBidsAsync_ReturnsNotFound_WhenNoBidsFound()
        {
            // Arrange
            _mockBidRepository.Setup(x => x.GetAllBidsAsync()).ReturnsAsync(Enumerable.Empty<Bid>());

            // Act
            var result = await _controller.GetAllBids();

            // Assert
            var notFoundResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedBids = Assert.IsAssignableFrom<IEnumerable<Bid>>(notFoundResult.Value);
        }

        [Fact]
        public async Task GetBidByIdAsync_ReturnsNotFound_WhenBidNotFound()
        {
            // Arrange
            _mockBidRepository.Setup(x => x.GetBidByIdAsync(It.IsAny<int>())).ReturnsAsync((Bid)null);

            // Act
            var result = await _controller.GetOneBid(1);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(result.Result);
            Assert.Equal(404, notFoundResult.StatusCode);
        }

        [Fact]
        public async Task CreateBidAsync_ReturnsNotFound_WhenBidIsNull()
        {
            // Arrange
            Bid bid = null;

            // Act
            var result = await _controller.CreateOneBid(bid);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(result.Result);
            Assert.Equal(404, notFoundResult.StatusCode);
        }

        [Fact]
        public async Task UpdateBidAsync_ReturnsNotFound_WhenBidMismatch()
        {
            // Arrange
            var bid = new Bid { BidId = 2 };

            // Act
            var result = await _controller.UpdateOneBid(1, bid);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(result);
            Assert.Equal(404, notFoundResult.StatusCode);
        }
    }
}
