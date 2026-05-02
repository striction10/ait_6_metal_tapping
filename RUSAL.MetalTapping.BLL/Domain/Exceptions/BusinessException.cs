namespace RUSAL.MetalTapping.BLL.Domain.Exceptions;

public class BusinessException : Exception
{
    public BusinessException(string message) : base(message){ }
}
