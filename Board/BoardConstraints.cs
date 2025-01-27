namespace Sudoku
{
    public class BoardConstraints
    {
        private readonly int[] RowConstraints;
        private readonly int[] ColConstraints;
        private readonly int[] CubeConstraints;
        private readonly int Size;
        private readonly int CubeSize;

        public BoardConstraints(int size)
        {
            Size = size;
            CubeSize = (int)Math.Sqrt(size);
            RowConstraints = new int[size];
            ColConstraints = new int[size];
            CubeConstraints = new int[size];
        }

        public void SetValue(int row, int col, int value)
        {
            int valueMask = 1 << (value - 1);
            int cubeIndex = GetCubeIndex(row, col);

            RowConstraints[row] |= valueMask;
            ColConstraints[col] |= valueMask;
            CubeConstraints[cubeIndex] |= valueMask;
        }

        public void ClearValue(int row, int col, int value)
        {
            int valueMask = ~(1 << (value - 1));
            int cubeIndex = GetCubeIndex(row, col);

            RowConstraints[row] &= valueMask;
            ColConstraints[col] &= valueMask;
            CubeConstraints[cubeIndex] &= valueMask;
        }

        public bool CanPlaceValue(int row, int col, int value)
        {
            int valueMask = 1 << (value - 1);
            int cubeIndex = GetCubeIndex(row, col);

            return (RowConstraints[row] & valueMask) == 0 &&
                   (ColConstraints[col] & valueMask) == 0 &&
                   (CubeConstraints[cubeIndex] & valueMask) == 0;
        }

        private int GetCubeIndex(int row, int col)
        {
            return (row / CubeSize) * CubeSize + (col / CubeSize);
        }

        public void Reset()
        {
            Array.Clear(RowConstraints, 0, Size);
            Array.Clear(ColConstraints, 0, Size);
            Array.Clear(CubeConstraints, 0, Size);
        }
    }
}
