using CensusAnalyserProblem;
using NUnit.Framework;
using static CensusAnalyserProblem.CensusAnalyser;
using System.Collections.Generic;
using Newtonsoft.Json;
using CensusAnalyserProblem.DAO;

namespace CensusAnalyserTest
{
    public class Tests
    {
        private string csvFilePath = @"C:\Users\abhishek verma\source\repos\CensusAnalyserTest\CsvFile\IndiaStateCensusData.csv";
        private string invalidCSVFilePath = @"C:\Users\abhishek verma\source\CensusAnalyserTest\CsvFile\IndiaStateCensusData.csv";
        private string incorrectFileType = @"C:\Users\abhishek verma\source\repos\CensusAnalyserTest\CsvFile\people.txt";
        private string csvFilePathWithInvalidDelimeter = @"C:\Users\abhishek verma\source\repos\CensusAnalyserTest\CsvFile\IndiaStateCensusDataWithInvalidDelimeter.csv";
        private string csvFilePathWithIncorrectHeader = @"C:\Users\abhishek verma\source\repos\CensusAnalyserTest\CsvFile\IndiaStateCensusDataWithIncorrectHeader.csv";
        private string csvStateCodeFilePath = @"C:\Users\abhishek verma\source\repos\CensusAnalyserTest\CsvFile\IndiaStateCode.csv";
        private string csvStateCodeFilePathWithInvalidDelimeter = @"C:\Users\abhishek verma\source\repos\CensusAnalyserTest\CsvFile\IndiaStateCodeWithInvalidDelimeter.csv";
        private string usCSVFilePath = @"C:\Users\abhishek verma\source\repos\CensusAnalyserTest\CsvFile\USCensusData.csv";
        private string stateCensusFileHeader = "State,Population,AreaInSqKm,DensityPerSqKm";
        private string stateCodeFileHeader = "SrNo,State Name,TIN,StateCode";
        private string usCensusFileHeader = "State Id,State,Population,Housing units,Total area,Water area,Land area,Population Density,Housing Density";
        CensusAnalyser censusAnalyser;
        Dictionary<string, CensusDataDAO> numberOfEntries;

        [SetUp]
        public void Setup()
        {
            censusAnalyser = new CensusAnalyser();
            numberOfEntries = new Dictionary<string, CensusDataDAO>();
        }

        [Test]
        public void givenIndianCensusCSVFileReturnsCorrectRecords()
        {
            numberOfEntries = censusAnalyser.loadCensusData(Country.INDIA, csvFilePath, csvStateCodeFilePath);
            Assert.AreEqual(29, numberOfEntries.Count);
        }

        [Test]
        public void givenIndiaCensusData_WhenWrongFile_ShouldThrowException()
        {
            var exception = Assert.Throws<CensusAnalyserException>(() => censusAnalyser.loadCensusData(Country.INDIA, invalidCSVFilePath));            
            Assert.AreEqual(CensusAnalyserException.ExceptionType.FILE_NOT_FOUND, exception.type);
        }

        [Test]
        public void givenIndiaCensusData_WhenIncorrectFileType_ShouldThrowException()
        {
            var exception = Assert.Throws<CensusAnalyserException>(() => censusAnalyser.loadCensusData(Country.INDIA, incorrectFileType));
            Assert.AreEqual(CensusAnalyserException.ExceptionType.INCORRECT_FILE_TYPE, exception.type);
        }

        [Test]
        public void givenIndiaCensusData_WhenFileContainInvalidDelimeter_ShouldThrowException()
        {
            var exception = Assert.Throws<CensusAnalyserException>(() => censusAnalyser.loadCensusData(Country.INDIA, csvFilePathWithInvalidDelimeter));
            Assert.AreEqual(CensusAnalyserException.ExceptionType.FILE_CONTAIN_INVALID_DELIMITER, exception.type);
        }

        [Test]
        public void givenIndianCensusCSVFile_WhenIncorrectHeadersInFile_ShouldThrowException()
        {
            var exception = Assert.Throws<CensusAnalyserException>(() => censusAnalyser.loadCensusData(Country.INDIA, csvFilePathWithIncorrectHeader));
            Assert.AreEqual(CensusAnalyserException.ExceptionType.INVALID_HEADERS, exception.type);
        }

        [Test]
        public void givenStateCodeData_WhenFileNotFound_ShouldThrowException()
        {
            var exception = Assert.Throws<CensusAnalyserException>(() => censusAnalyser.loadCensusData(Country.INDIA, invalidCSVFilePath));
            Assert.AreEqual(CensusAnalyserException.ExceptionType.FILE_NOT_FOUND, exception.type);
        }

        [Test]
        public void givenStateCodeData_WhenIncorrectFileType_ShouldThrowException()
        {
            var exception = Assert.Throws<CensusAnalyserException>(() => censusAnalyser.loadCensusData(Country.INDIA, incorrectFileType));
            Assert.AreEqual(CensusAnalyserException.ExceptionType.INCORRECT_FILE_TYPE, exception.type);
        }

        [Test]
        public void givenStateCodeData_WhenIncorrectHeadersInFile_ShouldThrowException()
        {
            var exception = Assert.Throws<CensusAnalyserException>(() => censusAnalyser.loadCensusData(Country.INDIA, csvFilePathWithIncorrectHeader));
            Assert.AreEqual(CensusAnalyserException.ExceptionType.INVALID_HEADERS, exception.type);
        }

        [Test]
        public void givenIndianStateCensusData_WhenSortedOnState_ShouldReturnSortedResult()
        {
            Dictionary<string, CensusDataDAO> censusData = censusAnalyser.loadCensusData(Country.INDIA, csvFilePath, csvStateCodeFilePath);
            string sortedData = censusAnalyser.getStateWiseSortedCensusData(censusData, SortFeild.SortBy.STATE);
            CensusDataDAO[] sortedCensusData = JsonConvert.DeserializeObject<CensusDataDAO[]>(sortedData);
            Assert.AreEqual("Andhra Pradesh", sortedCensusData[0].state);
        }

        [Test]
        public void givenIndianStateCSVFile_WhenProper_ShouldReturnSortedDataAccordingToStateCodeInJSONFormats()
        {
            Dictionary<string, CensusDataDAO> censusData = censusAnalyser.loadCensusData(Country.INDIA, csvFilePath, csvStateCodeFilePath);
            string sortedData = censusAnalyser.getStateWiseSortedCensusData(censusData, SortFeild.SortBy.STATE_CODE);
            CensusDataDAO[] sortedStateCensusData = JsonConvert.DeserializeObject<CensusDataDAO[]>(sortedData);
            Assert.AreEqual("AD", sortedStateCensusData[0].stateCode);
        }

        [Test]
        public void givenIndianCensusData_WhenSortedOnPopulation_ShouldReturnSortedResult()
        {
            Dictionary<string, CensusDataDAO> censusData = censusAnalyser.loadCensusData(Country.INDIA, csvFilePath, csvStateCodeFilePath);
            string sortedData = censusAnalyser.getStateWiseSortedCensusData(censusData, SortFeild.SortBy.POPULATION);
            CensusDataDAO[] sortedStateCensusData = JsonConvert.DeserializeObject<CensusDataDAO[]>(sortedData);
            Assert.AreEqual("Uttar Pradesh", sortedStateCensusData[0].state);
        }

        [Test]
        public void givenIndianCensusData_WhenSortedOnPopulationDensity_ShouldReturnSortedResult()
        {
            Dictionary<string, CensusDataDAO> censusData = censusAnalyser.loadCensusData(Country.INDIA, csvFilePath, csvStateCodeFilePath);
            string sortedData = censusAnalyser.getStateWiseSortedCensusData(censusData, SortFeild.SortBy.POPULATION_DENSITY);
            CensusDataDAO[] sortedStateCensusData = JsonConvert.DeserializeObject<CensusDataDAO[]>(sortedData);
            Assert.AreEqual("Bihar", sortedStateCensusData[0].state);
        }

        [Test]
        public void givenIndianCensusData_WhenSortedOnTotalArea_ShouldReturnSortedResult()
        {
            Dictionary<string, CensusDataDAO> censusData = censusAnalyser.loadCensusData(Country.INDIA, csvFilePath, csvStateCodeFilePath);
            string sortedData = censusAnalyser.getStateWiseSortedCensusData(censusData, SortFeild.SortBy.TOTAL_AREA);
            CensusDataDAO[] sortedStateCensusData = JsonConvert.DeserializeObject<CensusDataDAO[]>(sortedData);
            Assert.AreEqual("Rajasthan", sortedStateCensusData[0].state);
        }

        [Test]
        public void givenUSCensusCSVFileReturnsCorrectRecords()
        {
            numberOfEntries = censusAnalyser.loadCensusData(Country.US, usCSVFilePath);
            Assert.AreEqual(51, numberOfEntries.Count);
        }

        [Test]
        public void givenUSCensusData_WhenWrongFile_ShouldThrowException()
        {
            var exception = Assert.Throws<CensusAnalyserException>(() => censusAnalyser.loadCensusData(Country.US, invalidCSVFilePath));
            Assert.AreEqual(CensusAnalyserException.ExceptionType.FILE_NOT_FOUND, exception.type);
        }

        [Test]
        public void givenUSCensusData_WhenIncorrectFileType_ShouldThrowException()
        {
            var exception = Assert.Throws<CensusAnalyserException>(() => censusAnalyser.loadCensusData(Country.US, incorrectFileType));
            Assert.AreEqual(CensusAnalyserException.ExceptionType.INCORRECT_FILE_TYPE, exception.type);
        }

        [Test]
        public void givenUSCensusCSVFile_WhenIncorrectHeadersInFile_ShouldThrowException()
        {
            var exception = Assert.Throws<CensusAnalyserException>(() => censusAnalyser.loadCensusData(Country.US, csvFilePathWithIncorrectHeader));
            Assert.AreEqual(CensusAnalyserException.ExceptionType.INVALID_HEADERS, exception.type);
        }

        [Test]
        public void givenUSCensusData_WhenSortedOnPopulation_ShouldReturnSortedResult()
        {
            Dictionary<string, CensusDataDAO> censusData = censusAnalyser.loadCensusData(Country.US, usCSVFilePath);
            string sortedData = censusAnalyser.getStateWiseSortedCensusData(censusData, SortFeild.SortBy.POPULATION);
            CensusDataDAO[] sortedStateCensusData = JsonConvert.DeserializeObject<CensusDataDAO[]>(sortedData);
            Assert.AreEqual("California", sortedStateCensusData[0].state);
        }

        [Test]
        public void givenUSCensusData_WhenSortedOnPopulationDensity_ShouldReturnSortedResult()
        {
            Dictionary<string, CensusDataDAO> censusData = censusAnalyser.loadCensusData(Country.US, usCSVFilePath);
            string sortedData = censusAnalyser.getStateWiseSortedCensusData(censusData, SortFeild.SortBy.POPULATION_DENSITY);
            CensusDataDAO[] sortedStateCensusData = JsonConvert.DeserializeObject<CensusDataDAO[]>(sortedData);
            Assert.AreEqual("District of Columbia", sortedStateCensusData[0].state);
        }

        [Test]
        public void givenUSCensusData_WhenSortedOnTotalArea_ShouldReturnSortedResult()
        {
            Dictionary<string, CensusDataDAO> censusData = censusAnalyser.loadCensusData(Country.US, usCSVFilePath);
            string sortedData = censusAnalyser.getStateWiseSortedCensusData(censusData, SortFeild.SortBy.TOTAL_AREA);
            CensusDataDAO[] sortedStateCensusData = JsonConvert.DeserializeObject<CensusDataDAO[]>(sortedData);
            Assert.AreEqual("Alaska", sortedStateCensusData[0].state);
        }

        [Test]
        public void givenUSAndIndiaCensusData_WhenSortedOnPopulationDensity_ShouldReturnSortedResult()
        {
            Dictionary<string, CensusDataDAO> indianCensusData = censusAnalyser.loadCensusData(Country.INDIA, csvFilePath, csvStateCodeFilePath);
            string indianSortedData = censusAnalyser.getStateWiseSortedCensusData(indianCensusData, SortFeild.SortBy.POPULATION_DENSITY);
            CensusDataDAO[] sortedIndianStateCensusData = JsonConvert.DeserializeObject<CensusDataDAO[]>(indianSortedData);
            Dictionary<string, CensusDataDAO> usCensusData = censusAnalyser.loadCensusData(Country.US, usCSVFilePath);
            string usSortedData = censusAnalyser.getStateWiseSortedCensusData(usCensusData, SortFeild.SortBy.POPULATION_DENSITY);
            CensusDataDAO[] sortedUSStateCensusData = JsonConvert.DeserializeObject<CensusDataDAO[]>(usSortedData);
            Assert.AreEqual(false, sortedIndianStateCensusData[0].density.CompareTo(sortedUSStateCensusData[0].density) > 2);
        }

        [Test]
        public void givenUSAndIndiaCensusData_WhenSortedOnPopulation_ShouldReturnSortedResult()
        {
            Dictionary<string, CensusDataDAO> indianCensusData = censusAnalyser.loadCensusData(Country.INDIA, csvFilePath, csvStateCodeFilePath);
            string indianSortedData = censusAnalyser.getStateWiseSortedCensusData(indianCensusData, SortFeild.SortBy.POPULATION);
            CensusDataDAO[] sortedIndianStateCensusData = JsonConvert.DeserializeObject<CensusDataDAO[]>(indianSortedData);
            Dictionary<string, CensusDataDAO> usCensusData = censusAnalyser.loadCensusData(Country.US, usCSVFilePath);
            string usSortedData = censusAnalyser.getStateWiseSortedCensusData(usCensusData, SortFeild.SortBy.POPULATION);
            CensusDataDAO[] sortedUSStateCensusData = JsonConvert.DeserializeObject<CensusDataDAO[]>(usSortedData);
            Assert.AreEqual(false, sortedIndianStateCensusData[0].population.CompareTo(sortedUSStateCensusData[0].population) > 2);
        }

    }
}