using System.Text;

namespace InventarioAlmacen.Services;

/// <summary>
/// Genera reportes a partir de los datos del Almacén.
/// Depende de Almacen solo para leer: no guarda copias ni modifica nada.
/// </summary>
public class ReporteInventario
{
    private readonly Almacen _almacen;

    public ReporteInventario(Almacen almacen)
    {
        ArgumentNullException.ThrowIfNull(almacen);
        _almacen = almacen;
    }

    public string GenerarResumen()
    {
        var productos = _almacen.Productos;
        var sb = new StringBuilder();

        sb.AppendLine($"=== RESUMEN DE INVENTARIO - {_almacen.Nombre} ===");

        if (productos.Count == 0)
        {
            sb.AppendLine("No hay productos registrados.");
            return sb.ToString();
        }

        int totalUnidades = productos.Sum(p => p.Stock);
        decimal valorTotal = productos.Sum(p => p.ValorTotal);
        int agotados = productos.Count(p => p.EstaAgotado);
        var masValioso = productos.OrderByDescending(p => p.ValorTotal).First();

        sb.AppendLine($"Productos registrados : {productos.Count}");
        sb.AppendLine($"Unidades en stock     : {totalUnidades}");
        sb.AppendLine($"Valor total           : {valorTotal:N2}");
        sb.AppendLine($"Productos agotados    : {agotados}");
        sb.AppendLine($"Mayor valor en stock  : {masValioso.Nombre} ({masValioso.ValorTotal:N2})");
        sb.AppendLine();
        sb.AppendLine("Por categoría:");

        foreach (var categoria in _almacen.Categorias)
        {
            var deCategoria = productos.Where(p => p.Categoria.Id == categoria.Id).ToList();
            sb.AppendLine(
                $"  - {categoria.Nombre,-15} {deCategoria.Count,3} producto(s), " +
                $"{deCategoria.Sum(p => p.Stock),5} unidades, valor {deCategoria.Sum(p => p.ValorTotal):N2}");
        }

        return sb.ToString();
    }
}
