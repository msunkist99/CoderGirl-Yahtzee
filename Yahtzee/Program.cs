using System;

namespace Yahtzee
{
    class Program
    {
        const int numberOfDice = 6;

        static void Main(string[] args)
        {
            int[] savedUserDice = new int[0];

            //  ask user for their name
            Console.WriteLine("Please enter your name - ");
            string userName = Console.ReadLine();

            //  user's first roll
            Console.WriteLine($"Hello {userName} - here is your first roll");
            int[] dice = RollOfTheDice(numberOfDice);
            //  display the six rolled dice
            DisplayTheRollResults(dice, numberOfDice);
           //  ask user which dice to keep
            savedUserDice = SaveSelectedDice(dice, savedUserDice, "first");
            DisplayTheSavedDice(savedUserDice, savedUserDice.Length, userName);

            //  user's second roll of remaining dice
            dice = RollOfTheDice(numberOfDice - savedUserDice.Length);
            //  display the remaining dice
            DisplayTheRollResults(dice, dice.Length);
            //  ask user which dice to keep
            savedUserDice = SaveSelectedDice(dice, savedUserDice, "second");
            DisplayTheSavedDice(savedUserDice, savedUserDice.Length, userName);

            // user's third roll of remaining dice
            dice = RollOfTheDice(numberOfDice - savedUserDice.Length);
            DisplayTheRollResults(dice, dice.Length);
            // ask user which dice to keep
            savedUserDice = SaveSelectedDice(dice, savedUserDice, "third");
            DisplayTheSavedDice(savedUserDice, savedUserDice.Length, userName);

            // display user's score
            int finalUserScore = CalculateUserScore(savedUserDice);
            Console.WriteLine($"{userName} -- final score -- {finalUserScore}");
            Console.WriteLine();

            //
            //
            // display message that computer will now roll
            Console.WriteLine("My turn - my name is HAL");

            int[][] savedComputerRolls = new int[3][];

            //  roll the dice three times for the computer - saving the results of each roll
            for (int i = 0; i < 3; i++)
            {
                dice = RollOfTheDice(numberOfDice);
                savedComputerRolls[i] = dice;
                DisplayTheSavedDice(savedComputerRolls[i], numberOfDice, "HAL");
            }
            
            int[] computerScore = new int[3];
            for (int i = 0; i < 3; i++)
            {
                computerScore[i] = CalculateUserScore(savedComputerRolls[i]);
            }

            int finalComputerScore = 0;
            for (int i = 0; i < 3; i++)
            {
                if (computerScore[i] > finalComputerScore)
                {
                    finalComputerScore = computerScore[i];
                }
            }

            Console.WriteLine($"HAL -- final score -- {finalComputerScore}");
            Console.WriteLine();
            
            // display the winner of our Yahtzee game
            if (finalUserScore > finalComputerScore)
            {
                Console.WriteLine($"{userName} wins!");
            }
            else if (finalComputerScore > finalUserScore)
            {
                Console.WriteLine("HAL wins!");
            }
            else
            {
                Console.WriteLine($"Looks like a tie --- we both have a score of {finalUserScore}");
            }

            Console.ReadLine();
        }

        /// <summary>
        /// simulates the roll of SIX sided dice
        /// </summary>
        /// <param name="diceCount">how many dice do you want to roll?</param>
        /// <returns>array of the dice roll results</returns>
        private static int[] RollOfTheDice(int diceCount)
        {
            int[] dice = new int[diceCount];

            Random random = new Random();

            for (int i = 0; i < diceCount; i++)
            {
                dice[i] = random.Next(1, 7);     // creates as random number between 1 and 6
            }

            return dice;
        }

        /// <summary>
        /// displays the roll results of dice
        /// </summary>
        /// <param name="dice">array of the dice in your roll results</param>
        /// <param name="diceCount">how many dice are included in your roll results</param>
        private static void DisplayTheRollResults(int[] dice, int diceCount)
        {
            for (int i = 0; i < diceCount; i++)
            {
                Console.WriteLine($"dice #{i + 1} - {dice[i]} ");
            }

        }

        /// <summary>
        /// displays the values of the saved dice
        /// </summary>
        /// <param name="savedDice">array of the saved dice</param>
        /// <param name="savedUserDiceCount">how many dice are in your saved dice</param>
        /// <param name="playerName">name of the player</param>
        private static void DisplayTheSavedDice(int[] savedDice, int savedUserDiceCount, string playerName)
        {
            if ((savedDice.Length) > 0)
            {
                Console.Write($"{playerName} - saved dice are {string.Join(", ", savedDice)}");
               
                /*
                for (int i = 0; i < savedDice.Length; i++)
                {
                    Console.Write($"{savedDice[i]}, ");
                }
                */

                Console.WriteLine();
                Console.WriteLine();
            }
        }

        /// <summary>
        /// calculate the score of a dice roll result array
        /// </summary>
        /// <param name="savedDice">array of dice roll results</param>
        /// <returns>finalScore of the dice roll</returns>
        private static int CalculateUserScore(int[] savedDice)
        {
            int[] score = new int[numberOfDice];
            int finalScore = 0;

            for (int i = 0; i < numberOfDice; i++)
            {
                for (int j = 0; j < savedDice.Length; j++)
                {
                    if (savedDice[j] == (i + 1))
                    {
                        score[i]++;
                    }
                }
            }

            for (int i = 0; i < numberOfDice; i++)
            {
                if (score[i] > finalScore)
                {
                    finalScore = score[i];
                }
            }

            return finalScore;
        }

        private static int[] SaveSelectedDice(int[] dice, int[] savedDice, string rollNumber)
        {
            Console.WriteLine($"Tell me which dice from your {rollNumber} roll to keep");

            int[] interimSavedDice = new int[numberOfDice];
            int interimSavedDiceCount = 0;

            for (int i = 0; i < dice.Length; i++)
            {
                Console.Write($"Keep dice # {i + 1} - value is {dice[i]}?  Yes/No -- ");
                if (Console.ReadLine().ToUpper() == "YES")
                {
                    interimSavedDice[interimSavedDiceCount] = dice[i];
                    interimSavedDiceCount++;
                }
            }

            //
            int[] newSavedDice = new int[savedDice.Length + interimSavedDiceCount];

            for (int i = 0; i < interimSavedDiceCount; i++)
            {
                newSavedDice[i] = interimSavedDice[i];
            }

            for (int i = 0; i < savedDice.Length; i++)
            {
                newSavedDice[i + interimSavedDiceCount] = savedDice[i];
            }

            return newSavedDice;
        }
    }
}

