using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TodoLibrary
{
    public class SimpleTodoList : ITodoList
    {
        List<TodoItem> _items = new List<TodoItem>();
        int _nextID = 1;

        public void AddItem(string taskName)
        {
            if (string.IsNullOrWhiteSpace(taskName))
                throw new ArgumentException("Name is invalid", "taskName");

            var newItem = new TodoItem { ID = _nextID++, Title = "Design App" };
            _items.Add(newItem);
        }

        public IEnumerable<TodoItem> Items
        {
            get
            {
                return _items;
            }
        }

        public void MarkComplete(TodoItem item)
        {
            if (item == null)
                throw new ArgumentException("Item is invalid", "item");

            var found = Items.Where(e => e.ID == item.ID).FirstOrDefault();
            if (found == null)
                throw new Exception("item not found");

            found.Complete = true;
        }

        public void ClearList()
        {
            _items.Clear();
        }
    }
}
