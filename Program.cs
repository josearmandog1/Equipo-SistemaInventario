using InventarioAlmacen.Services;
using InventarioAlmacen.UI;

// Program solo ensambla los objetos y arranca el menú.
// Toda la lógica del negocio vive en Almacen y ReporteInventario.
var almacen = new Almacen("Almacén Central");
var reporte = new ReporteInventario(almacen);
var menu = new MenuInventario(almacen, reporte);

menu.Ejecutar();
