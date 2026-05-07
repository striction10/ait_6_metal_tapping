namespace RUSAL.MetalTapping.BLL.Application.Contracts;

public record EmailRequest(string email, string subject, string message, Stream pdf);
