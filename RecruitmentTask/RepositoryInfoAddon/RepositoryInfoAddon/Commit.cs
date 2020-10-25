using System;

namespace RepositoryInfoAddon
{
    public class Commit
    {
        public Author Author { get; set; }
        public string Message { get; set; }
        public DateTime DateTime { get; set; }
    }
}
