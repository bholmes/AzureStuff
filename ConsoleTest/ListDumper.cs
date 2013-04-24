using System;
using System.Collections.Generic;
using System.Reflection;

namespace ConsoleTest
{
	public static class ListDumperExtensions
	{
		static public void Dump <TT>(this IEnumerable<TT> items)
		{
			ListDumper<TT> ld = new ListDumper<TT> ();
			ld.Dump (items);
		}
	}

	public class ListDumper<TT>
	{
		static List<PropertyInfo> _piList =  new List<PropertyInfo> ();
		int _numProperties = _piList.Count;
		
		static ListDumper ()
		{
			foreach (PropertyInfo pi in typeof(TT).GetProperties ())
			{
				_piList.Add (pi);
			}
		}
		
		public void Dump (IEnumerable<TT> items)
		{
			List<ItemDumper> itemDumpers = new List<ItemDumper> ();
			int [] fieldLengths = new int[_numProperties];
			
			foreach (TT item in items)
			{
				var newItem = new ItemDumper (_numProperties, _piList, item);
				itemDumpers.Add (newItem);
				for (int i=0; i< _numProperties; i++)
				{
					if (fieldLengths[i] < newItem.FieldLengths[i])
						fieldLengths[i] = newItem.FieldLengths[i];
				}
			}

			WriteDashes (fieldLengths);
			
			foreach (ItemDumper item in itemDumpers)
			{
				item.Dump (fieldLengths);
			}

			WriteDashes (fieldLengths);
			
			Console.WriteLine ();
		}

		private void WriteDashes (int [] fieldLengths)
		{
			foreach (int len in fieldLengths)
			{
				for (int i=0; i< len; i++)
					Console.Write ("-");
			}

			Console.WriteLine ();
		}
		
		private class ItemDumper 
		{
			string [] _fields;
			int [] _fieldLengths;
			
			public ItemDumper (int numProperties, List<PropertyInfo> piList, TT item)
			{
				_fields = new string[numProperties];
				_fieldLengths = new int[numProperties];
				int index = 0;
				
				foreach (PropertyInfo pi in _piList)
				{
					var str = string.Format ("{0}:{1}    ", pi.Name, pi.GetValue (item, null));
					_fields[index] = str;
					_fieldLengths[index] = str.Length;
					index++;
				}
			}
			
			public void Dump (int[] widths)
			{
				for (int i=0; i<_fields.Length; i++)
				{
					var format = string.Format ("{{0,-{0}}}", widths[i]);
					Console.Write (format, _fields[i]);
				}
				
				Console.WriteLine ();
			}
			
			public int [] FieldLengths {get {return _fieldLengths;}}
		}
	}
}

