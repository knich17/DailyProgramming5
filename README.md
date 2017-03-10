# Day 5 - Hello World Genetic Algorithm
[Found at /r/dailyprogrammer](https://www.reddit.com/r/dailyprogrammer/comments/40rs67/20160113_challenge_249_intermediate_hello_world/)

## Problem Description
Use either an Evolutionary or Genetic Algorithm to evolve a solution to the fitness functions provided!

### Input description
The input string should be the target string you want to evolve the initial random solution into.
The target string (and therefore input) will be
'Hello, world!'
However, you want your program to initialize the process by randomly generating a string of the same length as the input. The only thing you want to use the input for is to determine the fitness of your function, so you don't want to just cheat by printing out the input string!

## Solution
As this was my first attempt at a genetic algorithm, I found [some info](https://www.tutorialspoint.com/genetic_algorithms/genetic_algorithms_fundamentals.htm) online that greatly helped me outline the program. 

The basic genetic algorithm approach used was a generation approach that starts by initializing the population before culling it (in this case in half), selecting parents to create children in order to fill the next generation population and stopping if any of these children are the goal solution.

When two parents have been selected (using the Roulette Wheel Selection method), weighted uniform gene crossover is then used to create the child chromosome as a combination of the parents. Mutations are also applied to the children with a specified probability (1/25 was used but this needs to scale with string length). The mutations at this point is simply randomizing a single allele.

## Improvements
Better mutations/mutation probabilty among other things should probably help bring the required generations down. I'd like the generations to be below 50.

## Learned
Genetic Algorithms and how to implement them (had no existing knowledge on this topic).