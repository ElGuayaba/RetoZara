using System;
using System.Globalization;
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

		public DataTable ImportCSV(string csvPath)
		{
			NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;
			string[] tempStringA;
			DataTable dt = new DataTable();
			dt.Columns.AddRange(new DataColumn[3]
			{new DataColumn("Fecha", typeof(DateTime)),
			new DataColumn("Apertura", typeof(Decimal)),
			new DataColumn("Cierre",typeof(Decimal)) });

			string csvData = File.ReadAllText(csvPath);
			string[] rows = csvData.Split('\n');
 
			foreach (string row in rows)
			{
				if (!string.IsNullOrEmpty(row))
				{
					dt.Rows.Add();
					tempStringA = row.Split(';');
					dt.Rows[dt.Rows.Count - 1][0] = DateConverter(tempStringA[0]);
					dt.Rows[dt.Rows.Count - 1][1] = Decimal.Parse(tempStringA[1], nfi);
					dt.Rows[dt.Rows.Count - 1][2] = Decimal.Parse(tempStringA[2], nfi);
				}
			}
			return dt;
		}
		static void Main(string[] args)
		{
			FileManager fm = new FileManager();
			//Console.WriteLine(fm.DateConverter("28-dic-2017").ToString());
			DataTable dt = fm.ImportCSV("C:/Users/formacion/Desktop/Curso/Reto_Zara/stocks-ITX.csv");
			DataRow[] rows = dt.Select();
			Console.WriteLine(rows[0].Field<Decimal>(1));
			Console.ReadLine();
		}
	}
}
