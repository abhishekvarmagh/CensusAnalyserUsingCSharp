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
        private string stateCensusFileHeader = "State,Population,AreaInSqKm,DensityPerSqKm";
        private string stateCodeFileHeader = "SrNo,State Name,TIN,StateCode";
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
            numberOfEntries = (Dictionary<string, CensusDataDAO>)censusAnalyser.loadIndiaCensusData(csvFilePath, stateCensusFileHeader);
            Assert.AreEqual(29, numberOfEntries.Count);
        }

        [Test]
        public void givenIndiaCensusData_WhenWrongFile_ShouldThrowException()
        {
            var exception = Assert.Throws<CensusAnalyserException>(() => censusAnalyser.loadIndiaCensusData(invalidCSVFilePath, stateCensusFileHeader));            
            Assert.AreEqual(CensusAnalyserException.ExceptionType.FILE_NOT_FOUND, exception.type);
        }

        [Test]
        public void givenIndiaCensusData_WhenIncorrectFileType_ShouldThrowException()
        {
            var exception = Assert.Throws<CensusAnalyserException>(() => censusAnalyser.loadIndiaCensusData(incorrectFileType, stateCensusFileHeader));
            Assert.AreEqual(CensusAnalyserException.ExceptionType.INCORRECT_FILE_TYPE, exception.type);
        }

        [Test]
        public void givenIndiaCensusData_WhenFileContainInvalidDelimeter_ShouldThrowException()
        {
            var exception = Assert.Throws<CensusAnalyserException>(() => censusAnalyser.loadIndiaCensusData(csvFilePathWithInvalidDelimeter, stateCensusFileHeader));
            Assert.AreEqual(CensusAnalyserException.ExceptionType.FILE_CONTAIN_INVALID_DELIMITER, exception.type);
        }

        [Test]
        public void givenIndianCensusCSVFile_WhenIncorrectHeadersInFile_ShouldThrowException()
        {
            var exception = Assert.Throws<CensusAnalyserException>(() => censusAnalyser.loadIndiaCensusData(csvFilePathWithIncorrectHeader, stateCensusFileHeader));
            Assert.AreEqual(CensusAnalyserException.ExceptionType.INVALID_HEADERS, exception.type);
        }

        [Test]
        public void givenStateCodeData_WhenCorrectFile_ShouldReturnCorrectNoOfRecords()
        {
            numberOfEntries = (Dictionary<string, CensusDataDAO>)censusAnalyser.loadIndiaCensusData(csvStateCodeFilePath, stateCodeFileHeader);
            Assert.AreEqual(37, numberOfEntries.Count);
        }

        [Test]
        public void givenStateCodeData_WhenFileNotFound_ShouldThrowException()
        {
            var exception = Assert.Throws<CensusAnalyserException>(() => censusAnalyser.loadIndiaCensusData(invalidCSVFilePath, stateCodeFileHeader));
            Assert.AreEqual(CensusAnalyserException.ExceptionType.FILE_NOT_FOUND, exception.type);
        }

        [Test]
        public void givenStateCodeData_WhenIncorrectFileType_ShouldThrowException()
        {
            var exception = Assert.Throws<CensusAnalyserException>(() => censusAnalyser.loadIndiaCensusData(incorrectFileType, stateCodeFileHeader));
            Assert.AreEqual(CensusAnalyserException.ExceptionType.INCORRECT_FILE_TYPE, exception.type);
        }

        [Test]
        public void givenStateCodeData_WhenIncorrectDelimeterInFile_ShouldThrowException()
        {
            var exception = Assert.Throws<CensusAnalyserException>(() => censusAnalyser.loadIndiaCensusData(csvStateCodeFilePathWithInvalidDelimeter, stateCodeFileHeader));
            Assert.AreEqual(CensusAnalyserException.ExceptionType.FILE_CONTAIN_INVALID_DELIMITER, exception.type);
        }

        [Test]
        public void givenStateCodeData_WhenIncorrectHeadersInFile_ShouldThrowException()
        {
            var exception = Assert.Throws<CensusAnalyserException>(() => censusAnalyser.loadIndiaCensusData(csvFilePathWithIncorrectHeader, stateCodeFileHeader));
            Assert.AreEqual(CensusAnalyserException.ExceptionType.INVALID_HEADERS, exception.type);
        }

        [Test]
        public void givenIndianStateCensusData_WhenSortedOnState_ShouldReturnSortedResult()
        {
            string sortedData = censusAnalyser.getStateWiseSortedCensusData(csvFilePath, stateCensusFileHeader, "stateName");
            CensusDataDAO[] sortedCensusData = JsonConvert.DeserializeObject<CensusDataDAO[]>(sortedData);
            Assert.AreEqual("Andhra Pradesh", sortedCensusData[0].state);
        }

        [Test]
        public void givenIndianStateCSVFile_WhenProper_ShouldReturnSortedDataAccordingToStateCodeInJSONFormats()
        {
            string sortedData = censusAnalyser.getStateWiseSortedCensusData(csvStateCodeFilePath, stateCodeFileHeader, "stateCode");
            CensusDataDAO[] sortedStateCensusData = JsonConvert.DeserializeObject<CensusDataDAO[]>(sortedData);
            Assert.AreEqual("AD", sortedStateCensusData[0].stateCode);
        }

        [Test]
        public void givenIndianCensusData_WhenSortedOnPopulation_ShouldReturnSortedResult()
        {
            string sortedData = censusAnalyser.getStateWiseSortedCensusData(csvFilePath, stateCensusFileHeader, "population");
            CensusDataDAO[] sortedStateCensusData = JsonConvert.DeserializeObject<CensusDataDAO[]>(sortedData);
            Assert.AreEqual(199812341, sortedStateCensusData[0].population);
        }
    }
}