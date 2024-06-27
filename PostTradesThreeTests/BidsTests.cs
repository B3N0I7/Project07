using Moq;
using PostTradesThree.Domain;
using PostTradesThree.Repositories;

namespace PostTradesThreeTests
{
    public class BidsTests
    {
        private readonly Mock<IBidRepository> bidRepository;

        public BidsTests()
        {
            bidRepository = new Mock<IBidRepository>();
        }

        [Fact]
        public async Task GetBidsAsync_ReturnsListOfBids()
        {
            // Arrange
            var expectedBids = new List<Bid>(); // Définir ici la liste de vos listes d'offres attendues
            var repositoryMock = new Mock<IBidRepository>();
            repositoryMock.Setup(repo => repo.GetAllBidsAsync()).ReturnsAsync(expectedBids);
            var repository = repositoryMock.Object;

            // Act
            var result = await repository.GetAllBidsAsync();

            // Assert
            Assert.Equal(expectedBids, result);
        }

        [Fact]
        public async Task GetByIdAsync_WithValidId_ReturnsBid()
        {
            // Arrange
            int id = 1; // Spécifiez l'ID valide d'une liste d'offres existante
            var expectedBid = new Bid(); // Définir ici la liste d'offres attendue avec l'ID spécifié
            var repositoryMock = new Mock<IBidRepository>();
            repositoryMock.Setup(repo => repo.GetBidByIdAsync(id)).ReturnsAsync(expectedBid);
            var repository = repositoryMock.Object;

            // Act
            var result = await repository.GetBidByIdAsync(id);

            // Assert
            Assert.Equal(expectedBid, result);
        }

        [Fact]
        public async Task AddAsync_ValidBid_AddsToList()
        {
            // Arrange
            var bidListToAdd = new Bid(); // Créez ici une liste d'offres valide à ajouter
            var repositoryMock = new Mock<IBidRepository>();
            repositoryMock.Setup(repo => repo.CreateBidAsync(bidListToAdd)).Verifiable();
            var repository = repositoryMock.Object;

            // Act
            await repository.CreateBidAsync(bidListToAdd);

            // Assert
            repositoryMock.Verify(repo => repo.CreateBidAsync(bidListToAdd), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_ValidBid_UpdatesList()
        {
            // Arrange
            var bidListToUpdate = new Bid(); // Créez ici une liste d'offres valide à mettre à jour
            var repositoryMock = new Mock<IBidRepository>();
            repositoryMock.Setup(repo => repo.UpdateBidAsync(bidListToUpdate)).Verifiable();
            var repository = repositoryMock.Object;

            // Act
            await repository.UpdateBidAsync(bidListToUpdate);

            // Assert
            repositoryMock.Verify(repo => repo.UpdateBidAsync(bidListToUpdate), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_WithValidId_DeletesBid()
        {
            // Arrange
            int idToDelete = 1; // Spécifiez l'ID valide de la liste d'offres à supprimer
            var repositoryMock = new Mock<IBidRepository>();
            repositoryMock.Setup(repo => repo.DeleteBidByIdAsync(idToDelete)).Verifiable();
            var repository = repositoryMock.Object;

            // Act
            await repository.DeleteBidByIdAsync(idToDelete);

            // Assert
            repositoryMock.Verify(repo => repo.DeleteBidByIdAsync(idToDelete), Times.Once);
        }
    }
}