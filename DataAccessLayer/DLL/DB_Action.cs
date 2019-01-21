using DataAccessLayer.AdoModel;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccessLayer.DLL
{
    public class DB_Action : Base
    {
        private ResultModelList<SP_Action_Result> List()
        {
            var result = new ResultModelList<SP_Action_Result>();
            try
            {
                result.resultItem = _dbContext.SP_Action()
                    .OrderBy(x=>x.Email).ToList();
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                result.exceptionMessage = ex.Message;
            }
            return result;
        }
        public ResultModelList<SP_Action_Result> ListSync()
        {
            return this.List();
        }
        public async Task<ResultModelList<SP_Action_Result>> ListAsync()
        {
            return await Task.Run(() => this.List());
        }
        public async Task<ResultModel> Add(AdoModel.Action model)
        {
            var result = new ResultModel();
            try
            {
                _dbContext.Action.Add(model);
                await _dbContext.SaveChangesAsync();
                result.success = true;
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                result.exceptionMessage = ex.Message;                
            }
            return result;
        }
        public async Task<ResultModel> DeleteCompletedAction()
        {
            var result = new ResultModel();
            try
            {
                var values = _dbContext.Action.Where(x => x.Completed == true).ToList();
                if (values.Count > 0)
                {
                    _dbContext.Action.RemoveRange(values);
                    await _dbContext.SaveChangesAsync();
                    result.success = true;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                result.exceptionMessage = ex.Message;
            }
            return result;
        }
    }
}
