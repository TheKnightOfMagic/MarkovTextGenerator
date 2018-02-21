using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkovTextGenerator
{
    //
    class Program
    {
        static void Main(string[] args)
        {
            Chain chain = new Chain();

            Console.WriteLine("Welcome to Marky Markov's Random Text Generator!");

            Console.WriteLine("Enter some text I can learn from (enter single ! to finish): ");
            bool going = true;
            while (going == true)
            {

                int counter = 0;
                string line;

                // Read the file and display it line by line.  
                System.IO.StreamReader file = new System.IO.StreamReader("TextFile1.txt");
                while ((line = file.ReadLine()) != null)
                {
                    //System.Console.WriteLine(line);






                    if (line == "!")
                        going = false;

                    chain.AddString(line);  // Let the chain process this string
                    counter++;
                }
                file.Close();
            }

            // Now let's update all the probabilities with the new data
            chain.UpdateProbabilities();

            // Okay now for the fun part

            String word = chain.GetRandomStartingWord();
            while (true)
            {


                //Console.WriteLine("Done learning!  Now give me a word and I'll tell you what comes next.");
                //Console.Write("> ");

                
                String nextWord = chain.GetNextWord(word);
                   Console.Write(" "+ nextWord);
                if (nextWord.Contains(".") || nextWord.Contains("!") || nextWord.Contains("?"))
                {
                    Console.WriteLine("\n");
                    word = chain.GetRandomStartingWord();
                }
                else
                {
                    word = nextWord;
                }
                if (word == "peckish")
                {
                    break;
                }
               
            }
        }
    }
}
