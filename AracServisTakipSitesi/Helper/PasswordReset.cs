using System.Net.Mail;

namespace AracServisTakipSitesi.Helper
{
    public static class PasswordReset
    {
        public static void PasswordResetSendEmail(string link, string email)

        {
            MailMessage mail = new MailMessage();

            SmtpClient smtpClient = new SmtpClient("mail.kucukmurat.com");

            mail.From = new MailAddress("admin@kucukmurat.com");
            mail.To.Add(email);

            mail.Subject = $"www.kucukmurat.com::Şifre sıfırlama";
            mail.Body = "<h2>Şifrenizi yenilemek için lütfen aşağıdaki linke tıklayınız.</h2><hr/>";
            mail.Body += $"<a href='{link}'>şifre yenileme linki</a>";
            mail.IsBodyHtml = true;
            smtpClient.Port = 587;
            smtpClient.Credentials = new System.Net.NetworkCredential("admin@kucukmurat.com", "1234");

            smtpClient.Send(mail);
        }
    }
}