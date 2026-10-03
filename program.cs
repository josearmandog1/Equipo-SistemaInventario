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

class Program
{
    static Almacen almacen = new Almacen();
 
    static void Main()
    {
        string opcion;
        do
        {
            Console.WriteLine("\n1.Registrar \n2.Buscar \n3.Eliminar \n4.Listar \n5.PorCategoria \n6.Agotados \n7.Resumen \n0.Salir");
            Console.Write("Opcion: ");
            opcion = Console.ReadLine();
 
            switch (opcion)
            {
                case "1": Registrar(); break;
                case "2": Buscar(); break;
                case "3": Eliminar(); break;
                case "4": Listar(almacen.Listar()); break;
                case "5": PorCategoria(); break;
                case "6": Listar(almacen.Agotados()); break;
                case "7": Console.WriteLine(almacen.Resumen()); break;
            }
        } while (opcion != "0");
    }
        static void Registrar()
    {
        int cantidad;
        bool cantidadValida;
 
        Console.Write("Codigo: "); string codigo = Console.ReadLine();
        Console.Write("Nombre: "); string nombre = Console.ReadLine();
        Console.Write("Precio: "); decimal.TryParse(Console.ReadLine(), out decimal precio);
        do
        {
            Console.Write("Cantidad: ");
            cantidadValida = int.TryParse(Console.ReadLine(), out cantidad) && cantidad >= 0;
            if (!cantidadValida)
                Console.WriteLine("La cantidad debe ser un número entero mayor o igual a cero.");
        } while (!cantidadValida);
 
        Console.WriteLine("Categorias: " + string.Join(", ", almacen.Categorias));
        Console.Write("Categoria: "); string categoria = Console.ReadLine();
 
        if (almacen.Registrar(new Producto(codigo, nombre, precio, cantidad, categoria)))
            Console.WriteLine("Producto registrado.");
        else
            Console.WriteLine("Ya existe un producto con ese codigo.");
    }
     static void Buscar()
    {
        Console.Write("Codigo: ");
        var p = almacen.BuscarPorCodigo(Console.ReadLine());
        Console.WriteLine(p == null ? "No encontrado." : p.ToString());
    }
 
    static void Eliminar()
    {
        Console.Write("Codigo: ");
        Console.WriteLine(almacen.Eliminar(Console.ReadLine()) ? "Eliminado." : "No encontrado.");
    }
}

