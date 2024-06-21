using ClosedXML.Excel;
using HtmlAgilityPack;
using Newtonsoft.Json;
using System;
using Test_sourcing.Models;

namespace Test_sourcing.DataProviders
{
    public class CompanyDataProvider
    {
        public async Task<List<Company>> ScrapeCompaniesFromUrl(Site site)
        {
            List<Company> companies = new List<Company>();

            using (HttpClient client = new HttpClient())
            {
                string? response = await client.GetStringAsync(site.Url);
                HtmlDocument? document = new HtmlDocument();
                document.LoadHtml(response);

                var nameNodes = document.DocumentNode.SelectNodes(site.NameXPath);
                var descriptionNodes = document.DocumentNode.SelectNodes(site.DescriptionXPath);

                if (nameNodes != null && descriptionNodes != null)
                {
                    for (int i = 0; i < nameNodes.Count; i++)
                    {
                        var company = new Company
                        {
                            Name = nameNodes[i]?.InnerText.Trim(),
                            Description = descriptionNodes[i]?.InnerText.Trim(),
                            Revenue = "empty",
                            Employees = 0,
                            Location = Enum.Parse<EnumLocation>(site.Location),
                        };
                        companies.Add(company);
                    }
                }
                else
                {
                    Console.WriteLine($"No company cards found on page: {site.Url}");
                }
            }

            return companies;
        }

        public void SaveToExcel(List<Company> companies)
        {
            string currentDateTime = DateTime.Now.ToString("ddMMyy_HHmm");
            string filePath = Path.Combine("FileOutputs", $"companies_data_{currentDateTime}.xlsx");

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Companies");
                worksheet.Cell(1, 1).Value = "Name";
                worksheet.Cell(1, 2).Value = "Description";
                worksheet.Cell(1, 3).Value = "Revenue (2022)";
                worksheet.Cell(1, 4).Value = "Employees";
                worksheet.Cell(1, 5).Value = "Location";

                for (int i = 0; i < companies.Count; i++)
                {
                    worksheet.Cell(i + 2, 1).Value = companies[i].Name;
                    worksheet.Cell(i + 2, 2).Value = companies[i].Description;
                    worksheet.Cell(i + 2, 3).Value = companies[i].Revenue ;
                    worksheet.Cell(i + 2, 4).Value = companies[i].Employees;
                    worksheet.Cell(i + 2, 5).Value = companies[i].Location.ToString();
                }

                var headerStyle = workbook.Style;
                headerStyle.Font.Bold = true;
                headerStyle.Fill.BackgroundColor = XLColor.LightBlue;

                worksheet.Column(1).Width = 20;  // Largeur de la première colonne
                worksheet.Column(2).Width = 60;  // Largeur de la deuxième colonne
                worksheet.Column(3).Width = 10;  // Largeur de la troisième colonne
                worksheet.Column(4).Width = 10;  // Largeur de la 4 colonne
                worksheet.Column(5).Width = 10;  // Largeur de la 5 colonne


                worksheet.Row(1).Style = headerStyle;

                workbook.SaveAs(filePath);
                Console.WriteLine($"Excel has been successfully saved at : {filePath}");
            }
        }
        }
    }

