using InventarioAlmacen.Models;
using InventarioAlmacen.Services;

namespace InventarioAlmacen.UI;

/// <summary>
/// Interfaz de consola. Solo pide datos y muestra resultados;
/// las reglas del negocio las aplican Almacen y Producto.
/// </summary>
public class MenuInventario
{
    private readonly Almacen _almacen;
    private readonly ReporteInventario _reporte;

    public MenuInventario(Almacen almacen, ReporteInventario reporte)
    {
        _almacen = almacen;
        _reporte = reporte;
    }

    public void Ejecutar()
    {
        bool salir = false;

        while (!salir)
        {
            MostrarMenu();
            int opcion = Entrada.LeerEntero("Seleccione una opción: ", 0, 13);
            Console.WriteLine();

            try
            {
                switch (opcion)
                {
                    case 1: RegistrarCategoria(); break;
                    case 2: RegistrarProducto(); break;
                    case 3: BuscarPorCodigo(); break;
                    case 4: BuscarPorNombre(); break;
                    case 5: MostrarProductos(_almacen.Productos, "No hay productos registrados."); break;
                    case 6: ListarCategorias(); break;
                    case 7: ConsultarPorCategoria(); break;
                    case 8: ModificarStock(); break;
                    case 9: ModificarPrecio(); break;
                    case 10: EliminarProducto(); break;
                    case 11: MostrarProductos(_almacen.ObtenerAgotados(), "No hay productos agotados."); break;
                    case 12: Console.WriteLine(_reporte.GenerarResumen()); break;
                    case 13: CargarDatosEjemplo(); break;
                    case 0: salir = true; Console.WriteLine("Hasta luego."); break;
                }
            }
            catch (Exception ex) when (ex is ArgumentException || ex is InvalidOperationException)
            {
                // Errores de regla de negocio: se informan y el programa continúa.
                Console.WriteLine($"  ! No se pudo completar la operación: {ex.Message}");
            }

            if (!salir) Entrada.Pausa();
        }
    }

    private void MostrarMenu()
    {
        Console.Clear();
        Console.WriteLine($"===== {_almacen.Nombre.ToUpper()} - GESTIÓN DE INVENTARIO =====");
        Console.WriteLine(" 1. Registrar categoría");
        Console.WriteLine(" 2. Registrar producto");
        Console.WriteLine(" 3. Buscar producto por código");
        Console.WriteLine(" 4. Buscar producto por nombre");
        Console.WriteLine(" 5. Listar productos");
        Console.WriteLine(" 6. Listar categorías");
        Console.WriteLine(" 7. Consultar productos por categoría");
        Console.WriteLine(" 8. Modificar stock (entrada/salida)");
        Console.WriteLine(" 9. Modificar precio");
        Console.WriteLine("10. Eliminar producto");
        Console.WriteLine("11. Ver productos agotados");
        Console.WriteLine("12. Resumen del inventario");
        Console.WriteLine("13. Cargar datos de ejemplo");
        Console.WriteLine(" 0. Salir");
        Console.WriteLine("=================================================");
    }

    // ---------- Acciones ----------

    private void RegistrarCategoria()
    {
        string nombre = Entrada.LeerTexto("Nombre de la categoría: ");
        var categoria = _almacen.RegistrarCategoria(nombre);
        Console.WriteLine($"Categoría registrada: {categoria}");
    }

    private void RegistrarProducto()
    {
        if (_almacen.Categorias.Count == 0)
        {
            Console.WriteLine("Primero debe registrar al menos una categoría (opción 1).");
            return;
        }

        string codigo = Entrada.LeerTexto("Código: ");
        if (_almacen.BuscarPorCodigo(codigo) != null)
        {
            Console.WriteLine($"Ya existe un producto con el código '{codigo.ToUpperInvariant()}'.");
            return;
        }

        string nombre = Entrada.LeerTexto("Nombre: ");
        decimal precio = Entrada.LeerDecimal("Precio: ");
        int stock = Entrada.LeerEntero("Stock inicial: ", 0);

        ListarCategorias();
        int idCategoria = Entrada.LeerEntero("Id de la categoría: ", 1);

        var producto = _almacen.RegistrarProducto(codigo, nombre, precio, stock, idCategoria);
        Console.WriteLine($"Producto registrado: {producto.Codigo} - {producto.Nombre}");
    }

    private void BuscarPorCodigo()
    {
        string codigo = Entrada.LeerTexto("Código a buscar: ");
        var producto = _almacen.BuscarPorCodigo(codigo);

        if (producto == null)
            Console.WriteLine($"No se encontró ningún producto con el código '{codigo}'.");
        else
            MostrarProductos(new[] { producto }, "");
    }

    private void BuscarPorNombre()
    {
        string texto = Entrada.LeerTexto("Nombre (o parte del nombre): ");
        MostrarProductos(_almacen.BuscarPorNombre(texto), $"No hay productos que coincidan con '{texto}'.");
    }

    private void ListarCategorias()
    {
        if (_almacen.Categorias.Count == 0)
        {
            Console.WriteLine("No hay categorías registradas.");
            return;
        }

        Console.WriteLine("Categorías:");
        foreach (var c in _almacen.Categorias)
            Console.WriteLine($"  {c}");
    }

    private void ConsultarPorCategoria()
    {
        if (_almacen.Categorias.Count == 0)
        {
            Console.WriteLine("No hay categorías registradas.");
            return;
        }

        ListarCategorias();
        int id = Entrada.LeerEntero("Id de la categoría: ", 1);
        var categoria = _almacen.BuscarCategoria(id);

        if (categoria == null)
        {
            Console.WriteLine($"No existe la categoría con id {id}.");
            return;
        }

        MostrarProductos(_almacen.ObtenerPorCategoria(id), $"No hay productos en la categoría '{categoria.Nombre}'.");
    }

    private void ModificarStock()
    {
        string codigo = Entrada.LeerTexto("Código del producto: ");
        var producto = _almacen.BuscarPorCodigo(codigo);
        if (producto == null)
        {
            Console.WriteLine($"No existe el producto '{codigo}'.");
            return;
        }

        Console.WriteLine($"Stock actual de {producto.Nombre}: {producto.Stock}");
        int cantidad = Entrada.LeerEntero("Cantidad a sumar (positivo) o restar (negativo): ");
        _almacen.ModificarStock(codigo, cantidad);
        Console.WriteLine($"Stock actualizado: {producto.Stock}");
    }

    private void ModificarPrecio()
    {
        string codigo = Entrada.LeerTexto("Código del producto: ");
        var producto = _almacen.BuscarPorCodigo(codigo);
        if (producto == null)
        {
            Console.WriteLine($"No existe el producto '{codigo}'.");
            return;
        }

        Console.WriteLine($"Precio actual de {producto.Nombre}: {producto.Precio:N2}");
        decimal nuevo = Entrada.LeerDecimal("Nuevo precio: ");
        _almacen.ModificarPrecio(codigo, nuevo);
        Console.WriteLine($"Precio actualizado: {producto.Precio:N2}");
    }

    private void EliminarProducto()
    {
        string codigo = Entrada.LeerTexto("Código del producto a eliminar: ");
        var producto = _almacen.BuscarPorCodigo(codigo);
        if (producto == null)
        {
            Console.WriteLine($"No existe el producto '{codigo}'.");
            return;
        }

        if (Entrada.Confirmar($"¿Eliminar '{producto.Nombre}'?"))
        {
            _almacen.EliminarProducto(codigo);
            Console.WriteLine("Producto eliminado.");
        }
        else
        {
            Console.WriteLine("Operación cancelada.");
        }
    }

    private void CargarDatosEjemplo()
    {
        if (_almacen.Categorias.Count > 0 || _almacen.Productos.Count > 0)
        {
            Console.WriteLine("Los datos de ejemplo solo se cargan con el almacén vacío.");
            return;
        }

        var electronica = _almacen.RegistrarCategoria("Electrónica");
        var papeleria = _almacen.RegistrarCategoria("Papelería");
        var limpieza = _almacen.RegistrarCategoria("Limpieza");

        _almacen.RegistrarProducto("E001", "Mouse inalámbrico", 850m, 25, electronica.Id);
        _almacen.RegistrarProducto("E002", "Teclado USB", 1200m, 0, electronica.Id);
        _almacen.RegistrarProducto("P001", "Cuaderno 100 hojas", 95m, 120, papeleria.Id);
        _almacen.RegistrarProducto("P002", "Bolígrafo azul", 15m, 0, papeleria.Id);
        _almacen.RegistrarProducto("L001", "Desinfectante 1L", 180m, 40, limpieza.Id);

        Console.WriteLine("Datos de ejemplo cargados: 3 categorías y 5 productos.");
    }

    // ---------- Presentación ----------

    private static void MostrarProductos(IEnumerable<Producto> productos, string mensajeVacio)
    {
        var lista = productos.ToList();
        if (lista.Count == 0)
        {
            Console.WriteLine(mensajeVacio);
            return;
        }

        Console.WriteLine($"{"Código",-8} {"Nombre",-24} {"Categoría",-14} {"Precio",10} {"Stock",6}  Estado");
        Console.WriteLine(new string('-', 74));
        foreach (var p in lista)
        {
            string estado = p.EstaAgotado ? "AGOTADO" : "Disponible";
            Console.WriteLine($"{p.Codigo,-8} {p.Nombre,-24} {p.Categoria.Nombre,-14} {p.Precio,10:N2} {p.Stock,6}  {estado}");
        }
        Console.WriteLine($"\nTotal: {lista.Count} producto(s).");
    }
}
