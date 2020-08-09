using CensusAnalyserProblem;
using NUnit.Framework;
using static CensusAnalyserProblem.CensusAnalyser;

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
        CSVData csvData;

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void givenIndianCensusCSVFileReturnsCorrectRecords()
        {
            censusAnalyser = new CensusAnalyser(csvFilePath, stateCensusFileHeader);
            csvData = new CSVData(censusAnalyser.loadIndiaCensusData);
            string[] numberOfEntries = (string[])censusAnalyser.loadIndiaCensusData();
            Assert.AreEqual(29, numberOfEntries.Length);
        }

        [Test]
        public void givenIndiaCensusData_WhenWrongFile_ShouldThrowException()
        {
            censusAnalyser = new CensusAnalyser(invalidCSVFilePath, stateCensusFileHeader);
            csvData = new CSVData(censusAnalyser.loadIndiaCensusData);
            var exception = Assert.Throws<CensusAnalyserException>(() => csvData());            
            Assert.AreEqual(CensusAnalyserException.ExceptionType.FILE_NOT_FOUND, exception.type);
        }

        [Test]
        public void givenIndiaCensusData_WhenIncorrectFileType_ShouldThrowException()
        {
            censusAnalyser = new CensusAnalyser(incorrectFileType, stateCensusFileHeader);
            csvData = new CSVData(censusAnalyser.loadIndiaCensusData);
            var exception = Assert.Throws<CensusAnalyserException>(() => csvData());
            Assert.AreEqual(CensusAnalyserException.ExceptionType.INCORRECT_FILE_TYPE, exception.type);
        }

        [Test]
        public void givenIndiaCensusData_WhenFileContainInvalidDelimeter_ShouldThrowException()
        {
            censusAnalyser = new CensusAnalyser(csvFilePathWithInvalidDelimeter, stateCensusFileHeader);
            csvData = new CSVData(censusAnalyser.loadIndiaCensusData);
            var exception = Assert.Throws<CensusAnalyserException>(() => csvData());
            Assert.AreEqual(CensusAnalyserException.ExceptionType.FILE_CONTAIN_INVALID_DELIMITER, exception.type);
        }

        [Test]
        public void givenIndianCensusCSVFile_WhenIncorrectHeadersInFile_ShouldThrowException()
        {
            censusAnalyser = new CensusAnalyser(csvFilePathWithIncorrectHeader, stateCensusFileHeader);
            csvData = new CSVData(censusAnalyser.loadIndiaCensusData);
            var exception = Assert.Throws<CensusAnalyserException>(() => csvData());
            Assert.AreEqual(CensusAnalyserException.ExceptionType.INVALID_HEADERS, exception.type);
        }

        [Test]
        public void givenStateCodeData_WhenCorrectFile_ShouldReturnCorrectNoOfRecords()
        {
            censusAnalyser = new CensusAnalyser(csvStateCodeFilePath, stateCodeFileHeader);
            csvData = new CSVData(censusAnalyser.loadIndiaCensusData);
            string[] noOfEntries = (string[])censusAnalyser.loadIndiaCensusData();
            Assert.AreEqual(37, noOfEntries.Length);
        }

        [Test]
        public void givenStateCodeData_WhenFileNotFound_ShouldThrowException()
        {
            censusAnalyser = new CensusAnalyser(invalidCSVFilePath, stateCodeFileHeader);
            csvData = new CSVData(censusAnalyser.loadIndiaCensusData);
            var exception = Assert.Throws<CensusAnalyserException>(() => csvData());
            Assert.AreEqual(CensusAnalyserException.ExceptionType.FILE_NOT_FOUND, exception.type);
        }

        [Test]
        public void givenStateCodeData_WhenIncorrectFileType_ShouldThrowException()
        {
            censusAnalyser = new CensusAnalyser(incorrectFileType, stateCodeFileHeader);
            csvData = new CSVData(censusAnalyser.loadIndiaCensusData);
            var exception = Assert.Throws<CensusAnalyserException>(() => csvData());
            Assert.AreEqual(CensusAnalyserException.ExceptionType.INCORRECT_FILE_TYPE, exception.type);
        }

        [Test]
        public void givenStateCodeData_WhenIncorrectDelimeterInFile_ShouldThrowException()
        {
            censusAnalyser = new CensusAnalyser(csvStateCodeFilePathWithInvalidDelimeter, stateCodeFileHeader);
            csvData = new CSVData(censusAnalyser.loadIndiaCensusData);
            var exception = Assert.Throws<CensusAnalyserException>(() => csvData());
            Assert.AreEqual(CensusAnalyserException.ExceptionType.FILE_CONTAIN_INVALID_DELIMITER, exception.type);
        }

        [Test]
        public void givenStateCodeData_WhenIncorrectHeadersInFile_ShouldThrowException()
        {
            censusAnalyser = new CensusAnalyser(csvFilePathWithIncorrectHeader, stateCodeFileHeader);
            csvData = new CSVData(censusAnalyser.loadIndiaCensusData);
            var exception = Assert.Throws<CensusAnalyserException>(() => csvData());
            Assert.AreEqual(CensusAnalyserException.ExceptionType.INVALID_HEADERS, exception.type);
        }

    }
}