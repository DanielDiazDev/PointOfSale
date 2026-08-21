namespace PointOfSale.Model.Primitives;

public class DomainException : Exception
{
    public DomainException(string message) : base(message)
    {
    }

    
}