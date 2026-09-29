# Sistema de Gestión de Inventario (Almacén)

**Asignatura:** Lenguaje de Programación I (INF-512) — Unidad 3
**Lenguaje:** C# (.NET 8) — Aplicación de consola

## Integrantes
| Nombre | Matrícula | Responsabilidad principal |
|--------|-----------|---------------------------|
| _Completar_ | _Completar_ | _Ej.: Modelos Producto y Categoria_ |
| _Completar_ | _Completar_ | _Ej.: Almacen (búsqueda, eliminación)_ |
| _Completar_ | _Completar_ | _Ej.: Menú y validaciones de entrada_ |
| _Completar_ | _Completar_ | _Ej.: ReporteInventario, UML y memoria_ |

## Descripción
Aplicación de consola para administrar productos, categorías e inventario de una empresa:
registrar, buscar, eliminar y listar productos; evitar códigos duplicados; consultar por
categoría; mostrar productos agotados y generar un resumen del inventario.

## Clases principales
| Clase | Responsabilidad |
|-------|-----------------|
| `Producto` | Datos y reglas de un artículo (precio ≥ 0, stock nunca negativo). |
| `Categoria` | Agrupa productos por tipo. |
| `Almacen` | Dueño de la colección `List<Producto>`; registra, busca, modifica, elimina y consulta. |
| `ReporteInventario` | Genera el resumen del inventario leyendo del `Almacen`. |
| `MenuInventario` | Interfaz de consola (menú). |
| `Entrada` | Lectura y validación segura de datos desde consola. |

## Contenedor utilizado
`List<Producto>` (colección genérica). Justificación y comparación con array en la memoria técnica.

## Cómo ejecutar
Requisito: .NET SDK 8.0 o superior.
```bash
git clone <URL-del-repositorio>
cd InventarioAlmacen
dotnet run
```
Tip: la opción **13** del menú carga datos de ejemplo para probar rápido.

## Distribución del trabajo
_Completar con el reparto real del equipo (debe coincidir con el historial de commits)._
