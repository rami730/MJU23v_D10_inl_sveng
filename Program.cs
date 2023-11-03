﻿namespace MJU23v_D10_inl_sveng
{
    internal class Program
    {
        static List<SweEngGloss> dictionary;
        class SweEngGloss
        {
            public string word_swe, word_eng;
            public SweEngGloss(string word_swe, string word_eng)
            {
                this.word_swe = word_swe; this.word_eng = word_eng;
            }
            public SweEngGloss(string line)
            {
                string[] words = line.Split('|');
                this.word_swe = words[0]; this.word_eng = words[1];
            }
        }
        static void Main(string[] args)
        {
            string defaultFile = "..\\..\\..\\dict\\sweeng.lis";
            string command;
            Console.WriteLine("Welcome to the dictionary app!");
            Console.WriteLine("Type 'h' for help");
            do
            {
                Console.Write("> ");
                string[] argument = Console.ReadLine().Split();
                command = argument[0];
                if (command == "quit")
                {
                    Console.WriteLine("Goodbye!");
                }
                else if (command == "h")
                {
                    HelpPrompt();
                }
                else if (command == "load")
                {
                    if(argument.Length == 2)
                    {
                        using (StreamReader sr = new StreamReader(argument[1])) // FIXME: 'System.IO.FileNotFoundException' when inputting non existing file name
                        {
                            dictionary = new List<SweEngGloss>(); // Empty it!
                            string line = sr.ReadLine();
                            while (line != null)
                            {
                                SweEngGloss gloss = new SweEngGloss(line);
                                dictionary.Add(gloss);
                                line = sr.ReadLine();
                            }
                        }
                    }
                    else if(argument.Length == 1)
                    {
                        using (StreamReader sr = new StreamReader(defaultFile))
                        {
                            dictionary = new List<SweEngGloss>(); // Empty it!
                            string line = sr.ReadLine();
                            while (line != null)
                            {
                                SweEngGloss gloss = new SweEngGloss(line);
                                dictionary.Add(gloss);
                                line = sr.ReadLine();
                            }
                        }
                    }
                }
                else if (command == "list")
                {
                    foreach(SweEngGloss gloss in dictionary) //FIXME: 'System.NullReferenceException' if nothing loaded
                    {
                        Console.WriteLine($"{gloss.word_swe,-10}  - {gloss.word_eng,-10}");
                    }
                }
                else if (command == "new")
                {
                    if (argument.Length == 3)
                    {
                        dictionary.Add(new SweEngGloss(argument[1], argument[2])); //FIXME: 'System.NullReferenceException' if nothing loaded
                    }
                    else if(argument.Length == 1)
                    {
                        Console.WriteLine("Write word in Swedish: ");
                        string swedishWordInput = Console.ReadLine();
                        Console.Write("Write word in English: ");
                        string englishWordInput = Console.ReadLine();
                        dictionary.Add(new SweEngGloss(swedishWordInput, englishWordInput)); //FIXME: 'System.NullReferenceException' if nothing loaded
                    }
                }
                else if (command == "delete")
                {
                    if (argument.Length == 3)
                    {
                        int index = -1;
                        for (int i = 0; i < dictionary.Count; i++) { //FIXME: 'System.NullReferenceException' if nothing loaded
                            SweEngGloss gloss = dictionary[i];
                            if (gloss.word_swe == argument[1] && gloss.word_eng == argument[2])
                                index = i;
                        }
                        dictionary.RemoveAt(index);
                    }
                    else if (argument.Length == 1)
                    {
                        Console.WriteLine("Write word in Swedish: ");
                        string swedishWordInput = Console.ReadLine();
                        Console.Write("Write word in English: ");
                        string englishWordInput = Console.ReadLine();
                        int index = -1;
                        for (int i = 0; i < dictionary.Count; i++) //FIXME: 'System.NullReferenceException' if nothing loaded
                        {
                            SweEngGloss gloss = dictionary[i];
                            if (gloss.word_swe == swedishWordInput && gloss.word_eng == englishWordInput)
                                index = i;
                        }
                        dictionary.RemoveAt(index); //FIXME: 'System.ArgumentOutOfRangeException' when trying to delete non exsisting word
                    }
                }
                else if (command == "translate")
                {
                    if (argument.Length == 2)
                    {
                        foreach(SweEngGloss gloss in dictionary) //FIXME: 'System.NullReferenceException' if nothing loaded
                        {
                            if (gloss.word_swe == argument[1])
                                Console.WriteLine($"English for {gloss.word_swe} is {gloss.word_eng}");
                            if (gloss.word_eng == argument[1])
                                Console.WriteLine($"Swedish for {gloss.word_eng} is {gloss.word_swe}");
                        }
                    }
                    else if (argument.Length == 1)
                    {
                        Console.WriteLine("Write word to be translated: ");
                        string wordToTranslate = Console.ReadLine();
                        foreach (SweEngGloss gloss in dictionary) //FIXME: 'System.NullReferenceException' if nothing loaded
                        {
                            if (gloss.word_swe == wordToTranslate)
                                Console.WriteLine($"English for {gloss.word_swe} is {gloss.word_eng}");
                            if (gloss.word_eng == wordToTranslate)
                                Console.WriteLine($"Swedish for {gloss.word_eng} is {gloss.word_swe}");
                        }
                    }
                }
                else
                {
                    Console.WriteLine($"Unknown command: '{command}'");
                }
            }
            while (command != "quit");
        }

        private static void HelpPrompt()
        {
            Console.WriteLine("Here are the available commands:");
            Console.WriteLine(" load                    - loads the default dictionary");
            Console.WriteLine(" load 'filename'         - loads a dictionary with a filename");
            Console.WriteLine(" list                    - lists all the words in the current dictionary");
            Console.WriteLine(" new                     - allows you to input a swedish and an english word to the dictionary");
            Console.WriteLine(" new 'word1' 'word2'     - adds a swedish and an english word to the dictionary");
            Console.WriteLine(" delete                  - allows you to input a swedish and english word to delete");
            Console.WriteLine(" delete 'word1' word2    - deletes a swedish and an english from the dictionary");
            Console.WriteLine(" translate               - allows you to input a word to translate");
            Console.WriteLine(" translate 'word'        - translates a word to swedish or english");
        }
    }
}