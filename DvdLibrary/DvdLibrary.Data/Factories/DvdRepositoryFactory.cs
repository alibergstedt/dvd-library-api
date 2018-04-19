using DvdLibrary.Data.ADO;
using DvdLibrary.Data.EF;
using DvdLibrary.Data.Interfaces;
using DvdLibrary.Data.SampleData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DvdLibrary.Data.Factories
{
    public class DvdRepositoryFactory
    {
        public static IDvdRepository GetRepository()
        {
            switch (Settings.GetRepositoryType())
            {
                case "ADO":
                    return new DvdRepositoryADO();
                case "EntityFramework":
                    return new DvdRepositoryEF();
                case "SampleData":
                    return new DvdRepositoryMock();
                default:
                    throw new Exception("Could not find valid RepositoryType configuration value.");
            }
        }
    }
}
