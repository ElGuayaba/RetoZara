using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Data;

namespace RetoZara
{
	public class FileManager
	{
		string[] calendar = {"ene", "feb", "mar" ,"abr", "may", "jun",
			"jul", "ago", "sep", "oct", "nov", "dic" };
		public DateTime DateConverter(string date)
		{
			var values = date.Split('-');
			int month = Array.IndexOf(calendar,values[1]);
			return new DateTime(Int32.Parse(values[2]), month + 1, Int32.Parse(values[0]));
		}

		public void ImportCSV(string csvPath)
		{
			string[] tempStringA;
			//var tempDate;
			//var tempShare;
			//Create a DataTable.  
			DataTable dt = new DataTable();
			dt.Columns.AddRange(new DataColumn[3]
			{new DataColumn("Fecha", typeof(DateTime)),
			new DataColumn("Apertura", typeof(string)),
			new DataColumn("Cierre",typeof(string)) });

			//Read the contents of CSV file.  
			string csvData = File.ReadAllText(csvPath);
			string[] rows = csvData.Split('\n');

			//Execute a loop over the rows.  
			foreach (string row in rows)
			{
				if (!string.IsNullOrEmpty(row))
				{
					dt.Rows.Add();
					tempStringA = row.Split(',');
					dt.Rows[dt.Rows.Count - 1][0] = DateConverter(tempStringA[0]);
					dt.Rows[dt.Rows.Count - 1][1] = tempStringA[1];
					dt.Rows[dt.Rows.Count - 1][2] = tempStringA[2];
				}
			}
		}
		static void Main(string[] args)
		{
			FileManager fm = new FileManager();
			Console.WriteLine(fm.DateConverter("28-dic-2017").ToString());
			Console.ReadLine();
		}
	}
}
