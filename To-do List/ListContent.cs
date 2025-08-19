using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace To_do_List
{
    public class ListContent
    {
        public string Description { get; set; }
        public bool IsCompleted { get; set; }

        public ListContent(string description)
        {
            Description = description;
            IsCompleted = false;
        }

        public override string ToString()
        {
            return Description;
        }
    }
}
