namespace Business.Constants;

public class MailSettings
{
    public string SenderEmail { get; set; }
    public string RecipientEmail { get; set; }
    public string Subject { get; set; }
    public string SmtpServer { get; set; }
    public int SmtpPort { get; set; }
    public string AppPassword { get; set; }
    public string WebsiteName { get; set; }
}