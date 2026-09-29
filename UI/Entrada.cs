using System.Globalization;

namespace InventarioAlmacen.UI;

/// <summary>
/// Lectura segura desde consola. Repite la pregunta hasta recibir un valor válido,
/// así una entrada incorrecta nunca termina la aplicación.
/// </summary>
public static class Entrada
{
    public static string LeerTexto(string mensaje)
    {
        while (true)
        {
            Console.Write(mensaje);
            string? texto = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(texto)) return texto.Trim();
            Console.WriteLine("  ! El valor no puede estar vacío.");
        }
    }

    public static int LeerEntero(string mensaje, int minimo = int.MinValue, int maximo = int.MaxValue)
    {
        while (true)
        {
            Console.Write(mensaje);
            if (int.TryParse(Console.ReadLine(), out int valor) && valor >= minimo && valor <= maximo)
                return valor;
            Console.WriteLine("  ! Ingrese un número entero válido" +
                (minimo != int.MinValue || maximo != int.MaxValue ? $" entre {minimo} y {maximo}." : "."));
        }
    }

    public static decimal LeerDecimal(string mensaje, decimal minimo = 0)
    {
        while (true)
        {
            Console.Write(mensaje);
            string? texto = Console.ReadLine()?.Trim();

            // Acepta "12.50" y "12,50" sin importar la configuración regional.
            bool ok = decimal.TryParse(texto, NumberStyles.Number, CultureInfo.CurrentCulture, out decimal valor)
                   || decimal.TryParse(texto, NumberStyles.Number, CultureInfo.InvariantCulture, out valor);

            if (ok && valor >= minimo) return valor;
            Console.WriteLine($"  ! Ingrese un número válido mayor o igual a {minimo}.");
        }
    }

    public static bool Confirmar(string mensaje)
    {
        while (true)
        {
            Console.Write($"{mensaje} (s/n): ");
            string? r = Console.ReadLine()?.Trim().ToLowerInvariant();
            if (r == "s" || r == "si" || r == "sí") return true;
            if (r == "n" || r == "no") return false;
            Console.WriteLine("  ! Responda 's' o 'n'.");
        }
    }

    public static void Pausa()
    {
        Console.Write("\nPresione Enter para continuar...");
        Console.ReadLine();
    }
}
