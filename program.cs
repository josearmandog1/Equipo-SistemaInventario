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