using SmartTools_CS.Db;
using SmartTools_CS.Models;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTools.Net.Services
{
    class CodeLessService
    {
        #region initialize
        private readonly SqlSugarClient _db;
        public CodeLessService(string connectString)
        {
            _db = DbUtil.GetInstance(connectString);
        }
        #endregion

        #region GetDatabase
        public List<DataBaseInfo> GetDatabase()
        {
            return _db.Queryable<DataBaseInfo>().AS("MASTER.sys.SYSDATABASES").Where(x => x.dbid > 4).OrderBy(x => x.name).ToList();
        }
        #endregion

        #region GetDbTable
        public List<DbTable> GetDbTable(string database)
        {
            return _db.Queryable<DbTable>().AS($"{database}.sys.tables").OrderBy(x => x.name).ToList();
        }
        #endregion
    }
}
