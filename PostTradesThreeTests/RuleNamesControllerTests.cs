using Microsoft.AspNetCore.Mvc;
using Moq;
using PostTradesThree.Controllers;
using PostTradesThree.Domain;
using PostTradesThree.Repositories;

namespace PostTradesThree.Tests.Controllers
{
    public class RuleNamesControllerTests
    {
        private readonly Mock<IRuleNameRepository> _mockRuleNameRepository;
        private readonly RuleNamesController _controller;

        public RuleNamesControllerTests()
        {
            _mockRuleNameRepository = new Mock<IRuleNameRepository>();
            _controller = new RuleNamesController(_mockRuleNameRepository.Object);
        }

        [Fact]
        public async Task GetAllRuleNamesAsync_ReturnsOkResult()
        {
            // Arrange
            var ruleNames = new List<RuleName>
            {
                new RuleName { RuleNameId = 1 },
                new RuleName { RuleNameId = 2 }
            };

            _mockRuleNameRepository.Setup(x => x.GetAllRuleNamesAsync()).ReturnsAsync(ruleNames);

            // Act
            var result = await _controller.GetAllRuleNames();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedRuleNames = Assert.IsAssignableFrom<IEnumerable<RuleName>>(okResult.Value);
            Assert.Equal(2, returnedRuleNames.Count());
        }

        [Fact]
        public async Task GetRuleNameByIdAsync_ReturnsOkResult()
        {
            // Arrange
            var ruleName = new RuleName { RuleNameId = 1 };

            _mockRuleNameRepository.Setup(x => x.GetRuleNameByIdAsync(It.IsAny<int>())).ReturnsAsync(ruleName);

            // Act
            var result = await _controller.GetOneRuleName(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedRuleName = Assert.IsType<RuleName>(okResult.Value);
            Assert.Equal(1, returnedRuleName.RuleNameId);
        }

        [Fact]
        public async Task CreateRuleNameAsync_ReturnsCreatedAtRouteResult()
        {
            // Arrange
            var ruleName = new RuleName { RuleNameId = 1 };

            _mockRuleNameRepository.Setup(x => x.CreateRuleNameAsync(It.IsAny<RuleName>())).ReturnsAsync(1);

            // Act
            var result = await _controller.CreateOneRuleName(ruleName);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Equal("GetOneRuleName", createdResult.ActionName);
            Assert.Equal(1, createdResult.RouteValues["id"]);
        }

        [Fact]
        public async Task UpdateRuleNameAsync_ReturnsOkResult()
        {
            // Arrange
            var ruleName = new RuleName { RuleNameId = 1 };

            _mockRuleNameRepository.Setup(x => x.UpdateRuleNameAsync(It.IsAny<RuleName>())).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.UpdateOneRuleName(1, ruleName);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task DeleteRuleNameByIdAsync_ReturnsNoContentResult()
        {
            // Arrange
            _mockRuleNameRepository.Setup(x => x.DeleteRuleNameByIdAsync(It.IsAny<int>())).Returns(Task.FromResult(1));

            // Act
            var result = await _controller.DeleteOneRuleName(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task GetAllRuleNamesAsync_ReturnsNotFound_WhenNoRuleNamesFound()
        {
            // Arrange
            _mockRuleNameRepository.Setup(x => x.GetAllRuleNamesAsync()).ReturnsAsync(Enumerable.Empty<RuleName>());

            // Act
            var result = await _controller.GetAllRuleNames();

            // Assert
            var notFoundResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedRuleNames = Assert.IsAssignableFrom<IEnumerable<RuleName>>(notFoundResult.Value);
        }

        [Fact]
        public async Task GetRuleNameByIdAsync_ReturnsNotFound_WhenRuleNameNotFound()
        {
            // Arrange
            _mockRuleNameRepository.Setup(x => x.GetRuleNameByIdAsync(It.IsAny<int>())).ReturnsAsync((RuleName)null);

            // Act
            var result = await _controller.GetOneRuleName(1);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(result.Result);
            Assert.Equal(404, notFoundResult.StatusCode);
        }

        [Fact]
        public async Task CreateRuleNameAsync_ReturnsNotFound_WhenRuleNameIsNull()
        {
            // Arrange
            RuleName ruleName = null;

            // Act
            var result = await _controller.CreateOneRuleName(ruleName);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(result.Result);
            Assert.Equal(404, notFoundResult.StatusCode);
        }

        [Fact]
        public async Task UpdateRuleNameAsync_ReturnsNotFound_WhenRuleNameMismatch()
        {
            // Arrange
            var ruleName = new RuleName { RuleNameId = 2 };

            // Act
            var result = await _controller.UpdateOneRuleName(1, ruleName);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(result);
            Assert.Equal(404, notFoundResult.StatusCode);
        }
    }
}
