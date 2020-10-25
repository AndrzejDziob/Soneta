using Soneta.Types;
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

        [Display(Name = "Informacja")]
        public string Message { get; set; }

        [Display(Name = "Data")]
        public string DateTime { get; set; }
    }
}
