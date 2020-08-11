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

        public CensusDataDAO(string state, string population, string area, string density)
        {
            this.state = state;
            this.population = Convert.ToUInt32(population);
            this.area = Convert.ToUInt32(area);
            this.density = Convert.ToUInt32(density);
        }

        public CensusDataDAO(int serialNo, string stateName, int tin, string stateCode)
        {
            this.serialNo = serialNo;
            this.state = stateName;
            this.tin = tin;
            this.stateCode = stateCode;
        }
    }
}
