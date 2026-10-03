# 📦 Sistema de Gestión de Inventario

Aplicación de consola orientada a objetos desarrollada en C# para la administración eficiente de productos, categorías e inventario de una empresa. Proyecto correspondiente a la **Actividad Grupal Integradora - Unidad 3**.

**Asignatura:** Lenguaje de Programación I (INF 5120 - Z01)  
**Universidad:** Universidad Autónoma de Santo Domingo (UASD)  
**Facilitador:** Victor Nuñez Vicente  

---

## 👥 Integrantes del Equipo

| Nombre | Usuario GitHub | Matrícula |
| :--- | :--- | :--- |
| Cesar Aybar | [@XxNatsuENDxX](https://github.com/XxNatsuENDxX) | 100692670 |
| Dennis Faneyte | [@dennisfaneyte-lab](https://github.com/dennisfaneyte-lab) | 100534187 |
| José García | [@josearmandog1](https://github.com/josearmandog1) | 100680928 |
| Jeremy Alcequiez | [@Jeremyalce02-boop](https://github.com/Jeremyalce02-boop) | 100633989 |

---

## 📝 Descripción del Proyecto

Este sistema resuelve la necesidad de centralizar la información de los productos y sus existencias. Permite realizar operaciones CRUD (Crear, Leer, Actualizar, Eliminar) sobre el inventario, evitando la duplicidad de datos mediante validaciones, y facilitando la consulta filtrada por categorías. Además, incluye un sistema robusto de manejo de errores que evita el cierre abrupto de la aplicación por entradas incorrectas del usuario.

## 🏗️ Clases Principales

* **`Producto`**: Define la entidad base del dominio, encapsulando propiedades como identificador, nombre, precio, cantidad en stock y su categoría correspondiente.
* **`Categoria`**: Maneja la clasificación de los productos para facilitar su búsqueda y filtrado.
* **`Almacen` (Clase Administradora)**: Contiene la colección principal de objetos y expone los métodos lógicos para buscar, agregar y eliminar productos.
* **`ReporteInventario`**: Responsable de generar consultas, leer existencias y mostrar listados específicos (ej. productos agotados).

## 📦 Contenedor Utilizado y Justificación

Se implementó la colección genérica **`List<T>`** como contenedor principal del sistema. 

**Justificación técnica:** En un sistema de inventario, la cantidad de productos es altamente variable. El uso de `List<T>` resulta ser la opción más apropiada gracias a su capacidad de redimensionamiento dinámico (crece automáticamente a medida que se registran nuevos productos), evitando la necesidad de establecer un tamaño fijo inicial como sucede con los *Arrays*. Además, simplifica considerablemente operaciones rutinarias como la inserción (`.Add()`) y la eliminación (`.Remove()`) abstrayendo la lógica manual de manejo de índices y espacios vacíos.

## 🚀 Instrucciones de Ejecución

1. Clona este repositorio en tu máquina local:
   ```bash
   git clone https://github.com/josearmandog1/Equipo-SistemaInventario.git
   ```
2. Abre la solución utilizando **Visual Studio** o cualquier IDE compatible con .NET (como VS Code con la extensión de C#).
3. Restaura las dependencias (si aplica) y compila el proyecto (`Ctrl + Shift + B`).
4. Ejecuta la aplicación de consola (`F5` o `Ctrl + F5`).
5. Sigue las instrucciones del menú interactivo en la terminal.

## 📊 Distribución General del Trabajo

* **Cesar Aybar:** Modelado de clases base y validaciones try-catch de entradas de consola.
* **Dennis Faneyte:** Implementación de la clase administradora y gestión de la colección `List<T>`.
* **José García:** Desarrollo de los reportes y lógicas de filtros por identificador de categoría.
* **Jeremy Alcequiez:** Diagramación del documento UML y orquestación del menú en el bloque `Main`.