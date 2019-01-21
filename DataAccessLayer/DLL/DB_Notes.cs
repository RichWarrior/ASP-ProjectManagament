using DataAccessLayer.AdoModel;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccessLayer.DLL
{
    public class DB_Notes : Base
    {
        private ResultModelList<Notes> List(string user_id,string args)
        {
            var result = new ResultModelList<Notes>();
            try
            {
                if (args == "Hepsi")
                {
                    result.resultItem = _dbContext.Notes.Where(x => x.User_Id == user_id).OrderByDescending(x => x.CreatedDate)
                    .ToList();
                }
                else
                {
                    result.resultItem = _dbContext.Notes.Where(x => x.User_Id == user_id).OrderByDescending(x => x.CreatedDate)
                        .Take(Convert.ToInt32(args))
                    .ToList();
                }                
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                result.exceptionMessage = ex.Message;
            }
            return result;
        }
        public ResultModelList<Notes> ListSync(string user_id,string args)
        {
            return this.List(user_id,args);
        }
        public async Task<ResultModelList<Notes>>ListAsync(string user_id,string args)
        {
            return await Task.Run(() => this.List(user_id, args));
        }
        public async Task<ResultModel> Add(Notes model)
        {
            var result = new ResultModel();
            try
            {
                _dbContext.Notes.Add(model);
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
        public async Task<ResultModel> Delete(string user_id,string id)
        {
            var result = new ResultModel();
            try
            {
                var value = _dbContext.Notes.FirstOrDefault(x => x.User_Id == user_id && x.Id == id);
                if (value != null)
                {
                    _dbContext.Notes.Remove(value);
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
        public async Task<ResultModel> Edit(string user_id,string id,Notes newModel)
        {
            var result = new ResultModel();
            try
            {
                var value = _dbContext.Notes.FirstOrDefault(x => x.User_Id == user_id && x.Id == id);
                if (value != null)
                {
                    value.Title = newModel.Title;
                    value.Description = newModel.Description;
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
        private ResultModelSingle<Notes> Find(string user_id,string id)
        {
            var result = new ResultModelSingle<Notes>();
            try
            {
                result.resultItem =  _dbContext.Notes.FirstOrDefault(x => x.Id == id && x.User_Id == user_id);               
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                result.exceptionMessage = ex.Message;
            }
            return result;
        }
        public ResultModelSingle<Notes>FindBySync(string user_id,string id)
        {
            return this.Find(user_id,id);
        }
        public async Task<ResultModelSingle<Notes>> FindByAsync(string user_id,string id)
        {
            return await Task.Run(() => this.Find(user_id, id));
        }
    }
}
