using System;
using System.Collections.Generic;

namespace UrlShortener.Services
{
    public class RandomUrlGenerator
    {
        private const int urlLength = 10;

        private List<char> characters = new List<char>()
        {'1', '2', '3', '4', '5', '6', '7', '8', '9', '0',
        'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n',
        'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', 'A', 'B',
        'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P',
        'Q', 'R', 'S',  'T', 'U', 'V', 'W', 'X', 'Y', 'Z', '-', '_'};

        public string Generate()
        {
            string url = "";
            Random rand = new Random();
    
            for (int i = 0; i < urlLength; i++)
            {
                var random = rand.Next(0, characters.Count);
                url += characters[random].ToString();
            }

            return url;
        }
    }
}
