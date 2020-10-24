using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryInfoAddon.UI
{
    public class CommitRow
    {

        [Display(Name = "Informacja")]//, Order = -9, Prompt = "Enter Last Name", Description = "Emp Last Name")]
        public string Message { get; set; }
        [Display(Name = "Data")]
        public string DateTime { get; set; }

        public override string ToString()
        {
            return $"Wiadomosc: {Message}";
        }
    }
}
