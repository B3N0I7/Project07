using Microsoft.AspNetCore.Mvc;
using Moq;
using PostTradesThree.Controllers;
using PostTradesThree.Domain;
using PostTradesThree.Repositories;

namespace PostTradesThree.Tests.Controllers
{
    public class RatingsControllerTests
    {
        private readonly Mock<IRatingRepository> _mockRatingRepository;
        private readonly RatingsController _controller;

        public RatingsControllerTests()
        {
            _mockRatingRepository = new Mock<IRatingRepository>();
            _controller = new RatingsController(_mockRatingRepository.Object);
        }

        [Fact]
        public async Task GetAllRatingsAsync_ReturnsOkResult()
        {
            // Arrange
            var ratings = new List<Rating>
            {
                new Rating { RatingId = 1 },
                new Rating { RatingId = 2 }
            };

            _mockRatingRepository.Setup(x => x.GetAllRatingsAsync()).ReturnsAsync(ratings);

            // Act
            var result = await _controller.GetAllRatings();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedRatings = Assert.IsAssignableFrom<IEnumerable<Rating>>(okResult.Value);
            Assert.Equal(2, returnedRatings.Count());
        }

        [Fact]
        public async Task GetRatingByIdAsync_ReturnsOkResult()
        {
            // Arrange
            var rating = new Rating { RatingId = 1 };

            _mockRatingRepository.Setup(x => x.GetRatingByIdAsync(It.IsAny<int>())).ReturnsAsync(rating);

            // Act
            var result = await _controller.GetOneRating(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedRating = Assert.IsType<Rating>(okResult.Value);
            Assert.Equal(1, returnedRating.RatingId);
        }

        [Fact]
        public async Task CreateRatingAsync_ReturnsCreatedAtRouteResult()
        {
            // Arrange
            var rating = new Rating { RatingId = 1 };

            _mockRatingRepository.Setup(x => x.CreateRatingAsync(It.IsAny<Rating>())).ReturnsAsync(1);

            // Act
            var result = await _controller.CreateOneRating(rating);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Equal("GetOneRating", createdResult.ActionName);
            Assert.Equal(1, createdResult.RouteValues["id"]);
        }

        [Fact]
        public async Task UpdateRatingAsync_ReturnsOkResult()
        {
            // Arrange
            var rating = new Rating { RatingId = 1 };

            _mockRatingRepository.Setup(x => x.UpdateRatingAsync(It.IsAny<Rating>())).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.UpdateOneRating(1, rating);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task DeleteRatingByIdAsync_ReturnsNoContentResult()
        {
            // Arrange
            _mockRatingRepository.Setup(x => x.DeleteRatingByIdAsync(It.IsAny<int>())).Returns(Task.FromResult(1));

            // Act
            var result = await _controller.DeleteOneRating(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task GetAllRatingsAsync_ReturnsNotFound_WhenNoRatingsFound()
        {
            // Arrange
            _mockRatingRepository.Setup(x => x.GetAllRatingsAsync()).ReturnsAsync(Enumerable.Empty<Rating>());

            // Act
            var result = await _controller.GetAllRatings();

            // Assert
            var notFoundResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedRatings = Assert.IsAssignableFrom<IEnumerable<Rating>>(notFoundResult.Value);
        }

        [Fact]
        public async Task GetRatingByIdAsync_ReturnsNotFound_WhenRatingNotFound()
        {
            // Arrange
            _mockRatingRepository.Setup(x => x.GetRatingByIdAsync(It.IsAny<int>())).ReturnsAsync((Rating)null);

            // Act
            var result = await _controller.GetOneRating(1);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(result.Result);
            Assert.Equal(404, notFoundResult.StatusCode);
        }

        [Fact]
        public async Task CreateRatingAsync_ReturnsNotFound_WhenRatingIsNull()
        {
            // Arrange
            Rating rating = null;

            // Act
            var result = await _controller.CreateOneRating(rating);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(result.Result);
            Assert.Equal(404, notFoundResult.StatusCode);
        }

        [Fact]
        public async Task UpdateRatingAsync_ReturnsNotFound_WhenRatingMismatch()
        {
            // Arrange
            var rating = new Rating { RatingId = 2 };

            // Act
            var result = await _controller.UpdateOneRating(1, rating);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(result);
            Assert.Equal(404, notFoundResult.StatusCode);
        }
    }
}
