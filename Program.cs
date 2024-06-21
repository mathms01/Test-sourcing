using Test_sourcing.DataProviders;
using Test_sourcing.Models;
using Test_sourcing.Models.Seeders;

class Program
{
    static async Task Main(string[] args)
    {
        CompanyDataProvider companyDataProvider = new CompanyDataProvider();
        XPathConfiguration? config = await DataInitializer.LoadConfig();

        if (config == null)
        {
            throw new Exception("Error : Can't get the configuration");
        }

        List<Company> data = new List<Company>();

        foreach (Site? site in config.Sites)
        {
            List<Company> companies = await companyDataProvider.ScrapeCompaniesFromUrl(site);
            data.AddRange(companies);
        }

        companyDataProvider.SaveToExcel(data);
    }
}
