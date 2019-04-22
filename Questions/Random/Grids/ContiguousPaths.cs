namespace Grids
{
    using System;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ContiguousPaths
    {
        private readonly char[,] grid =
        {
	        { 'E', 'E', 'F', 'F', 'F' },
	        { 'E', 'F', 'F', 'E', 'F' },
	        { 'E', 'F', 'F', 'F', 'F' },
	        { 'E', 'E', 'E', 'E', 'E' },
	        { 'E', 'F', 'F', 'F', 'E' }
        };

        [TestMethod]
        public void FindConnectedBlocks()
        {
            PrintGrid(title: "Before");
            var positions = new List<(int row, int col)>();
            FindConnectedBlocksFromPosition(row: 1, col: 1, positions);
            PrintGrid(title: "After");
            Console.WriteLine("Positions visited");
            Console.WriteLine(string.Join(Environment.NewLine, positions));
        }

        private void PrintGrid(string title)
        {
            Console.WriteLine(title);
            for (int row = 0; row < grid.GetLength(0); row++)
            {
                for (int col = 0; col < grid.GetLength(1); col++)
                {
                    Console.Write(grid[row, col]);
                }
                Console.WriteLine();
            }
        }

        public void FindConnectedBlocksFromPosition(int row, int col, ICollection<(int row, int col)> positions)
        {

	        positions.Add((row, col));
	        grid[row,col] = 'V'; // visited

	        // go left
	        int nextRow = row;
	        int nextCol = col-1;
	        if (nextRow >= 0 && nextRow < grid.GetLength(0) && nextCol >= 0 && nextCol < grid.GetLength(1) && grid[nextRow, nextCol] == 'F')
		        FindConnectedBlocksFromPosition(nextRow, nextCol, positions);
	
	        // go right
	        nextCol = col+1;
	        if (nextRow >= 0 && nextRow < grid.GetLength(0) && nextCol >= 0 && nextCol < grid.GetLength(1) && grid[nextRow, nextCol] == 'F')
		        FindConnectedBlocksFromPosition(nextRow, nextCol, positions);
	
	        // go up
	        nextRow = row-1;
	        nextCol = col;
	        if (nextRow >= 0 && nextRow < grid.GetLength(0) && nextCol >= 0 && nextCol < grid.GetLength(1) && grid[nextRow, nextCol] == 'F')
		        FindConnectedBlocksFromPosition(nextRow, nextCol, positions);
	
	        // go down
	        nextRow = row+1;
	        if (nextRow >= 0 && nextRow < grid.GetLength(0) && nextCol >= 0 && nextCol < grid.GetLength(1) && grid[nextRow, nextCol] == 'F')
		        FindConnectedBlocksFromPosition(nextRow, nextCol, positions);
        }
    }
}
