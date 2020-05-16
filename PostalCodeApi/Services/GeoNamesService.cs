using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using PostalCodeApi.Domain.Models;
using PostalCodeApi.Domain.Services;

namespace PostalCodeApi.Services
{
    public class GeoNamesService : IGeoNamesService
    {
        private readonly ICityService _cityService;
        private readonly IConfiguration _configuration;
        private readonly IPostalCodeCityService _postalCodeCityService;
        private readonly IPostalCodeService _postalCodeService;

        public GeoNamesService(IConfiguration configuration, IPostalCodeService postalCodeService,
            ICityService cityService, IPostalCodeCityService postalCodeCityService)
        {
            _configuration = configuration;
            _postalCodeService = postalCodeService;
            _cityService = cityService;
            _postalCodeCityService = postalCodeCityService;
        }

        public async Task ImportAsync(string countryIso)
        {
            // Convert the country iso to uppercase
            var upperCountryIso = countryIso.ToUpperInvariant();
            // Folder where downloaded files are stored
            var downloadsFolder = _configuration.GetValue<string>("ImportOptions:DownloadsFolder");
            // Path to the downloaded file
            var downloadedFile = downloadsFolder + upperCountryIso +
                                 _configuration.GetValue<string>("ImportOptions:DownloadFileExtension");
            // Path to the extracted file
            var extractedFile = downloadsFolder + upperCountryIso +
                                _configuration.GetValue<string>("ImportOptions:ExtractedFileExtension");

            // Check if the folder exists or create it
            var exists = Directory.Exists(downloadsFolder);
            if (!exists)
                Directory.CreateDirectory(downloadsFolder);

            // Download the zip file containing the Postal codes
            using (var client = new WebClient())
            {
                client.DownloadFile(
                    _configuration.GetValue<string>("ImportOptions:DownloadUrl") + upperCountryIso +
                    _configuration.GetValue<string>("ImportOptions:DownloadFileExtension"), downloadedFile);
            }

            try
            {
                // Extract the downloaded zip
                ZipFile.ExtractToDirectory(downloadedFile, downloadsFolder);

                // Read the file line by line
                foreach (var line in await File.ReadAllLinesAsync(extractedFile, Encoding.UTF8))
                {
                    var fields = line.Split("\t");
                    
                    // Get or add the postal code
                    var postalCode = await _postalCodeService.FindMatchOrSaveAsync(new PostalCode
                    {
                        Code = fields[1],
                        CountryIso = fields[0]
                    });

                    // Get or add the city
                    var city = await _cityService.FindMatchOrSaveAsync(new City
                    {
                        Name = fields[2]
                    });

                    // Get or add the link between the city and postal code
                    await _postalCodeCityService.FindMatchOrSaveAsync(new PostalCodeCity
                    {
                        CityId = city.Resource.Id,
                        PostalCodeId = postalCode.Resource.Id
                    });
                }
            }
            finally
            {
                // Clean up the downloads folder
                var dir = new DirectoryInfo(downloadsFolder);

                foreach (var fi in dir.GetFiles()) fi.Delete();
            }
        }
    }
}