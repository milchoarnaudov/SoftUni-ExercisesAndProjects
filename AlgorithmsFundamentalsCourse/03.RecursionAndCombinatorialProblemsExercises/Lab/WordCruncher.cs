namespace Lab
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class WordCruncher
    {
        private string targetWord;
        private Dictionary<int, List<string>> wordsByLength;
        private Dictionary<string, int> occurrences;
        private List<string> selectedWords;
        private HashSet<string> result;

        public void Start()
        {
            var words = Console.ReadLine().Split(", ");
            this.targetWord = Console.ReadLine();
            this.wordsByLength = new Dictionary<int, List<string>>();
            this.occurrences = new Dictionary<string, int>();
            this.selectedWords = new List<string>();
            this.result = new HashSet<string>();

            foreach (var word in words)
            {
                if (!targetWord.Contains(word))
                {
                    continue;
                }

                var length = word.Length;
                if (!this.wordsByLength.ContainsKey(length))
                {
                    this.wordsByLength.Add(length, new List<string>());
                }

                if (this.occurrences.ContainsKey(word))
                {
                    this.occurrences[word] += 1;
                }
                else
                {
                    this.occurrences.Add(word, 1);
                }

                wordsByLength[length].Add(word);
            }

            this.GenerateSolutions(targetWord.Length, "");
            Console.WriteLine(string.Join(Environment.NewLine, this.result));
        }

        private void GenerateSolutions(int length, string current)
        {
            if (length == 0)
            {
                if (current == targetWord)
                {
                    result.Add(string.Join(" ", this.selectedWords));
                }
                return;
            }

            foreach (var kvp in this.wordsByLength)
            {
                if (kvp.Key > length)
                {
                    continue;
                }

                foreach (var word in kvp.Value)
                {
                    if (occurrences[word] > 0)
                    {
                        current += word;

                        if (this.IsMatching(targetWord, current))
                        {
                            this.occurrences[word] -= 1;
                            this.selectedWords.Add(word);
                            this.GenerateSolutions(length - word.Length, current);
                            this.occurrences[word] += 1;
                            this.selectedWords.RemoveAt(selectedWords.Count - 1);
                        }

                        current = current.Remove(current.Length - word.Length, word.Length);
                    }
                }
            }
        }

        private bool IsMatching(string expected, string actual)
        {
            for (int i = 0; i < actual.Length; i++)
            {
                if (expected[i] != actual[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
