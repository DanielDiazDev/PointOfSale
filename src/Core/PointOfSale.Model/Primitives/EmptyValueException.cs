namespace PointOfSale.Model.Primitives;

public class EmptyValueException : DomainException
{
    public EmptyValueException(string fieldName)
        : base($"The field '{fieldName}' is empty or null.")
    {
    }
}