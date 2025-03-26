using Microsoft.AspNet.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MyApp.Api.MailHelper;
using MyApp.Api.ServicsEmail;

namespace MyApp.Api.MailHelper
{
    public class MailRequest
    {
        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }

    }
}






//google app passwords

///////////////////

//private readonly IEmailService _emailService;
//public AuthService(ApplicationDbContext context, IConfiguration configuration, IEmailService emailService)
//{
//    _context = context;
//    _configuration = configuration;
//    _emailService = emailService;
//}

///////////////////////////////////////////////////////////
//try
//{
//    // SMTP Email

//    MailRequest mail = new MailRequest();

//    mail.ToEmail = user.Email;
//    mail.Subject = "Hello from Booking";
//    mail.Body = $@"
//          <div style='font-family: Arial, sans-serif; max-width: 600px; margin: 0 auto; padding: 20px; border: 1px solid #ddd; border-radius: 10px;'>
//              <div style='background-color: #4CAF50; color: white; padding: 10px; text-align: center; font-size: 20px; border-radius: 10px 10px 0 0;'>
//                  Welcome to BookingHotels!
//              </div>
//              <div style='padding: 20px;'>
//                  <p>Dear <strong>{user.UserName}</strong>,</p>
//                  <p>Thank you for registering on <strong>BookingHotels</strong>. Your account has been successfully created, and you can now start booking your favorite hotels with ease.</p>
//                  <p>Here are your account details:</p>
//                  <table border='0' cellpadding='5'>
//                      <tr><td><strong>Name:</strong></td><td>{user.UserName}</td></tr>
//                      <tr><td><strong>Email:</strong></td><td>{user.Email}</td></tr>
//                  </table>
//                  <p>You can now log in and explore exclusive deals and comfortable stays at the best hotels.</p>
//                  <p>If you have any questions or need assistance, feel free to contact our support team.</p>
//                  <p>We’re excited to have you on board!</p>
//              </div>
//              <div style='text-align: center; font-size: 14px; margin-top: 20px; color: #555;'>
//                  Best regards,<br>
//                  <strong>BookingHotels Team</strong><br>
//                  <a href='mailto:support@bookinghotels.com'>support@bookinghotels.com</a> | <a href='tel:+1234567890'>+1234567890</a>
//              </div>
//          </div>";

//    await _emailService.SendEmailAsync(mail);


//}
//catch (Exception ex)
//{

//    Console.WriteLine($"Error sending email: {ex.Message}");
//}

