using Domain;
using Helper.Enums;

namespace Service
{
    public interface IMineService
    {
        Mine CreateMine(int[] mineCoordinates);
        void Detonate(Mine mine);
    }
    public class MineService : IMineService
    {
        private readonly ICoordinateService _coordinateService;

        public MineService(ICoordinateService coordinateService)
        {
            _coordinateService = coordinateService;
        }
        public Mine CreateMine(int[] mineCoordinates)
        {
            var x = mineCoordinates[0];
            var y = mineCoordinates[1];

            var mine = new Mine
            {
                Position = _coordinateService.Create(x, y),
                Status = MineStatus.Active
            };

            return mine;
        }

        public void Detonate(Mine mine)
        {
            mine.Status = MineStatus.Detonated;
        }
    }
}
