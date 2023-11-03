namespace MJU23v_D10_inl_sveng
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
            string defaultPath = "..\\..\\..\\dict\\";
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
                        LoadDictionary(defaultPath + argument[1]);
                    }
                    else if(argument.Length == 1)
                    {
                        LoadDictionary(defaultFile);
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
                        string swedishWordInput, englishWordInput;
                        SwedishAndEnglishInput(out swedishWordInput, out englishWordInput);
                        dictionary.Add(new SweEngGloss(swedishWordInput, englishWordInput)); //FIXME: 'System.NullReferenceException' if nothing loaded
                    }
                }
                else if (command == "delete")
                {
                    if (argument.Length == 3)
                    {
                        DeleteWord(argument[1], argument[2]);
                    }
                    else if (argument.Length == 1)
                    {
                        string swedishWordInput, englishWordInput;
                        SwedishAndEnglishInput(out swedishWordInput, out englishWordInput);
                        DeleteWord(swedishWordInput, englishWordInput);
                    }
                }
                else if (command == "translate")
                {
                    if (argument.Length == 2)
                    {
                        TranslateWord(argument[1]);
                    }
                    else if (argument.Length == 1)
                    {
                        Console.WriteLine("Write word to be translated: ");
                        string wordToTranslate = Console.ReadLine();
                        TranslateWord(wordToTranslate);
                    }
                }
                else
                {
                    Console.WriteLine($"Unknown command: '{command}'");
                }
            }
            while (command != "quit");
        }

        private static void DeleteWord(string swedishWordInput, string englishWordInput)
        {
            int index = -1;
            for (int i = 0; i < dictionary.Count; i++) //FIXME: 'System.NullReferenceException' if nothing loaded
            {
                SweEngGloss gloss = dictionary[i];
                if (gloss.word_swe == swedishWordInput && gloss.word_eng == englishWordInput)
                    index = i;
            }
            dictionary.RemoveAt(index); //FIXME: 'System.ArgumentOutOfRangeException' when trying to delete non exsisting word
        }

        private static void TranslateWord(string argument)
        {
            foreach (SweEngGloss gloss in dictionary) //FIXME: 'System.NullReferenceException' if nothing loaded
            {
                if (gloss.word_swe == argument)
                    Console.WriteLine($"English for {gloss.word_swe} is {gloss.word_eng}");
                if (gloss.word_eng == argument)
                    Console.WriteLine($"Swedish for {gloss.word_eng} is {gloss.word_swe}");
            }
        }

        private static void SwedishAndEnglishInput(out string swedishWordInput, out string englishWordInput)
        {
            Console.WriteLine("Write word in Swedish: ");
            swedishWordInput = Console.ReadLine();
            Console.WriteLine("Write word in English: ");
            englishWordInput = Console.ReadLine();
        }

        private static void LoadDictionary(string argument)
        {
            using (StreamReader sr = new StreamReader(argument)) // FIXME: 'System.IO.FileNotFoundException' when inputting non existing file name
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