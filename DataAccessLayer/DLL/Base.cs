using DataAccessLayer.AdoModel;
using log4net;
using System;

namespace DataAccessLayer.DLL
{
    /// <summary>
    /// Constructuor
    /// </summary>
    public class Base : IDisposable
    {
        public PM _dbContext { get; set; }
        public static readonly ILog Log =
              LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public Base()
        {
            if (_dbContext == null)
                _dbContext = new PM();
            _dbContext.Configuration.LazyLoadingEnabled = false;
        }

        public void Dispose()
        {
            if(_dbContext!=null)
            {
                _dbContext.Dispose();
                _dbContext = null;
            }
        }
    }
}
