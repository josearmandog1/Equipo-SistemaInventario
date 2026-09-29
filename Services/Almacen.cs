using InventarioAlmacen.Models;

namespace InventarioAlmacen.Services;

/// <summary>
/// Administra el inventario. Es el dueño de la colección principal
/// (List&lt;Producto&gt;) y de la lista de categorías.
/// </summary>
public class Almacen
{
    private readonly List<Producto> _productos = new();
    private readonly List<Categoria> _categorias = new();
    private int _siguienteIdCategoria = 1;

    public string Nombre { get; }

    // Solo lectura hacia afuera: nadie puede modificar la lista sin pasar por Almacen.
    public IReadOnlyList<Producto> Productos => _productos;
    public IReadOnlyList<Categoria> Categorias => _categorias;

    public Almacen(string nombre)
    {
        if (string.IsNullOrWhiteSpace(nombre))
            throw new ArgumentException("El nombre del almacén no puede estar vacío.");
        Nombre = nombre.Trim();
    }

    // ---------- Categorías ----------

    public Categoria RegistrarCategoria(string nombre)
    {
        if (string.IsNullOrWhiteSpace(nombre))
            throw new ArgumentException("El nombre de la categoría no puede estar vacío.");

        bool existe = _categorias.Any(c =>
            c.Nombre.Equals(nombre.Trim(), StringComparison.OrdinalIgnoreCase));
        if (existe)
            throw new InvalidOperationException($"Ya existe la categoría '{nombre.Trim()}'.");

        var categoria = new Categoria(_siguienteIdCategoria++, nombre);
        _categorias.Add(categoria);
        return categoria;
    }

    public Categoria? BuscarCategoria(int id) => _categorias.FirstOrDefault(c => c.Id == id);

    // ---------- Productos ----------

    public Producto RegistrarProducto(string codigo, string nombre, decimal precio, int stock, int idCategoria)
    {
        var categoria = BuscarCategoria(idCategoria)
            ?? throw new InvalidOperationException($"No existe la categoría con id {idCategoria}.");

        if (!string.IsNullOrWhiteSpace(codigo) && BuscarPorCodigo(codigo) != null)
            throw new InvalidOperationException($"Ya existe un producto con el código '{codigo.Trim().ToUpperInvariant()}'.");

        var producto = new Producto(codigo, nombre, precio, stock, categoria);
        _productos.Add(producto);
        return producto;
    }

    public Producto? BuscarPorCodigo(string codigo)
    {
        if (string.IsNullOrWhiteSpace(codigo)) return null;
        string buscado = codigo.Trim();
        return _productos.FirstOrDefault(p =>
            p.Codigo.Equals(buscado, StringComparison.OrdinalIgnoreCase));
    }

    public List<Producto> BuscarPorNombre(string texto)
    {
        if (string.IsNullOrWhiteSpace(texto)) return new List<Producto>();
        return _productos
            .Where(p => p.Nombre.Contains(texto.Trim(), StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    /// <returns>true si se eliminó; false si el código no existe.</returns>
    public bool EliminarProducto(string codigo)
    {
        var producto = BuscarPorCodigo(codigo);
        if (producto == null) return false;
        return _productos.Remove(producto);
    }

    public bool ModificarStock(string codigo, int cantidad)
    {
        var producto = BuscarPorCodigo(codigo);
        if (producto == null) return false;
        producto.AjustarStock(cantidad);
        return true;
    }

    public bool ModificarPrecio(string codigo, decimal nuevoPrecio)
    {
        var producto = BuscarPorCodigo(codigo);
        if (producto == null) return false;
        producto.ActualizarPrecio(nuevoPrecio);
        return true;
    }

    // ---------- Consultas ----------

    public List<Producto> ObtenerPorCategoria(int idCategoria) =>
        _productos.Where(p => p.Categoria.Id == idCategoria).ToList();

    public List<Producto> ObtenerAgotados() =>
        _productos.Where(p => p.EstaAgotado).ToList();
}
