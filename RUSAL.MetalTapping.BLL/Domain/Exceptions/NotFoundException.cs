namespace RUSAL.MetalTapping.BLL.Domain.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message): base(message) { }
    }
}