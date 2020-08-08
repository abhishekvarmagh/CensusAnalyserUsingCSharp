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
        private string stateCensusFileHeader = "State,Population,AreaInSqKm,DensityPerSqKm";
        private string stateCodeFileHeader = "SrNo,State Name,TIN,StateCode";
        CensusAnalyser censusAnalyser;

        [SetUp]
        public void Setup()
        {
            censusAnalyser = new CensusAnalyser();
        }

        [Test]
        public void givenIndianCensusCSVFileReturnsCorrectRecords()
        {
            string[] numberOfEntries = censusAnalyser.loadIndiaCensusData(csvFilePath, stateCensusFileHeader);
            Assert.AreEqual(29, numberOfEntries.Length);
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
            string[] noOfEntries = censusAnalyser.loadIndiaCensusData(csvStateCodeFilePath, stateCodeFileHeader);
            Assert.AreEqual(37, noOfEntries.Length);
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

    }
}