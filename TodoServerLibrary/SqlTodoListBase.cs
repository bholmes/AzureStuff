using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TodoLibrary;

namespace TodoServerLibrary
{
    public partial class SqlTodoItem
    {
        public TodoItem ToTodoItem()
        {
            return new TodoItem
            {
                Id=this.id,
                Title = this.title,
                Complete = this.complete
            };
        }
    }

    public abstract class SqlTodoListBase : ITodoList, IDisposable
    {
        protected abstract TodoDataContext DataContext { get; }

        #region ITodoList Members

        public void AddItem(string taskName)
        {
            var newItem = new SqlTodoItem { title = taskName };
            var table = DataContext.GetTable<SqlTodoItem>();
            table.InsertOnSubmit(newItem);

            DataContext.SubmitChanges();
        }

        public IEnumerable<TodoItem> Items
        {
            get 
            {
                var table = DataContext.GetTable<SqlTodoItem>();
                foreach (SqlTodoItem sqlItem in table)
                {
                    yield return sqlItem.ToTodoItem();
                }
            }
        }

        public void MarkComplete(TodoItem item)
        {
            if (item == null)
                throw new ArgumentException("Item is invalid", "item");

            var table = DataContext.GetTable<SqlTodoItem>();

            var found = table.Where(e => e.id == item.Id).FirstOrDefault();
            if (found == null)
                throw new Exception("item not found");

            found.complete = true;
            DataContext.SubmitChanges();
        }

        public void ClearList()
        {
            var table = DataContext.GetTable<SqlTodoItem>();
            var query1 = table;

            table.DeleteAllOnSubmit(query1);

            DataContext.SubmitChanges();
        }

        #endregion

        #region IDisposable Members

        public abstract void Dispose();

        #endregion
    }
}
