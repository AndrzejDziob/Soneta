using System;

namespace RepositoryInfoAddon
{
    public class Author
    {
        public string Name { get; set; }
        public string Email { get; set; }

        public static string ExtractName(string input)
        {
            string name = string.Empty;
            string[] inputParts = input.Split('<');
            if (inputParts.Length > 0)
                name = inputParts[0].Remove(inputParts[0].Length - 1);
            return name;
        }

        public static string ExtractEmail(string input)
        {
            string email = string.Empty;
            var inputParts = input.Split('<');
            if(inputParts.Length == 2)
                email = inputParts[1].Remove(inputParts[1].Length - 1);
            return email;
        }
    }
}
