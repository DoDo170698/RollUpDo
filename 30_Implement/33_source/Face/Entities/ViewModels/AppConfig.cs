using System.ComponentModel.DataAnnotations;

namespace Entities.ViewModels
{

    public class AppConfig
    {
        public long Id { get; set; }
        public string SiteName { get; set; }
        public string Slogan { get; set; }
        public string Logo { get; set; }
        public string Copyright { get; set; }
        public string Email { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string HotLine { get; set; }
        public string WebsiteAddress { get; set; }

        public string FacebookPage { get; set; }

        public string FacebookAppId { get; set; }

        //SMTP
        public string SMTPEmail { get; set; }

        public string SMTPPassword { get; set; }

        public string SMTPName { get; set; }

        public bool Activate { get; set; }

        public string Describe()
        {
            return "SystemInformation";
        }
    }

    public class HeadViewModel
    {
        public string Company { get; set; }
        public string Authen { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
    }
}
