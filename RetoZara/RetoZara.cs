using System;
using System.Linq;
using System.Data;

namespace RetoZara
{
	public class RetoZara
	{
		decimal acciones { get; set; }
		DataTable datos;
		bool esUltima = false;
		
		/// <summary>
		/// Calcula la próxima fecha en la que el usuario recibirá un pago.
		/// </summary>
		/// <param name="dia">fecha del pago anterior</param>
		/// <returns>
		/// Fecha del siguiente pago del usuario en formato Datetime.
		/// </returns>
		public DateTime SiguientePaga(DateTime fechaPrev)
		{
			if (fechaPrev.Month != 12)
				fechaPrev = new DateTime(fechaPrev.Year, fechaPrev.Month + 1, 22);
			else
				fechaPrev = new DateTime(fechaPrev.Year + 1, 1, 22);

			return UltimoJueves(fechaPrev);
		}

		public DateTime UltimoJueves(DateTime fecha)
		{
			DateTime temp = DateTime.Now;
			fecha = new DateTime(fecha.Year, fecha.Month, 22);
			while (fecha.Day <= 31 && fecha.Day > 21)
			{
				if (fecha.DayOfWeek == DayOfWeek.Thursday)
					temp = fecha;
				fecha = fecha.AddDays(1);
			}
			if (EsElDia(temp))
				esUltima = true;
			return temp;
		}



		/// <summary>
		/// Compra acciones al precio de apertura dado tomando en cuenta la
		/// comisión del eskrow. Lo acumula todo en la propiedad "acciones".
		/// </summary>
		/// <param name="apertura">Valor de las acciones en el momento en que se compran</param>
		/// <returns></returns>
		public void ComprarAcciones(DateTime diaDeCompra)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Vende acciones al precio de de cierre de la fecha dada.
		/// </summary>
		/// <param></param>
		/// <returns></returns>
		public decimal VenderAcciones()
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Retorna la siguiente fecha en la que hubo cotización en la bolsa
		/// </summary>
		/// <param name="paga">Día en que el usuario cobra su salario</param>
		/// <returns></returns>
		public DateTime EncontrarCotizacion(DateTime paga)
		{
			var result = datos.AsEnumerable().Where(rows => rows.Field<DateTime>(0).Equals(paga));
			while (!result.Any())
			{
				paga = paga.AddDays(1);
				result = datos.AsEnumerable().Where(rows => rows.Field<DateTime>(0).Equals(paga));
			}

			return result.First().Field<DateTime>(0);
		}

		/// <summary>
		/// Verifica si es el día especificado para vender las acciones.
		/// </summary>
		/// <param name="dia">Día en que el usuario cobra su salario</param>
		/// <returns></returns>
		public bool EsElDia(DateTime dia)
		{
			int result = dia.CompareTo(new DateTime(2017, 12, 28));
			return result == 0;
		}

		/// <summary>
		/// Utiliza el filemanager para cargar los datos.
		/// </summary>
		/// <param name="path">Ruta del archivo con los datos</param>
		/// <returns></returns>
		public void ImportarTabla(string path)
		{
			datos = FileManager.ImportCSV(path);
		}

		/// <summary>
		/// Verifica si es el día especificado para vender las acciones.
		/// </summary>
		/// <param name="dia">Día en que el usuario cobra su salario</param>
		/// <returns></returns>
		public decimal CalcularValor(string path)
		{
			ImportarTabla("C:/Users/formacion/Desktop/Curso/Reto_Zara/stocks-ITX.csv");
			DateTime curDate = new DateTime(2001,5,23);
			curDate = UltimoJueves(curDate);
			curDate = EncontrarCotizacion(curDate);
			ComprarAcciones(curDate);
			EsElDia(curDate);

			while (!esUltima)
			{
				curDate = SiguientePaga(curDate);
				curDate = EncontrarCotizacion(curDate);
				ComprarAcciones(curDate);
				EsElDia(curDate);
			}
			//return
			VenderAcciones();
			throw new NotImplementedException();
		}


		public static void Main(String[] args)
		{
			DateTime fecha = new DateTime(2001, 5, 23);
			RetoZara rz = new RetoZara();
			rz.ImportarTabla("C:/Users/formacion/Desktop/Curso/Reto_Zara/stocks-ITX.csv");
			DateTime salida = rz.UltimoJueves(fecha);
			salida = rz.EncontrarCotizacion(salida);
			Console.WriteLine(salida);
			for (int i = 0; i < 5; i++)
			{
				salida = rz.SiguientePaga(salida);
				salida = rz.EncontrarCotizacion(salida);
				Console.WriteLine(salida);
				//fecha = fecha.AddMonths(1);
			}
			Console.ReadLine();
		}
	}
}
