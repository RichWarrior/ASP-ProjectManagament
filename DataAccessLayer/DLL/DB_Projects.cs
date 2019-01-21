using DataAccessLayer.AdoModel;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccessLayer.DLL
{
    public class DB_Projects : Base
    {
        private ResultModelList<Projects> List(string user_id)
        {
            var result = new ResultModelList<Projects>();
            try
            {
                result.resultItem = _dbContext.Projects.Where(x=>x.User_Id==user_id).OrderBy(x => x.Name).ToList();
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                result.exceptionMessage = ex.Message;
            }
            return result;
        }
        public ResultModelList<Projects> ListSync(string user_id)
        {
            return this.List(user_id);
        }
        public async Task<ResultModelList<Projects>> ListAsync(string user_id)
        {
            return await Task.Run(() => this.List(user_id));
        }
        public async Task<ResultModel> Add(Projects model)
        {
            var result = new ResultModel();
            try
            {
                _dbContext.Projects.Add(model);
                await _dbContext.SaveChangesAsync();
                result.success = true;
            }
            catch (Exception ex)
            {
                result.exceptionMessage = ex.Message;
                Log.Error(ex);
            }
            return result;
        }
        public async Task<ResultModel> Edit(string id,string user_id,Projects newModel)
        {
            var result = new ResultModel();
            try
            {
                var value = _dbContext.Projects.FirstOrDefault(x => x.Id == id && x.User_Id == user_id);
                if (value != null)
                {
                    value.Name = newModel.Name;
                    await _dbContext.SaveChangesAsync();
                    result.success = true;
                }
            }
            catch (Exception ex)
            {
                result.exceptionMessage = ex.Message;
                Log.Error(ex);
            }
            return result;
        }
        private ResultModelSingle<Projects> FindById(string id,string user_id)
        {
            var result = new ResultModelSingle<Projects>();
            try
            {
                result.resultItem = _dbContext.Projects.FirstOrDefault(x => x.Id == id && x.User_Id == user_id);
            }
            catch (Exception ex)
            {
                result.exceptionMessage = ex.Message;
                Log.Error(ex);
            }
            return result;
        }
        public ResultModelSingle<Projects> FindByIdSync(string id,string user_id)
        {
            return this.FindById(id,user_id);
        }
        public async Task<ResultModelSingle<Projects>> FindByIdAsync(string id,string user_id)
        {
            return await Task.Run(() => this.FindById(id, user_id));
        }
        public async Task<ResultModel> Delete(string id,string user_id)
        {
            var result = new ResultModel();
            try
            {
                var value = _dbContext.Projects.FirstOrDefault(x => x.Id == id && x.User_Id == user_id);
                if (value != null)
                {
                    _dbContext.Projects.Remove(value);
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
