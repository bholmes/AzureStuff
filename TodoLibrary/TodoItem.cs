using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TodoLibrary
{
    public class TodoItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool Complete { get; set; }
    }
}
