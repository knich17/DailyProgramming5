using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorldGeneticAlgorithm
{
    class Chromosome {
        public const int ASCII_START = 32;
        public const int ASCII_END = 127;
        private string _chromosome;
        public string chromosome {
            get {
                return _chromosome;
            }
        }
        private int _fitness;

        public int fitness {
            get {
                return _fitness;
            }
        }

        public Chromosome() { _chromosome = ""; }
        public Chromosome(string goal) {
            _chromosome = "";
            for(int i = 0; i < goal.Length; i++) {
                _chromosome += (char)Population.rnd.Next(ASCII_START, ASCII_END);
            }
            calcFitness(goal);
        }

        public Chromosome(string goal, Chromosome parentA, Chromosome parentB) {
            _chromosome = "";

            double chanceToPickA = 0.5;
            if (parentA.fitness <= parentB.fitness) {
                chanceToPickA = ((double)(parentA.fitness-1) / (parentB.fitness-1)) / 2.0;
            } else {
                chanceToPickA = 1.0-(((double)(parentB.fitness-1) / (parentA.fitness-1)) / 2.0);
            }
            chanceToPickA *= 100;
            //chanceToPickA = 50;

            int mutRate = 1000; //(1 / 1000)
            for(int i = 0; i < parentA.chromosome.Length; i++) {
                if(Population.rnd.Next(0,mutRate) < 1) {
                    _chromosome += (char)Population.rnd.Next(ASCII_START, ASCII_END);
                } else if(Population.rnd.Next(0,100) < chanceToPickA) {
                    _chromosome += parentA.chromosome[i];
                } else {
                    _chromosome += parentB.chromosome[i];
                }
            }

            calcFitness(goal);
        }

        private void calcFitness(string goal) {
            _fitness = 1;
            for(int i = 0; i < goal.Length; i++) {
                if (_chromosome[i] == goal[i]) _fitness++;
            }
        }
    }

    class Population {
        private const double CULL_FACTOR = 0.5;
        public static Random rnd;
        private int _popSize;
        private Chromosome[] population;

        public Population(string goal, int popSize) {
            _popSize = popSize;
            population = new Chromosome[_popSize];

            for(int i = 0; i < _popSize; i++) {
                population[i] = new Chromosome(goal);
            }
        }

        private void sortPop() {
            population.OrderByDescending(item => item.fitness);
        }

        public void print() {
            sortPop();
            foreach(Chromosome c in population) {
                if (c != null) Console.Out.WriteLine(c.chromosome + " - " + c.fitness);
            }
        }

        public Chromosome getBestChrome() {
            sortPop();
            return population[0];
        }

        public void cull() {
            for (int i = _popSize-1; i > (_popSize * CULL_FACTOR)-1; i--) {
                population[i] = null;
            }
        }

        //Could I instead, make only enough populations to fill the population (half as many)
        //And merge them with the existing population? Or would this stall progress as the existing population
        //Has not been mutated.
        public void boomBabies(string goal) {
            Chromosome[] tempPopulation = new Chromosome[_popSize];
            int finesses = calcFinesses();

            for(int i = 0; i < _popSize; i++) {
                tempPopulation[i] = new Chromosome(goal, findParent(finesses), findParent(finesses));
            }
            population = tempPopulation;
        }

        private Chromosome findParent() {
            return findParent(calcFinesses());
        }

        private int calcFinesses() {
            int finesses = 0;
            foreach(Chromosome c in population) {
                if (c != null) finesses += c.fitness;
            }
            return finesses;
        }

        private Chromosome findParent(int finesses) {
            int p = rnd.Next(0, finesses);
            int i = -1;
            while(p < finesses) {
                i++;
                if (population[i] != null) p += population[i].fitness;
            }
            return population[i];
        }
    }

    class Program
    {
        static void Main(string[] args) {
            string goal = "I think, therefore I am.";

            Population.rnd = new Random();

            //generate starting set
            //find fitness of population
            Population pop = new Population(goal, 10000);

            //until goal is met
            Chromosome best = pop.getBestChrome();
            Console.Out.WriteLine("Gen1: " + best.chromosome + " - " + best.fitness);
            int bestFitness = best.fitness;
            int gen = 1;
            while (!best.chromosome.Equals(goal)) {
                gen++;
                pop.cull();
                pop.boomBabies(goal);
                Chromosome tempC = pop.getBestChrome();
                if(tempC.fitness != best.fitness) {
                    Console.Out.WriteLine("Gen" + gen + ": " + tempC.chromosome + " - " + tempC.fitness);
                }
                best = tempC;
                //parent selection
                //crossover with probability pc
                //mutation with probability pm
                //decode and fitness calculation
                //survivor selection
                //find best
            }
            while (true) ;
        }
    }
}
