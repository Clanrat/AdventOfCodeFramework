# AdventOfCodeFramework
Csharp framework with fluent API for writing and running solutions for the advent of code event

## Useage
Clone the repo and refrence the project in your solution

Example:


```C#
//Have your solution class derive from ISolution<TResult>

using System;
using AoCFramework;


public class Part1 : ISolution<string>
{
    public virtual string Solve(string input)
    {
        return "foo";
    }
}

//Create a new runner and register any test cases along with where to find your puzzle input
class Program
  {
      static void Main(string[] args)
      {
          //Register any tests along with the main input
          var part1 = new SolutionRunner<Part1, int>()
              .AddTestCase("bar", "bar")
              .AddTestCase("foo", "foo")
              .SetMainInput(@"C:\Users\UserNameHere\Desktop\AoC2016Input\day1\1.txt");

          //You can also have test data in files, or input the raw puzzle data to the main run
          var part1alternate = new SolutionRunner<Part1, int>()
              .AddTestCase(@"C:\Users\UserNameHere\Desktop\AoC2016Input\day1\test\1.txt","bar")
              .SetMainInput("foo");
              
          //To run your solution, specify if you want your tests to run as well
          part1.Run(true);
          
          //To get your result after the run
          var result part1.Result;
          
          /*
          The console will output:
          Starting Run
          Running 2 tests...
          
          Test failed: Expected bar, got foo, Time: 0ms
          Test passed! Time 0ms
          
          Running main run...
          
          Main run finished!
          Result was: foo, run took 0ms
          Run finished press enter to continue          
          */
      }
  }
    
  
```
