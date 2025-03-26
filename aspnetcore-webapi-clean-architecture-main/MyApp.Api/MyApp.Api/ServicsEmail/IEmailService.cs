using MyApp.Api.MailHelper;
using System.Runtime.InteropServices;

namespace MyApp.Api.ServicsEmail
{
  public interface IEmailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
    }


}
