namespace OSS.Domain.Sms;

public interface ISmsSender
{
    Task SendSmsAsync(string number, string message);
}
