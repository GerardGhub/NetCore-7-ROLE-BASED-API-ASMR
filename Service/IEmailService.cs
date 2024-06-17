using LearnAPI.Modal;

namespace LearnAPI.Service
{
    public interface IEmailService
    {
        Task SendEmail(Mailrequest mailrequest);
    }
}
