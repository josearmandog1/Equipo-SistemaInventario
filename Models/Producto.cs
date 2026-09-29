namespace InventarioAlmacen.Models;

/// <summary>
/// Artículo del inventario. Mantiene su propio estado (precio, stock)
/// y protege sus reglas: el precio no es negativo y el stock nunca baja de cero.
/// </summary>
public class Producto
{
    public string Codigo { get; }
    public string Nombre { get; private set; }
    public decimal Precio { get; private set; }
    public int Stock { get; private set; }
    public Categoria Categoria { get; private set; }

    public bool EstaAgotado => Stock == 0;
    public decimal ValorTotal => Precio * Stock;

    public Producto(string codigo, string nombre, decimal precio, int stock, Categoria categoria)
    {
        if (string.IsNullOrWhiteSpace(codigo))
            throw new ArgumentException("El código del producto no puede estar vacío.");
        if (string.IsNullOrWhiteSpace(nombre))
            throw new ArgumentException("El nombre del producto no puede estar vacío.");
        if (precio < 0)
            throw new ArgumentException("El precio no puede ser negativo.");
        if (stock < 0)
            throw new ArgumentException("El stock inicial no puede ser negativo.");
        ArgumentNullException.ThrowIfNull(categoria);

        Codigo = codigo.Trim().ToUpperInvariant();
        Nombre = nombre.Trim();
        Precio = precio;
        Stock = stock;
        Categoria = categoria;
    }

    /// <summary>Suma (positivo) o resta (negativo) unidades al stock.</summary>
    public void AjustarStock(int cantidad)
    {
        if (Stock + cantidad < 0)
            throw new InvalidOperationException(
                $"Stock insuficiente: hay {Stock} unidades y se intentó restar {-cantidad}.");
        Stock += cantidad;
    }

    public void ActualizarPrecio(decimal nuevoPrecio)
    {
        if (nuevoPrecio < 0)
            throw new ArgumentException("El precio no puede ser negativo.");
        Precio = nuevoPrecio;
    }
}
