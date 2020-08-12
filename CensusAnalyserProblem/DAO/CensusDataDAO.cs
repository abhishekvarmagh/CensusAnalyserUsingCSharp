using CensusAnalyserProblem.POCO;
using System;
using System.Collections.Generic;
using System.Text;

namespace CensusAnalyserProblem.DAO
{
    public class CensusDataDAO
    {
        public string state;
        public long population;
        public long area;
        public long density;
        public int serialNo;
        public int tin;
        public string stateCode;

        public CensusDataDAO() { }

        public CensusDataDAO(IndianCensus indianCensus)
        {
            this.state = indianCensus.state;
            this.population = indianCensus.population;
            this.area = indianCensus.area;
            this.density = indianCensus.density;
        }

        public CensusDataDAO(IndianStateCode indianStateCode)
        {
            this.serialNo = indianStateCode.serialNo;
            this.state = indianStateCode.state;
            this.tin = indianStateCode.tin;
            this.stateCode = indianStateCode.stateCode;
        }

        public CensusDataDAO(USCensus usCensus)
        {
            this.state = usCensus.state;
            this.population = usCensus.population;
            this.area = (long)usCensus.totalArea;
            this.density = (long)usCensus.density;
        }
    }
}
