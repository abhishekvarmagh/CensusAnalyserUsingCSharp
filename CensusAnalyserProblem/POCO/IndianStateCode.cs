using System;
using System.Collections.Generic;
using System.Text;

namespace CensusAnalyserProblem.POCO
{
    public class IndianStateCode
    {
        public int serialNo;
        public string state;
        public int tin;
        public string stateCode;

        public IndianStateCode(int serialNo, string state, int tin, string stateCode)
        {
            this.serialNo = serialNo;
            this.state = state;
            this.tin = tin;
            this.stateCode = stateCode;
        }
    }
}
