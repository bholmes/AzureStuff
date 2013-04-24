using System;
using System.Collections.Generic;
using System.Linq;
using TodoLibrary;
using TodoServerLibrary;

namespace ConsoleTest
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            ITodoList list = null;
            try
            {
                //list = new SimpleTodoList();
                list = new LocalSqlTodoListBase();
                list.ClearList();

                list.AddItem("Design App");
                list.AddItem("Implement App");
                list.AddItem("Test App");
                list.AddItem("Profit");

                list.Items.Dump();
                var query1 = from item in list.Items
                             where item.ID == 2 || item.ID == 4
                             select item;

                foreach (TodoItem item in query1)
                    list.MarkComplete(item);

                list.Items.Dump();
            }

            finally
            {
                if (list != null && list is IDisposable)
                {
                    var dispose = list as IDisposable;
                    dispose.Dispose();
                }
            }
        }
    }
}
