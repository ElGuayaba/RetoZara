using System;
using System.Data;

namespace RetoZara
{
	public class RetoZara
	{
		decimal acciones { get; set; }
		DataTable datos;
		
		/// <summary>
		/// Calcula la próxima fecha en la que el usuario recibirá un pago.
		/// </summary>
		/// <param name="dia">fecha del pago anterior</param>
		/// <returns>
		/// Fecha del siguiente pago del usuario en formato Datetime.
		/// </returns>
		public DateTime GetSiguientePaga(DateTime fechaPrev)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Retorna la primera fecha a evaluar
		/// </summary>
		/// <param name="dia">fecha del pago anterior</param>
		/// <returns>
		/// Fecha del siguiente pago del usuario en formato Datetime.
		/// </returns>
		public DateTime GetPrimerDia()
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Compra acciones al precio de apertura dado tomando en cuenta la
		/// comisión del eskrow. Lo acumula todo en la propiedad "acciones".
		/// </summary>
		/// <param name="apertura">Valor de las acciones en el momento en que se compran</param>
		/// <returns></returns>
		public void ComprarAcciones(decimal apertura)
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
		public DateTime CalcularValor(string path)
		{
			ImportarTabla("C:/Users/formacion/Desktop/Curso/Reto_Zara/stocks-ITX.csv");
			//select * from datos where "Fecha" == new DateTime(2001,05,23)
			//...
			throw new NotImplementedException();
		}


		public static void Main(String[] args)
		{
			//"C:/Users/formacion/Desktop/Curso/Reto_Zara/stocks-ITX.csv"
		}
	}
}
