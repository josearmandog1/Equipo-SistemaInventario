# Guía para la memoria técnica y la defensa

## 3. Justificación del contenedor: `List<Producto>`
- El inventario cambia constantemente (se registran y eliminan productos); no se conoce la
  cantidad de antemano.
- `List<T>` crece sola, y `Add`, `Remove`, `FirstOrDefault`, `Where` evitan reescribir a mano
  el desplazamiento de elementos que exigiría un array.
- Menos código = menos errores y mayor mantenibilidad (`Producto[]` obligaría a llevar un
  contador, redimensionar y compactar huecos al eliminar).

## 4. Comparación técnica Array vs List<T>
| Criterio | Array (`Producto[]`) | `List<Producto>` |
|----------|----------------------|------------------|
| Tamaño | Fijo, se define al crearlo | Dinámico, crece automáticamente |
| Inserción | Manual: buscar posición libre o redimensionar | `Add()` / `Insert()` |
| Eliminación | Manual: desplazar elementos y llevar contador | `Remove()` / `RemoveAt()` |
| Búsqueda | Recorrido manual o `Array.Find` (O(n)) | `Find`, `FirstOrDefault`, LINQ (O(n)) |
| Flexibilidad | Baja | Alta |
| Complejidad de implementación | Alta (más código de control) | Baja |
| Uso de memoria | Exacto, sin espacio extra | Reserva capacidad extra (crece al duplicar) |
| Escalabilidad | Limitada al tamaño inicial | Buena para tamaños variables |

**¿En qué escenario cambiaría por un array?** Si el número de elementos fuera fijo y conocido
(ej. exactamente 12 estantes) o en código donde el rendimiento/memoria fuera crítico y el tamaño
nunca cambiara. (Si la búsqueda por código fuera masiva, la mejor alternativa sería un
`Dictionary<string, Producto>`, pero eso es otra estructura distinta.)

## 5. Análisis de relaciones entre clases
| # | Clases | Tipo | Multiplicidad | Ciclo de vida |
|---|--------|------|---------------|---------------|
| 1 | Almacen → Producto | **Agregación** | 1 a 0..* | El producto es gestionado por el almacén; puede existir como concepto sin él, pero en esta implementación solo se crea dentro del almacén. |
| 2 | Producto → Categoria | **Asociación** | 0..* a 1 | Un producto siempre pertenece a una categoría; la categoría existe sin productos. |
| 3 | ReporteInventario → Almacen | **Dependencia** | uso temporal | El reporte solo lee datos del almacén; no lo posee ni lo modifica. |
| 4 | MenuInventario → Almacen | **Asociación** | 1 a 1 | El menú recibe el almacén por constructor y lo usa. |

**Pregunta obligatoria: «¿Puede uno de estos objetos existir independientemente del otro?»**
- *Categoria y Producto:* **Sí, la Categoria puede existir sin productos** (una categoría nueva
  está vacía), pero un Producto **no** puede existir sin categoría (el constructor lo exige).
  Por eso es asociación y no composición: eliminar un producto no elimina su categoría.
- *Almacen y Producto:* al eliminar un producto del almacén, el objeto deja de ser gestionado;
  por eso es agregación (no composición estricta). Decide y defiende tu postura: si tu equipo
  considera que un producto no tiene sentido fuera del almacén, se justifica composición.

## 6. Cohesión, acoplamiento, modularidad
- **Cohesión alta:** cada clase tiene una sola responsabilidad (Producto = sus reglas,
  Almacen = colección y operaciones, ReporteInventario = reportes, MenuInventario/Entrada = consola).
- **Bajo acoplamiento:** Almacen y Producto no conocen la consola; ReporteInventario devuelve
  un `string` en vez de imprimir; la colección se expone como `IReadOnlyList`.
- **Modularidad:** carpetas `Models`, `Services`, `UI`; `Program.cs` solo ensambla y arranca.
- **Encapsulamiento:** `private set`, listas privadas, validaciones en constructores y métodos.

## Requisitos del mandato → dónde están en el código
| Requisito | Ubicación |
|-----------|-----------|
| Registrar / buscar / eliminar / listar productos | `Almacen.RegistrarProducto/BuscarPorCodigo/BuscarPorNombre/EliminarProducto`, opciones 2–5, 10 |
| Evitar códigos duplicados | `Almacen.RegistrarProducto` (y verificación previa en `MenuInventario`) |
| Consultar por categoría | `Almacen.ObtenerPorCategoria`, opción 7 |
| Productos agotados | `Almacen.ObtenerAgotados`, opción 11 |
| Resumen del inventario | `ReporteInventario.GenerarResumen`, opción 12 |
| Validar números y menú | `Entrada.LeerEntero / LeerDecimal` |
| Búsquedas sin resultado / objeto inexistente | Mensajes en `MenuInventario` |

## Posibles preguntas/modificaciones en la defensa (practíquenlas)
1. «Agrega búsqueda por rango de precio» → nuevo método en `Almacen` + opción en el menú.
2. «Impide registrar un producto con precio 0» → cambiar la validación en `Producto`.
3. «Agrega un stock mínimo por producto y lista los que estén por debajo» → propiedad en `Producto` + consulta en `Almacen`.
4. «¿Por qué `BuscarPorCodigo` devuelve `null` y no lanza excepción?» → buscar sin resultado es un caso normal, no un error.
5. «¿Por qué `ReporteInventario` devuelve string?» → separar lógica de presentación (bajo acoplamiento).

## Plan de commits sugerido (mínimo 2 significativos por integrante)
- "Crear modelo inicial de Producto y Categoria"
- "Implementar Almacen con registro y búsqueda por código"
- "Agregar validación de códigos duplicados"
- "Implementar consulta por categoría y productos agotados"
- "Agregar validaciones de entrada en consola (Entrada)"
- "Implementar ReporteInventario con resumen"
- "Agregar UML y README"
Que cada integrante haga sus commits **conforme avanza**, no todos al final.
