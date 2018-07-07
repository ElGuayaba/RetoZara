using System;
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
		public DateTime GetSiguientePaga(DateTime fechaPrev)
		{
			DateTime temp = DateTime.Now;
			fechaPrev = new DateTime(fechaPrev.Year, fechaPrev.Month + 1, 21);
			while (fechaPrev.Day <= 31 && fechaPrev.Day >= 21)
			{
				//Console.WriteLine("yes");
				if (fechaPrev.DayOfWeek == DayOfWeek.Thursday)
				temp = fechaPrev;
				fechaPrev = fechaPrev.AddDays(1);
			}
			return temp;

			//throw new NotImplementedException();
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
			throw new NotImplementedException();
		}

		/// <summary>
		/// Verifica si es el día especificado para vender las acciones.
		/// </summary>
		/// <param name="dia">Día en que el usuario cobra su salario</param>
		/// <returns></returns>
		public DateTime EsElDia(DateTime dia)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Toma los valores del documento entregado y los asocia al parámetro "datos".
		/// </summary>
		/// <param name="dia">Día en que el usuario cobra su salario</param>
		/// <returns></returns>
		public void ImportarTabla(string path)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Verifica si es el día especificado para vender las acciones.
		/// </summary>
		/// <param name="dia">Día en que el usuario cobra su salario</param>
		/// <returns></returns>
		public decimal CalcularValor(string path)
		{
			decimal resultado;
			ImportarTabla("C:/Users/formacion/Desktop/Curso/Reto_Zara/stocks-ITX.csv");
			DateTime curDate = new DateTime(2001,5,23);
			while (!esUltima)
			{
				curDate = GetSiguientePaga(curDate);
				curDate = EncontrarCotizacion(curDate);
				ComprarAcciones(curDate);
				EsElDia(curDate);
			}
			//return
			resultado = VenderAcciones();
			throw new NotImplementedException();
		}


		public static void Main(String[] args)
		{
			//"C:/Users/formacion/Desktop/Curso/Reto_Zara/stocks-ITX.csv"
			DateTime fecha = new DateTime(2001, 5, 23);
			RetoZara rz = new RetoZara();
			DateTime salida = rz.GetSiguientePaga(fecha);
			Console.WriteLine(salida);
			Console.ReadLine();
		}
	}
}
