using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorldGeneticAlgorithm
{
    class Chromosome : IComparable {
        public const int ASCII_START = 32;
        public const int ASCII_END = 127;
        public static Random rnd;
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
                _chromosome += (char)rnd.Next(ASCII_START, ASCII_END);
            }
            calcFitness(goal);
        }

        private void calcFitness(string goal) {
            _fitness = 0;
            for(int i = 0; i < goal.Length; i++) {
                if (_chromosome[i] != goal[i]) _fitness++;
            }
        }

        public int CompareTo(object obj) {
            if (fitness < ((Chromosome)obj).fitness) {
                return -1;
            } else if (fitness > ((Chromosome)obj).fitness) {
                return 1;
            } else {
                return 0;
            }
        }
    }

    class Population {
        private int _popSize;
        private Chromosome[] population;

        public Population(string goal, int popSize) {
            _popSize = popSize;
            population = new Chromosome[_popSize];

            for(int i = 0; i < _popSize; i++) {
                population[i] = new Chromosome(goal);
            }
        }

        public void print() {
            //population.OrderBy(item => item.fitness);
            Array.Sort(population);
            foreach(Chromosome c in population) {
                Console.Out.WriteLine(c.chromosome + " - " + c.fitness);
            }
        }

        public Chromosome getBestChrome() {
            Array.Sort(population);
            return population[0];
        }
    }

    class Program
    {
        static void Main(string[] args) {
            string goal = "Hello, world!";

            Chromosome.rnd = new Random();

            //generate starting set
            //find fitness of population
            Population pop = new Population(goal, 100);
            pop.print();

            //until goal is met
            int bestFitness = pop.getBestChrome().fitness;
            while(bestFitness > 0) {
                //parent selection
                //crossover with probability pc
                //mutation with probability pm
                //decode and fitness calculation
                //survivor selection
                //find best
            }
        }
    }
}
