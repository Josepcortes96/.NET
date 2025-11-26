namespace FitData.Entidades;

    public class ListaEspera
{
    public int IdLista { get; set; }
    public int IdCliente { get; set; }
    public int IdHorario { get; set; }
    public int Posicion { get; set; }  // 1 = primero en la lista
}
