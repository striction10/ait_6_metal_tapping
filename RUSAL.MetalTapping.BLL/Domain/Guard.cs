using RUSAL.MetalTapping.BLL.Domain.Exceptions;

namespace RUSAL.MetalTapping.BLL.Domain;

public static class Guard
{
    public static T EnsureFound<T>(T entity, string message)
    {
        if (entity == null)
        {
            throw new NotFoundException(message);
        }

        return entity;
    }
}
