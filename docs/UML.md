# Diagrama UML (Mermaid)

Pega este bloque en https://mermaid.live para exportarlo como imagen/PDF, o usa la extensión
de Mermaid en VS Code. GitHub también lo renderiza directamente.

```mermaid
classDiagram
    direction LR

    class Categoria {
        -int Id
        -string Nombre
        +Categoria(int id, string nombre)
        +ToString() string
    }

    class Producto {
        -string Codigo
        -string Nombre
        -decimal Precio
        -int Stock
        -Categoria Categoria
        +bool EstaAgotado
        +decimal ValorTotal
        +Producto(string, string, decimal, int, Categoria)
        +AjustarStock(int cantidad) void
        +ActualizarPrecio(decimal nuevoPrecio) void
    }

    class Almacen {
        -List~Producto~ _productos
        -List~Categoria~ _categorias
        -int _siguienteIdCategoria
        +string Nombre
        +RegistrarCategoria(string) Categoria
        +BuscarCategoria(int) Categoria
        +RegistrarProducto(string, string, decimal, int, int) Producto
        +BuscarPorCodigo(string) Producto
        +BuscarPorNombre(string) List~Producto~
        +EliminarProducto(string) bool
        +ModificarStock(string, int) bool
        +ModificarPrecio(string, decimal) bool
        +ObtenerPorCategoria(int) List~Producto~
        +ObtenerAgotados() List~Producto~
    }

    class ReporteInventario {
        -Almacen _almacen
        +ReporteInventario(Almacen)
        +GenerarResumen() string
    }

    class MenuInventario {
        -Almacen _almacen
        -ReporteInventario _reporte
        +Ejecutar() void
    }

    class Entrada {
        <<static>>
        +LeerTexto(string) string
        +LeerEntero(string, int, int) int
        +LeerDecimal(string, decimal) decimal
        +Confirmar(string) bool
        +Pausa() void
    }

    Almacen "1" o-- "0..*" Producto : contiene
    Almacen "1" o-- "0..*" Categoria : administra
    Producto "0..*" --> "1" Categoria : pertenece a
    ReporteInventario ..> Almacen : depende de (lee)
    MenuInventario --> Almacen : usa
    MenuInventario --> ReporteInventario : usa
    MenuInventario ..> Entrada : usa
```

> Nota: los miembros marcados con `-` en Producto/Categoria son campos privados expuestos como
> propiedades públicas con `private set` (o solo lectura). Si tu profesor pide visibilidad
> explícita de las propiedades, cámbialas a `+`.
