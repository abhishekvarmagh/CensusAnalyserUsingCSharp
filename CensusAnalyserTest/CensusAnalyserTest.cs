using CensusAnalyserProblem;
using NUnit.Framework;
using static CensusAnalyserProblem.CensusAnalyser;
using System.Collections.Generic;
using Newtonsoft.Json;

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
        CSVFactory csvFactory;
        CSVData csvData;
        List<string> numberOfEntries;

        [SetUp]
        public void Setup()
        {
            csvFactory = new CSVFactory();
            numberOfEntries = new List<string>();
        }

        [Test]
        public void givenIndianCensusCSVFileReturnsCorrectRecords()
        {
            CensusAnalyser censusAnalyser = (CensusAnalyser)csvFactory.getCensusAnalyser();
            csvData = new CSVData(censusAnalyser.loadIndiaCensusData);
            numberOfEntries = (List<string>)censusAnalyser.loadIndiaCensusData(csvFilePath, stateCensusFileHeader);
            Assert.AreEqual(29, numberOfEntries.Count);
        }

        [Test]
        public void givenIndiaCensusData_WhenWrongFile_ShouldThrowException()
        {
            CensusAnalyser censusAnalyser = (CensusAnalyser)csvFactory.getCensusAnalyser();
            csvData = new CSVData(censusAnalyser.loadIndiaCensusData);
            var exception = Assert.Throws<CensusAnalyserException>(() => csvData(invalidCSVFilePath, stateCensusFileHeader));            
            Assert.AreEqual(CensusAnalyserException.ExceptionType.FILE_NOT_FOUND, exception.type);
        }

        [Test]
        public void givenIndiaCensusData_WhenIncorrectFileType_ShouldThrowException()
        {
            CensusAnalyser censusAnalyser = (CensusAnalyser)csvFactory.getCensusAnalyser();
            csvData = new CSVData(censusAnalyser.loadIndiaCensusData);
            var exception = Assert.Throws<CensusAnalyserException>(() => csvData(incorrectFileType, stateCensusFileHeader));
            Assert.AreEqual(CensusAnalyserException.ExceptionType.INCORRECT_FILE_TYPE, exception.type);
        }

        [Test]
        public void givenIndiaCensusData_WhenFileContainInvalidDelimeter_ShouldThrowException()
        {
            CensusAnalyser censusAnalyser = (CensusAnalyser)csvFactory.getCensusAnalyser();
            csvData = new CSVData(censusAnalyser.loadIndiaCensusData);
            var exception = Assert.Throws<CensusAnalyserException>(() => csvData(csvFilePathWithInvalidDelimeter, stateCensusFileHeader));
            Assert.AreEqual(CensusAnalyserException.ExceptionType.FILE_CONTAIN_INVALID_DELIMITER, exception.type);
        }

        [Test]
        public void givenIndianCensusCSVFile_WhenIncorrectHeadersInFile_ShouldThrowException()
        {
            CensusAnalyser censusAnalyser = (CensusAnalyser)csvFactory.getCensusAnalyser();
            csvData = new CSVData(censusAnalyser.loadIndiaCensusData);
            var exception = Assert.Throws<CensusAnalyserException>(() => csvData(csvFilePathWithIncorrectHeader, stateCensusFileHeader));
            Assert.AreEqual(CensusAnalyserException.ExceptionType.INVALID_HEADERS, exception.type);
        }

        [Test]
        public void givenStateCodeData_WhenCorrectFile_ShouldReturnCorrectNoOfRecords()
        {
            CensusAnalyser censusAnalyser = (CensusAnalyser)csvFactory.getCensusAnalyser();
            csvData = new CSVData(censusAnalyser.loadIndiaCensusData);
            numberOfEntries = (List<string>)censusAnalyser.loadIndiaCensusData(csvStateCodeFilePath, stateCodeFileHeader);
            Assert.AreEqual(37, numberOfEntries.Count);
        }

        [Test]
        public void givenStateCodeData_WhenFileNotFound_ShouldThrowException()
        {
            CensusAnalyser censusAnalyser = (CensusAnalyser)csvFactory.getCensusAnalyser();
            csvData = new CSVData(censusAnalyser.loadIndiaCensusData);
            var exception = Assert.Throws<CensusAnalyserException>(() => csvData(invalidCSVFilePath, stateCodeFileHeader));
            Assert.AreEqual(CensusAnalyserException.ExceptionType.FILE_NOT_FOUND, exception.type);
        }

        [Test]
        public void givenStateCodeData_WhenIncorrectFileType_ShouldThrowException()
        {
            CensusAnalyser censusAnalyser = (CensusAnalyser)csvFactory.getCensusAnalyser();
            csvData = new CSVData(censusAnalyser.loadIndiaCensusData);
            var exception = Assert.Throws<CensusAnalyserException>(() => csvData(incorrectFileType, stateCodeFileHeader));
            Assert.AreEqual(CensusAnalyserException.ExceptionType.INCORRECT_FILE_TYPE, exception.type);
        }

        [Test]
        public void givenStateCodeData_WhenIncorrectDelimeterInFile_ShouldThrowException()
        {
            CensusAnalyser censusAnalyser = (CensusAnalyser)csvFactory.getCensusAnalyser();
            csvData = new CSVData(censusAnalyser.loadIndiaCensusData);
            var exception = Assert.Throws<CensusAnalyserException>(() => csvData(csvStateCodeFilePathWithInvalidDelimeter, stateCodeFileHeader));
            Assert.AreEqual(CensusAnalyserException.ExceptionType.FILE_CONTAIN_INVALID_DELIMITER, exception.type);
        }

        [Test]
        public void givenStateCodeData_WhenIncorrectHeadersInFile_ShouldThrowException()
        {
            CensusAnalyser censusAnalyser = (CensusAnalyser)csvFactory.getCensusAnalyser();
            csvData = new CSVData(censusAnalyser.loadIndiaCensusData);
            var exception = Assert.Throws<CensusAnalyserException>(() => csvData(csvFilePathWithIncorrectHeader, stateCodeFileHeader));
            Assert.AreEqual(CensusAnalyserException.ExceptionType.INVALID_HEADERS, exception.type);
        }

        [Test]
        public void GivenIndianCensusCSVFile_WhenProper_ShouldReturnSortedDataInJSONFormat()
        {
            CensusAnalyser censusAnalyser = new CensusAnalyser();
            string sortedData = censusAnalyser.getStateWiseSortedCensusData(csvFilePath).ToString();
            string[] sortedCensusData = JsonConvert.DeserializeObject<string[]>(sortedData);
            Assert.AreEqual("Andhra Pradesh,49386799,162968,303", sortedCensusData[0]);
        }

    }
}