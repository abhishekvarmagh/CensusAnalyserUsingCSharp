using CensusAnalyserProblem;
using NUnit.Framework;

namespace CensusAnalyserTest
{
    public class Tests
    {
        private string csvFilePath = @"C:\Users\abhishek verma\source\repos\CensusAnalyserTest\CsvFile\IndiaStateCensusData.csv";
        private string invalidCSVFilePath = @"C:\Users\abhishek verma\source\CensusAnalyserTest\CsvFile\IndiaStateCensusData.csv";
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
        public void givenIndiaCensusData_WithWrongFile_ShouldThrowException()
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
    }
}