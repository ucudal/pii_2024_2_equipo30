namespace Library;

//clase interface hecha para seguir la guia de diseño y que mantenga un bajo acoplamiento
public interface IItem
{
    int MaxHealt { get; set; }
    string ItemsName { get; set; }
    string ItemsDescription { get; set; }
    int Quantity { get; set; }
    
    void Use(Pokemon pokemon); //para aplicar los items se necesita usar el metodo use sino no surge efecto según la guia de diseño
    void Consume();
}