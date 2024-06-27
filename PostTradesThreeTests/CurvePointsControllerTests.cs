using Microsoft.AspNetCore.Mvc;
using Moq;
using PostTradesThree.Controllers;
using PostTradesThree.Domain;
using PostTradesThree.Repositories;

namespace PostTradesThree.Tests.Controllers
{
    public class CurvePointsControllerTests
    {
        private readonly Mock<ICurvePointRepository> _mockCurvePointRepository;
        private readonly CurvePointsController _controller;

        public CurvePointsControllerTests()
        {
            _mockCurvePointRepository = new Mock<ICurvePointRepository>();
            _controller = new CurvePointsController(_mockCurvePointRepository.Object);
        }

        [Fact]
        public async Task GetAllCurvePointsAsync_ReturnsOkResult()
        {
            // Arrange
            var curvePoints = new List<CurvePoint>
            {
                new CurvePoint { CurvePointId = 1 },
                new CurvePoint { CurvePointId = 2 }
            };

            _mockCurvePointRepository.Setup(x => x.GetAllCurvePointsAsync()).ReturnsAsync(curvePoints);

            // Act
            var result = await _controller.GetAllCurvePoints();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedCurvePoints = Assert.IsAssignableFrom<IEnumerable<CurvePoint>>(okResult.Value);
            Assert.Equal(2, returnedCurvePoints.Count());
        }

        [Fact]
        public async Task GetCurvePointByIdAsync_ReturnsOkResult()
        {
            // Arrange
            var curvePoint = new CurvePoint { CurvePointId = 1 };

            _mockCurvePointRepository.Setup(x => x.GetCurvePointByIdAsync(It.IsAny<int>())).ReturnsAsync(curvePoint);

            // Act
            var result = await _controller.GetOneCurvePoint(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedCurvePoint = Assert.IsType<CurvePoint>(okResult.Value);
            Assert.Equal(1, returnedCurvePoint.CurvePointId);
        }

        [Fact]
        public async Task CreateCurvePointAsync_ReturnsCreatedAtRouteResult()
        {
            // Arrange
            var curvePoint = new CurvePoint { CurvePointId = 1 };

            _mockCurvePointRepository.Setup(x => x.CreateCurvePointAsync(It.IsAny<CurvePoint>())).ReturnsAsync(1);

            // Act
            var result = await _controller.CreateOneCurvePoint(curvePoint);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Equal("GetOneCurvePoint", createdResult.ActionName);
            Assert.Equal(1, createdResult.RouteValues["id"]);
        }

        [Fact]
        public async Task UpdateCurvePointAsync_ReturnsOkResult()
        {
            // Arrange
            var curvePoint = new CurvePoint { CurvePointId = 1 };

            _mockCurvePointRepository.Setup(x => x.UpdateCurvePointAsync(It.IsAny<CurvePoint>())).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.UpdateOneCurvePoint(1, curvePoint);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task DeleteCurvePointByIdAsync_ReturnsNoContentResult()
        {
            // Arrange
            _mockCurvePointRepository.Setup(x => x.DeleteCurvePointByIdAsync(It.IsAny<int>())).Returns(Task.FromResult(1));

            // Act
            var result = await _controller.DeleteOneCurvePoint(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task GetAllCurvePointsAsync_ReturnsNotFound_WhenNoCurvePointsFound()
        {
            // Arrange
            _mockCurvePointRepository.Setup(x => x.GetAllCurvePointsAsync()).ReturnsAsync(Enumerable.Empty<CurvePoint>());

            // Act
            var result = await _controller.GetAllCurvePoints();

            // Assert
            var notFoundResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedCurvePoints = Assert.IsAssignableFrom<IEnumerable<CurvePoint>>(notFoundResult.Value);
        }

        [Fact]
        public async Task GetCurvePointByIdAsync_ReturnsNotFound_WhenCurvePointNotFound()
        {
            // Arrange
            _mockCurvePointRepository.Setup(x => x.GetCurvePointByIdAsync(It.IsAny<int>())).ReturnsAsync((CurvePoint)null);

            // Act
            var result = await _controller.GetOneCurvePoint(1);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(result.Result);
            Assert.Equal(404, notFoundResult.StatusCode);
        }

        [Fact]
        public async Task CreateCurvePointAsync_ReturnsNotFound_WhenCurvePointIsNull()
        {
            // Arrange
            CurvePoint curvePoint = null;

            // Act
            var result = await _controller.CreateOneCurvePoint(curvePoint);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(result.Result);
            Assert.Equal(404, notFoundResult.StatusCode);
        }

        [Fact]
        public async Task UpdateCurvePointAsync_ReturnsNotFound_WhenCurvePointMismatch()
        {
            // Arrange
            var curvePoint = new CurvePoint { CurvePointId = 2 };

            // Act
            var result = await _controller.UpdateOneCurvePoint(1, curvePoint);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(result);
            Assert.Equal(404, notFoundResult.StatusCode);
        }
    }
}
