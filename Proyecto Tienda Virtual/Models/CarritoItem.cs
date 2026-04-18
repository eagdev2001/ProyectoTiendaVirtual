using System.ComponentModel.DataAnnotations;

public class CarritoItem
{
    public int ProductoId { get; set; }
    public string Nombre { get; set; }
    public double Precio { get; set; }
    public int Cantidad { get; set; }

}