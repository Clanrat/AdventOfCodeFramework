using System;
using System.IO;

namespace AoCFramework
{
    /// <summary>
    /// Extension methods for <see cref="SolutionRunner{TSolution,TResult}"/>
    /// </summary>
    public static class SolutionRunnerExtensions
    {
        /// <summary>
        /// Adds a single test case to the runner
        /// </summary>
        /// <typeparam name="TSolution"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="runner"></param>
        /// <param name="input">Test case puzzle input, can be a file or raw data</param>
        /// <param name="expected">The expected answer</param>
        /// <returns></returns>
        public static SolutionRunner<TSolution, TResult> AddTestCase<TSolution, TResult>(this SolutionRunner<TSolution, TResult> runner,
            string input, TResult expected) where TSolution : ISolution<TResult>, new ()
        {
            try
            {
                var realInput = GetInput(input);

                runner.TestCases.Add(realInput, expected);
            }
            catch (FileNotFoundException)
            {
                ConsoleUtils.WriteLineInColor($"Could not find file {input}", ConsoleColor.Red);
            }
            return runner;


        }

        /// <summary>
        /// Sets the main input, for use in the fluent API
        /// </summary>
        /// <typeparam name="TSolution"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="runner"></param>
        /// <param name="input">Main puzzle input can be file or raw data</param>
        /// <returns></returns>
        public static SolutionRunner<TSolution, TResult> SetMainInput<TSolution, TResult>(
            this SolutionRunner<TSolution, TResult> runner, string input) where TSolution : ISolution<TResult>, new()
        {

            try
            {
                var realInput = GetInput(input);

                runner.MainInput = realInput;
            }
            catch (FileNotFoundException)
            {
                ConsoleUtils.WriteLineInColor($"Could not find file {input}", ConsoleColor.Red);
            }
            return runner;

        }

        private static string GetInput(string inputOrPath)
        {
            if (IsInputAFile(inputOrPath))
            {
                return File.ReadAllText(inputOrPath);
            }

            return inputOrPath;
        }


        private static bool IsInputAFile(string input)
        {
            return File.Exists(input);
        } 
    }
}
