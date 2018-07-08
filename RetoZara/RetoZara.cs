using System;
using System.Linq;
using System.Data;

namespace RetoZara
{
	public class RetoZara
	{
		decimal acciones { get; set; }
		decimal compra = 49;
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
			fechaPrev = fechaPrev.AddMonths(1);
			return UltimoJueves(fechaPrev);
		}

		/// <summary>
		/// Calcula el último jueves del mes.
		/// </summary>
		/// <param name="fecha">Fecha de partida.</param>
		/// <returns></returns>
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
		/// <param name="cotizacion">La Fila de la tabla que contiene el valor de la cotizacion</param>
		/// <returns></returns>
		public void ComprarAcciones(DataRow cotizacion)
		{
			decimal apertura = cotizacion.Field<decimal>(2);
			decimal resultado = Decimal.Round(compra / apertura, 3);
			acciones = acciones + resultado;
		}

		/// <summary>
		/// Vende acciones al precio de de cierre de la fecha dada.
		/// </summary>
		/// <param></param>
		/// <returns></returns>
		public decimal VenderAcciones(DataRow cotizacion)
		{
			decimal cierre = cotizacion.Field<decimal>(1);
			return Decimal.Round(acciones * cierre, 3);
		}

		/// <summary>
		/// Retorna la siguiente fecha en la que hubo cotización en la bolsa
		/// </summary>
		/// <param name="paga">Día en que el usuario cobra su salario</param>
		/// <returns></returns>
		public DataRow EncontrarCotizacion(DateTime paga)
		{
			var result = datos.AsEnumerable().Where(rows => rows.Field<DateTime>(0).Equals(paga));
			while (!result.Any())
			{
				paga = paga.AddDays(1);
				result = datos.AsEnumerable().Where(rows => rows.Field<DateTime>(0).Equals(paga));
			}

			return result.First();
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
			ImportarTabla(path);
			DateTime curDate = new DateTime(2001,5,23);
			DataRow cotizacion;

			curDate = UltimoJueves(curDate);
			cotizacion = EncontrarCotizacion(curDate);
			curDate = cotizacion.Field<DateTime>(0);
			ComprarAcciones(cotizacion);
			EsElDia(curDate);

			while (!EsElDia(curDate))
			{
				curDate = SiguientePaga(curDate);
				cotizacion = EncontrarCotizacion(curDate);
				ComprarAcciones(cotizacion);
			}
			return VenderAcciones(cotizacion);
			//throw new NotImplementedException();
		}


		public static void Main(String[] args)
		{
			DateTime fecha = new DateTime(2001, 5, 23);
			RetoZara rz = new RetoZara();
			decimal resultado = rz.CalcularValor("C:/Users/formacion/Desktop/Curso/Reto_Zara/stocks-ITX.csv");
			Console.WriteLine(resultado);
			Console.ReadLine();
		}
	}
}
