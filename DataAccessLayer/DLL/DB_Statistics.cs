using DataAccessLayer.AdoModel;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccessLayer.DLL
{
    public class DB_Statistics : Base
    {
        private ResultModelSingle<SP_STATISTIC_Result> List()
        {
            var result = new ResultModelSingle<SP_STATISTIC_Result>();
            try
            {
                result.resultItem = _dbContext.SP_STATISTIC().FirstOrDefault();
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                result.exceptionMessage = ex.Message;
            }
            return result;
        }
        public ResultModelSingle<SP_STATISTIC_Result> ListSync()
        {
            return this.List();
        }
        public async Task<ResultModelSingle<SP_STATISTIC_Result>> ListAsync()
        {
            return await Task.Run(() => this.List());
        }
    }
}
