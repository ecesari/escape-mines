using Moq;
using Service;
using Xunit;

namespace Test
{
    public class MineTests
    {
        [Theory]
        [InlineData("1,1 1,3 3,3")]
        public void Returns_MineExists(string value)
        {
            var boardServiceStub = new Mock<IBoardService>();
            var coordinateServiceStub = new Mock<ICoordinateService>();
            var mineService = new MineService(boardServiceStub.Object,coordinateServiceStub.Object);
            mineService.Create(value);

            //add board test
        }
    }
}