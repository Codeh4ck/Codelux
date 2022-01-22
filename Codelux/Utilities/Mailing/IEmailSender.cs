using System.Threading;
using System.Threading.Tasks;

namespace Codelux.Utilities.Mailing
{
    public interface IEmailSender
    {
        bool SendEmail(Email email);
        Task<bool> SendEmailAsync(Email email, CancellationToken token = default);
    }
}
