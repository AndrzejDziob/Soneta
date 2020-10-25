using System;

namespace RepositoryInfoAddon
{
    public class Author
    {
        public string Name { get; set; }
        public string Email { get; set; }

        public static string ExtractName(string input)
        {
            string[] inputParts = input.Split('<');
            string name = inputParts[0].Remove(inputParts[0].Length - 1);
            return name;
        }

        public static string ExtractEmail(string input)
        {
            var inputParts = input.Split('<');
            string email = inputParts[1].Remove(inputParts[1].Length - 1);
            return email;
        }
    }
}
