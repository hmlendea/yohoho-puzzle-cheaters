using YohohoPuzzleCheaters.Cheats.Patching.Entities;

namespace YohohoPuzzleCheaters.Cheats.Patching
{
    public class PatchingSolver
    {
        public PatchingBoard CalculateSolution(PatchingBoard board)
        {
            PatchingBoard solution = board.CreateCopy();

            return solution;
        }
    }
}
