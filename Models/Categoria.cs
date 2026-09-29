namespace InventarioAlmacen.Models;

/// <summary>
/// Agrupa productos por tipo (ej. Electrónica, Papelería).
/// </summary>
public class Categoria
{
    public int Id { get; }
    public string Nombre { get; private set; }

    public Categoria(int id, string nombre)
    {
        if (id <= 0)
            throw new ArgumentException("El id de la categoría debe ser mayor que cero.");
        if (string.IsNullOrWhiteSpace(nombre))
            throw new ArgumentException("El nombre de la categoría no puede estar vacío.");

        Id = id;
        Nombre = nombre.Trim();
    }

    public override string ToString() => $"[{Id}] {Nombre}";
}
