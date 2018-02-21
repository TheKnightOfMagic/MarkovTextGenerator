using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkovTextGenerator
{
    class Chain
    {
        public Dictionary<String, List<Word>> words;
        private Dictionary<String, int> sums;
        public List<string> start= new List<string>();
        private Random rand;

        public Chain ()
        {
            words = new Dictionary<String, List<Word>>();
            sums = new Dictionary<string, int>();
            rand = new Random(System.Environment.TickCount);
        }

        // This may not be the best approach.. better may be to actually store
        // a separate list of actual sentence starting words and randomly choose from that
        public String GetRandomStartingWord ()
        {
            return start[rand.Next() % start.Count];
            
        }

        // Adds a sentence to the chain
        // You can use the empty string to indicate the sentence will end
        //
        // For example, if sentence is "The house is on fire" you would do the following:
        //  AddPair("The", "house")
        //  AddPair("house", "is")
        //  AddPair("is", "on")
        //  AddPair("on", "fire")
        //  AddPair("fire", "")

        public void AddString (String sentence)
        {
            // TODO: Break sentence up into word pairs
            // TODO: Add each word pair to the chain
            sentence += " ";
            List<string> list = sentence.Split(' ').ToList();
            for (int i =0; i< list.Count-1; i++)
            {
                AddPair(list[i], list[i+1]);
            }
        }

        // Adds a pair of words to the chain that will appear in order
        public void AddPair(String word, String word2)
        {
            if (!words.ContainsKey(word))
            {
                sums.Add(word, 1);
                words.Add(word, new List<Word>());
                words[word].Add(new Word(word2));
            }
            else
            {
                bool found = false;
                foreach (Word s in words[word])
                {
                    if (s.ToString() == word2)
                    {
                        found = true;
                        s.Count++;
                        sums[word]++;
                    }
                }

                if (!found)
                {
                    words[word].Add(new Word(word2));
                    sums[word]++;
                }
            }
        }

        // Given a word, randomly chooses the next word
        public String GetNextWord (String word)
        {
            //words[word].Probability

            //http://www.vcskicks.com/random-element.php
            if (words.ContainsKey(word))
            {
                double choice = rand.NextDouble();

                //Console.WriteLine("I picked the number " + choice);
                double total = 0;

                foreach (Word s in words[word])
                {
                    total += s.Probability;

                    if (choice<total)
                    {
                        return s.ToString();
                    }
                }
                Console.WriteLine("Well it failed");

            }

            return word;
        }

        public void UpdateProbabilities ()
        {
            string upper = "ASDFGHJKLQWERTYUIOPZXCVBNM";
            foreach (String word in words.Keys)
            {
                double sum = 0;  // Total sum of all the occurences of each followup word

                if (word != "") {
                    if (upper.Contains(word[0]))
                    {
                        start.Add(word);
                    }
                }

                // Update the probabilities
                foreach (Word s in words[word])
                {
                    s.Probability = (double)s.Count / sums[word];
                }

            }
        }
    }
}
