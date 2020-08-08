using CensusAnalyserProblem;
using NUnit.Framework;

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
        CensusAnalyser censusAnalyser;

        [SetUp]
        public void Setup()
        {
            censusAnalyser = new CensusAnalyser();
        }

        [Test]
        public void givenIndianCensusCSVFileReturnsCorrectRecords()
        {
            string[] numberOfEntries = censusAnalyser.loadIndiaCensusData(csvFilePath);
            Assert.AreEqual(29, numberOfEntries.Length);
        }

        [Test]
        public void givenIndiaCensusData_WhenWrongFile_ShouldThrowException()
        {
            try
            {
                censusAnalyser.loadIndiaCensusData(invalidCSVFilePath);
            }
            catch (CensusAnalyserException e)
            {
                Assert.AreEqual(CensusAnalyserException.ExceptionType.FILE_NOT_FOUND, e.type);
            }
        }

        [Test]
        public void givenIndiaCensusData_WhenIncorrectFileType_ShouldThrowException()
        {
            try
            {
                censusAnalyser.loadIndiaCensusData(incorrectFileType);
            }
            catch (CensusAnalyserException e)
            {
                Assert.AreEqual(CensusAnalyserException.ExceptionType.INCORRECT_FILE_TYPE, e.type);
            }
        }

        [Test]
        public void givenIndiaCensusData_WhenFileContainInvalidDelimeter_ShouldThrowException()
        {
            try
            {
                censusAnalyser.loadIndiaCensusData(csvFilePathWithInvalidDelimeter);
            }
            catch (CensusAnalyserException e)
            {
                Assert.AreEqual(CensusAnalyserException.ExceptionType.FILE_CONTAIN_INVALID_DELIMITER, e.type);
            }
        }

        [Test]
        public void givenIndianCensusCSVFile_WhenIncorrectHeadersInFile_ShouldThrowException()
        {
            try
            {
                censusAnalyser.loadIndiaCensusData(csvFilePathWithIncorrectHeader);
            }
            catch (CensusAnalyserException e)
            {
                Assert.AreEqual(CensusAnalyserException.ExceptionType.INVALID_HEADERS, e.type);
            }
        }

        [Test]
        public void givenStateCensusCSVFile_WhenCorrectFile_ShouldReturnCorrectNoOfRecords()
        {
            string[] noOfEntries = censusAnalyser.loadStateCensusData(csvStateCodeFilePath);
            Assert.AreEqual(37, noOfEntries.Length);
        }

        [Test]
        public void givenStateCodesCSVFile_WhenFileNotFound_ShouldThrowException()
        {
            try
            {
                censusAnalyser.loadStateCensusData(invalidCSVFilePath);
            }
            catch (CensusAnalyserException ex)
            {
                Assert.AreEqual(CensusAnalyserException.ExceptionType.FILE_NOT_FOUND, ex.type);
            }
        }

        [Test]
        public void givenStateCodesCSVFile_WhenIncorrectFileType_ShouldThrowException()
        {
            try
            {
                censusAnalyser.loadStateCensusData(incorrectFileType);
            }
            catch (CensusAnalyserException e)
            {
                Assert.AreEqual(CensusAnalyserException.ExceptionType.INCORRECT_FILE_TYPE, e.type);
            }
        }

        [Test]
        public void givenStateCodesCSVFile_WhenIncorrectDelimeterInFile_ShouldThrowException()
        {
            try
            {
                censusAnalyser.loadStateCensusData(csvStateCodeFilePathWithInvalidDelimeter);
            }
            catch (CensusAnalyserException e)
            {
                Assert.AreEqual(CensusAnalyserException.ExceptionType.FILE_CONTAIN_INVALID_DELIMITER, e.type);
            }
        }

        [Test]
        public void givenStateCodesCSVFile_WhenIncorrectHeadersInFile_ShouldThrowException()
        {
            try
            {
                censusAnalyser.loadStateCensusData(csvFilePathWithIncorrectHeader);
            }
            catch (CensusAnalyserException e)
            {
                Assert.AreEqual(CensusAnalyserException.ExceptionType.INVALID_HEADERS, e.type);
            }
        }

    }
}