using System.Collections.Generic;

namespace eTickets.core.DataTable
{
	public class DataTable
	{
		public class Response<T>
		{
			public int Draw { get; set; }
			public long RecordsTotal { get; set; }
			public int RecordsFiltered { get; set; }
			public IEnumerable<T> Data { get; set; }
			//public T Data { get; set; }
			public string Error { get; set; }
		}

		public class Request
		{
			public int Draw { get; set; }
			public List<Column> Columns { get; set; }
			public List<Order> Order { get; set; }
			public int Start { get; set; }
			public int Length { get; set; }
			public Search Search { get; set; }
		}
		public class Column
		{
			public string Data { get; set; }
			public string Name { get; set; }
			public bool Searchable { get; set; }
			public bool Orderable { get; set; }
			public Search Search { get; set; }
		}
		public class Search
		{
			public string Value { get; set; }
			public bool Regex { get; set; }
		}
		public class Order
		{
			public int Column { get; set; }
			public string Dir { get; set; }
		}
	}
}
