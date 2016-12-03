namespace AoCFramework
{
    /// <summary>
    /// Solution for an advent of code puzzle
    /// </summary>
    /// <typeparam name="TResult">Type of the answer returned from solve</typeparam>
    public interface ISolution<out TResult>
    {
        /// <summary>
        /// Solves the puzzle for the given input
        /// </summary>
        /// <param name="input">Puzzle input</param>
        /// <returns>Result from solving the puzzle</returns>
        TResult Solve(string input);
    }
}
