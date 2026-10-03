using System;
using System.Collections.Generic;

class Producto
{
    public string Codigo, Nombre;
    public decimal Precio;
    public int Cantidad;
    public string Categoria;

    public Producto(string codigo, string nombre, decimal precio, int cantidad, string categoria)
    {
        Codigo = codigo; Nombre = nombre; Precio = precio; Cantidad = cantidad; Categoria = categoria;
    }

    public override string ToString() =>
        $"[{Codigo}] {Nombre} - ${Precio} - Cant: {Cantidad} - {Categoria}";
}

class Almacen
{
    List<Producto> productos = new List<Producto>();
    public List<string> Categorias = new List<string> { "Electronica", "Oficina", "Hogar" };

    public bool Registrar(Producto p)
    {
        if (BuscarPorCodigo(p.Codigo) != null) return false;
        productos.Add(p);
        return true;
    }

    public Producto BuscarPorCodigo(string codigo) => productos.Find(p => p.Codigo == codigo);

    public bool Eliminar(string codigo)
    {
        var p = BuscarPorCodigo(codigo);
        return p != null && productos.Remove(p);
    }

    public List<Producto> Listar() => productos;

    public List<Producto> PorCategoria(string categoria) =>
        productos.FindAll(p => p.Categoria.Equals(categoria, StringComparison.OrdinalIgnoreCase));

    public List<Producto> Agotados() => productos.FindAll(p => p.Cantidad == 0);

    public string Resumen()
    {
        int unidades = 0; decimal valor = 0;
        foreach (var p in productos) { unidades += p.Cantidad; valor += p.Precio * p.Cantidad; }
        return $"Productos: {productos.Count} | Unidades: {unidades} | Valor total: ${valor}";
    }
}
