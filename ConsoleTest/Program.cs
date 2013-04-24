using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleTest
{
	public class ToDoItem
	{
		public int ID {get;set;}
		public string Title {get;set;}
		public bool Complete {get;set;}
	}

	class MainClass
	{
		public static void Main (string[] args)
		{
			List<ToDoItem> items = new List<ToDoItem> ();
			items.Add (new ToDoItem {ID=1,Title="Design App"});
			items.Add (new ToDoItem {ID=2,Title="Implement App"});
			items.Add (new ToDoItem {ID=3,Title="Test App"});
			items.Add (new ToDoItem {ID=4,Title="Profit"});

			items.Dump ();
			items.Select (e=>new {Title = e.Title, ID=e.ID}).Dump ();
		}
	}
}
