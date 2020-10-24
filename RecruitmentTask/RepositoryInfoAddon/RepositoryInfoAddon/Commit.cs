using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryInfoAddon
{
    public class Commit
    {
        public Author Author { get; set; }
        public string Message { get; set; }
        public DateTime DateTime { get; set; }
    }
}
