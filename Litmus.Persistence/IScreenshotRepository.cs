using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Litmus.Domain;

using MySql;
using MySql.Data;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using Dapper;

namespace Litmus.Persistence
{
    public interface IScreenshotRepository
    {
        Screenshot Find(string url);
        List<Screenshot> GetAll();
        int Add(IScreenshot screenshot);
        int Update(IScreenshot screenshot);
        int Remove(string url);
        int RemoveUrlBy(string pattern);
    }
}
