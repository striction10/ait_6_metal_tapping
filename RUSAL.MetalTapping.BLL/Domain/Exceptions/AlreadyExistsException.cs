namespace RUSAL.MetalTapping.BLL.Domain.Exceptions;

public class AlreadyExistsException : Exception
{
    public AlreadyExistsException(string message) : base(message) { }
}