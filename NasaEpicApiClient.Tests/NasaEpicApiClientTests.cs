using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NasaEpicApiClient.Models;
using Xunit;

namespace NasaEpicApiClient.Tests
{
    public class NasaEpicApiClientTests
    {
        private readonly NasaEpicApiClientSettings _settings = new NasaEpicApiClientSettings()
        {
            Url = "https://epic.gsfc.nasa.gov/",
            ApiKey = "BsEAYKOQ7mXgy5hNvk8fhxyHatXAJTB6bNggmvGC"
        };


        [Fact]
        public async Task GetValidNaturalImageAvailableDates()
        {
            //Arrange
            var client = new NasaEpicApiClient(_settings);
            //Act
            var result = await client.GetNaturalImageAvailableDates();
            //Assert
            Assert.NotNull(result);
            Assert.IsType<DateTime>(result.First());
        }

        [Theory]
        [InlineData("2022-02-18")]
        public async Task GetValidNaturalImagesForDate(DateTime date)
        {
            //Arrange
            var client = new NasaEpicApiClient(_settings);
            //Act
            var result = await client.GetImagesForDate(date);
            //Assert
            Assert.NotNull(result);
            Assert.IsType<ImageModel>(result.First());
        }


        [Theory]
        [InlineData("2019-05-30", "png", "20190530011359")]
        public async Task GetValidNaturalImageByDateNameAndFromat(DateTime date, string format, string name)
        {
            //Arrange
            var client = new NasaEpicApiClient(_settings);
            //Act
            var result = await client.GetImageByDateName(date, format, name);
            //Assert
            Assert.NotNull(result);
            Assert.IsType<RawImage>(result);
        }
    }
}
