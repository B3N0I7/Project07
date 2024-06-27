using AutoFixture;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PostTradesThree.Controllers;
using PostTradesThree.Dtos;
using PostTradesThree.Services;


namespace PostTradesThree.Tests.Controllers
{
    public class AuthControllerTests
    {
        private readonly Mock<IAuthService> _mockAuthService;
        private readonly Fixture _fixture;
        private readonly AuthController _controller;

        public AuthControllerTests()
        {
            _mockAuthService = new Mock<IAuthService>();
            _fixture = new Fixture();
            _controller = new AuthController(_mockAuthService.Object);
        }

        [Fact]
        public async Task SeedRoles_ShouldReturnOkResult()
        {
            // Arrange
            var responseDto = _fixture.Create<AuthServiceResponseDto>();
            _mockAuthService.Setup(x => x.SeedRolesAsync()).ReturnsAsync(responseDto);

            // Act
            var result = await _controller.SeedRoles();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(responseDto, okResult.Value);
        }

        [Fact]
        public async Task RegisterAsync_ShouldReturnOkResult_WhenRegistrationSucceeds()
        {
            // Arrange
            var registerDto = _fixture.Create<RegisterDto>();
            var responseDto = _fixture.Create<AuthServiceResponseDto>();
            responseDto.IsSucceed = true;
            _mockAuthService.Setup(x => x.RegisterAsync(registerDto)).ReturnsAsync(responseDto);

            // Act
            var result = await _controller.RegisterAsync(registerDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(responseDto, okResult.Value);
        }

        [Fact]
        public async Task RegisterAsync_ShouldReturnBadRequest_WhenRegistrationFails()
        {
            // Arrange
            var registerDto = _fixture.Create<RegisterDto>();
            var responseDto = _fixture.Create<AuthServiceResponseDto>();
            responseDto.IsSucceed = false;
            _mockAuthService.Setup(x => x.RegisterAsync(registerDto)).ReturnsAsync(responseDto);

            // Act
            var result = await _controller.RegisterAsync(registerDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(responseDto, badRequestResult.Value);
        }

        [Fact]
        public async Task LoginAsync_ShouldReturnOkResult_WhenLoginSucceeds()
        {
            // Arrange
            var loginDto = _fixture.Create<LoginDto>();
            var responseDto = _fixture.Create<AuthServiceResponseDto>();
            responseDto.IsSucceed = true;
            _mockAuthService.Setup(x => x.LoginAsync(loginDto)).ReturnsAsync(responseDto);

            // Act
            var result = await _controller.LoginAsync(loginDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(responseDto, okResult.Value);
        }

        [Fact]
        public async Task LoginAsync_ShouldReturnUnauthorizedResult_WhenLoginFails()
        {
            // Arrange
            var loginDto = _fixture.Create<LoginDto>();
            var responseDto = _fixture.Create<AuthServiceResponseDto>();
            responseDto.IsSucceed = false;
            _mockAuthService.Setup(x => x.LoginAsync(loginDto)).ReturnsAsync(responseDto);

            // Act
            var result = await _controller.LoginAsync(loginDto);

            // Assert
            var unauthorizedResult = Assert.IsType<UnauthorizedObjectResult>(result);
            Assert.Equal(responseDto, unauthorizedResult.Value);
        }

        [Fact]
        public async Task MakeAdminAsync_ShouldReturnOkResult_WhenOperationSucceeds()
        {
            // Arrange
            var updateRoleDto = _fixture.Create<UpdateRoleDto>();
            var responseDto = _fixture.Create<AuthServiceResponseDto>();
            responseDto.IsSucceed = true;
            _mockAuthService.Setup(x => x.MakeAdminAsync(updateRoleDto)).ReturnsAsync(responseDto);

            // Act
            var result = await _controller.MakeAdminAsync(updateRoleDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(responseDto, okResult.Value);
        }

        [Fact]
        public async Task MakeAdminAsync_ShouldReturnBadRequest_WhenOperationFails()
        {
            // Arrange
            var updateRoleDto = _fixture.Create<UpdateRoleDto>();
            var responseDto = _fixture.Create<AuthServiceResponseDto>();
            responseDto.IsSucceed = false;
            _mockAuthService.Setup(x => x.MakeAdminAsync(updateRoleDto)).ReturnsAsync(responseDto);

            // Act
            var result = await _controller.MakeAdminAsync(updateRoleDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(responseDto, badRequestResult.Value);
        }

        [Fact]
        public async Task MakeOwnerAsync_ShouldReturnOkResult_WhenOperationSucceeds()
        {
            // Arrange
            var updateRoleDto = _fixture.Create<UpdateRoleDto>();
            var responseDto = _fixture.Create<AuthServiceResponseDto>();
            responseDto.IsSucceed = true;
            _mockAuthService.Setup(x => x.MakeOwnerAsync(updateRoleDto)).ReturnsAsync(responseDto);

            // Act
            var result = await _controller.MakeOwnerAsync(updateRoleDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(responseDto, okResult.Value);
        }

        [Fact]
        public async Task MakeOwnerAsync_ShouldReturnBadRequest_WhenOperationFails()
        {
            // Arrange
            var updateRoleDto = _fixture.Create<UpdateRoleDto>();
            var responseDto = _fixture.Create<AuthServiceResponseDto>();
            responseDto.IsSucceed = false;
            _mockAuthService.Setup(x => x.MakeOwnerAsync(updateRoleDto)).ReturnsAsync(responseDto);

            // Act
            var result = await _controller.MakeOwnerAsync(updateRoleDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(responseDto, badRequestResult.Value);
        }

        [Fact]
        public async Task LogoutAsync_ShouldReturnOkResult()
        {
            // Act
            var result = await _controller.LogoutAsync();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Logout successful", okResult.Value);
        }
    }
}
