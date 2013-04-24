using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TodoLibrary
{
    public interface ITodoList
    {
        void AddItem(string taskName);
        IEnumerable<TodoItem> Items { get; }
        void MarkComplete(TodoItem item);
        void ClearList();
    }
}
