using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace AoCFramework
{
    /// <summary>
    /// Runs and verifies a solution to an advent of code puzzle
    /// </summary>
    /// <typeparam name="TSolution">Solution type, must derive from <see cref="ISolution{TResult}"/></typeparam>
    /// <typeparam name="TResult">Return type from <see cref="ISolution{TResult}"/></typeparam>
    public class SolutionRunner<TSolution,TResult> where TSolution : ISolution<TResult>, new()
    {
        /// <summary>
        /// Whenther to throw if an exception is caught during a run
        /// </summary>
        protected readonly bool ThrowOnException;

        /// <summary>
        /// Test inputs and their expected results
        /// </summary>
        protected internal Dictionary<string, TResult> TestCases { get; } = new Dictionary<string, TResult>();

        /// <summary>
        /// Main raw puzzle input
        /// </summary>
        public string MainInput { get; set; }

        /// <summary>
        /// Result from the run
        /// </summary>
        public TResult Result { get; private set; }

        public SolutionRunner(bool throwOnException = true)
        {
            ThrowOnException = throwOnException;
        }

        /// <summary>
        /// Starts the session
        /// </summary>
        /// <param name="runTests">Indicates if any registered tests should be run as well</param>
        public virtual void Run(bool runTests)
        {
            

            Console.WriteLine("Starting run");
            if (runTests)
            {
                Console.WriteLine($"Running {TestCases.Count} tests...\n");
                var testNumber = 1;
                foreach (var kvp in TestCases)
                {
                    RunTest(testNumber, kvp.Key, kvp.Value);
                    testNumber++;
                }
            }

            Console.WriteLine();

            if (!string.IsNullOrEmpty(MainInput))
            {
                try
                {
                    Console.WriteLine("Running main run...\n");

                    var result = RunSingle(MainInput);
                    Result = result.Result;
                       
                    ConsoleUtils.WriteLineInColor("Main run finised!", ConsoleColor.Green);
                    Console.WriteLine($"Result was: {result.Result}, run took {result.Time}ms");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Main run threw an exception: {ex}");
                    if (ThrowOnException)
                    {
                        throw;
                    }
                }

            }

            Console.WriteLine("Run finished press enter to continue");
            Console.ReadLine();
        }

        
        /// <summary>
        /// Runs a single test
        /// </summary>
        /// <param name="testNo">Test number in sequence added</param>
        /// <param name="input">Input for the test</param>
        /// <param name="expected">Expected value</param>
        protected virtual void RunTest(int testNo, string input, TResult expected)
        {
            RunResult result;
            try
            {

                result = RunSingle(input);
  
            }
            catch (Exception ex)
            {

                ConsoleUtils.WriteLineInColor($"Test {testNo} failed: Solution threw an exception {ex}", ConsoleColor.Red);

                if (ThrowOnException)
                {
                    throw;
                }

                return;
            }

            if (!result.Result.Equals(expected))
            {
                ConsoleUtils.WriteLineInColor($"Test {testNo} failed: Expected {expected}, Got {result.Result}, Time: {result.Time}ms", ConsoleColor.Red);
            }
            else
            {
                ConsoleUtils.WriteLineInColor($"Test {testNo} passed!, Time: {result.Time}ms", ConsoleColor.Green);
            }
        }


        /// <summary>
        /// Solves a single puzzle using <see cref="TSolution"/> for the given input
        /// </summary>
        /// <param name="input">Input for this puzzle</param>
        /// <returns><see cref="RunResult"/> containg the result of the run</returns>
        protected virtual RunResult RunSingle(string input)
        {
            var solution = new TSolution();
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var result = solution.Solve(input);
            stopwatch.Stop();

            return new RunResult
            {
                Result = result,
                Time = stopwatch.ElapsedMilliseconds
            };
        }


        /// <summary>
        /// Single result from solving a puzzle
        /// </summary>
        protected class RunResult
        {
            /// <summary>
            /// Puzzle answer
            /// </summary>
            public TResult Result { get; set; }
            /// <summary>
            /// Time it took to solve for this input
            /// </summary>
            public long Time { get; set; }


        }
        
    }
}
