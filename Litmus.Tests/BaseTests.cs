using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;
using Litmus.Domain;
using System.Configuration;

namespace Litmus.Tests
{
    public class BaseTests
    {
        public Configuration config = ConfigurationManager.OpenMappedExeConfiguration(new ExeConfigurationFileMap() { ExeConfigFilename = "App.config" }, ConfigurationUserLevel.None);

        public BaseTests()
        {

        }
    }
}