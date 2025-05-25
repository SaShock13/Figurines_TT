public class FigurineData
{
    public ShapeType shape;
    public BackColor color;
    public AnimalType animal;

    public FigurineData(ShapeType shape, BackColor color, AnimalType animal)
    {
        this.shape = shape;
        this.color = color;
        this.animal = animal;
    }

    public bool IsSame(FigurineData other)
    {
        return shape == other.shape && color == other.color && animal == other.animal;
    }
}