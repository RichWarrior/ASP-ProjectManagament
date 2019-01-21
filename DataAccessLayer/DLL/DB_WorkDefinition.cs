using DataAccessLayer.AdoModel;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccessLayer.DLL
{
    public class DB_WorkDefinition : Base
    {
        private ResultModelList<WorkDefinition> List(string user_id, string args)
        {
            var result = new ResultModelList<WorkDefinition>();
            try
            {
                if (args == "Hepsi")
                {
                    result.resultItem = _dbContext.WorkDefinition
                    .Include("Projects").Where(x => x.User_Id == user_id).OrderByDescending(x => x.Rank).ToList();
                }
                else
                {
                    result.resultItem = _dbContext.WorkDefinition
                    .Include("Projects").Where(x => x.User_Id == user_id).OrderByDescending(x => x.Rank)
                    .Take(Convert.ToInt32(args)).ToList();
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                result.exceptionMessage = ex.Message;
            }
            return result;
        }
        public ResultModelList<WorkDefinition> ListSync(string user_id, string args)
        {
            return this.List(user_id, args);
        }
        public async Task<ResultModelList<WorkDefinition>> ListAsync(string user_id, string args)
        {
            return await Task.Run(() => this.List(user_id, args));
        }
        private ResultModelList<WorkDefinition> List(string user_id, string projects_id, string args)
        {
            var result = new ResultModelList<WorkDefinition>();
            try
            {
                if (args == "Hepsi")
                {
                    result.resultItem = _dbContext.WorkDefinition
                    .Include("Projects").Where(x => x.User_Id == user_id && x.Project_Id == projects_id).OrderByDescending(x => x.Rank).ToList();
                }
                else
                {
                    result.resultItem = _dbContext.WorkDefinition
                    .Include("Projects").Where(x => x.User_Id == user_id && x.Project_Id == projects_id).OrderByDescending(x => x.Rank)
                    .Take(Convert.ToInt32(args)).ToList();
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                result.exceptionMessage = ex.Message;
            }
            return result;
        }
        public ResultModelList<WorkDefinition> ListSync(string user_id, string project_id, string args)
        {
            return this.List(user_id, project_id, args);
        }
        public async Task<ResultModelList<WorkDefinition>> ListAsync(string user_id, string project_id, string args)
        {
            return await Task.Run(() => this.List(user_id, project_id, args));
        }
        private ResultModelSingle<WorkDefinition> FindById(string user_id, string project_id)
        {
            var result = new ResultModelSingle<WorkDefinition>();
            try
            {
                result.resultItem = _dbContext.WorkDefinition.Include("Projects").FirstOrDefault(x => x.User_Id == user_id && x.Id == project_id);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                result.exceptionMessage = ex.Message;
            }
            return result;
        }
        public ResultModelSingle<WorkDefinition> FindByIdSync(string user_id, string project_id)
        {
            return this.FindById(user_id, project_id);
        }
        public async Task<ResultModelSingle<WorkDefinition>> FindByIdAsync(string user_id, string project_id)
        {
            return await Task.Run(() => this.FindById(user_id, project_id));
        }
        public async Task<ResultModel> Add(WorkDefinition model)
        {
            var result = new ResultModel();
            try
            {
                _dbContext.WorkDefinition.Add(model);
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
        public async Task<ResultModel> Edit(string id, string user_id, WorkDefinition model)
        {
            var result = new ResultModel();
            try
            {
                var value = _dbContext.WorkDefinition.Include("Projects").FirstOrDefault(x => x.Id == id && x.User_Id == user_id);
                if (value != null)
                {
                    value.Title = model.Title;
                    value.Description = model.Description;
                    value.ExpireDate = model.ExpireDate;
                    value.Project_Id = model.Project_Id;
                    value.IsCompleted = model.IsCompleted;
                    value.Rank = model.Rank;
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
        public async Task<ResultModel> Delete(string id, string user_id)
        {
            var result = new ResultModel();
            try
            {
                var value = _dbContext.WorkDefinition.FirstOrDefault(x => x.Id == id && x.User_Id == user_id);
                if (value != null)
                {
                    _dbContext.WorkDefinition.Remove(value);
                    await _dbContext.SaveChangesAsync();
                    result.success = true;
                }
            }
            catch (Exception)
            {
            }
            return result;
        }
        private ResultModelList<WorkDefinition> ListInCompletedAll(DateTime now)
        {
            var result = new ResultModelList<WorkDefinition>();
            try
            {
                result.resultItem = _dbContext.WorkDefinition.Include("Projects").Include("AspNetUsers").Where(x => x.IsCompleted == false && x.ExpireDate < now)
                    .ToList();
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                result.exceptionMessage = ex.Message;
            }
            return result;
        }
        public ResultModelList<WorkDefinition> ListInCompletedAllSync(DateTime now)
        {
            return this.ListInCompletedAll(now);
        }
        public async Task<ResultModelList<WorkDefinition>> ListInCompletedAllAsync(DateTime now)
        {
            return await Task.Run(() => this.ListInCompletedAll(now));
        }
        #region Trash
        //private ResultModelList<WorkDefinition> Filter(string user_id,string project_id,int count)
        //{
        //    var result = new ResultModelList<WorkDefinition>();
        //    try
        //    {
        //        result.resultItem = _dbContext.WorkDefinition.Include("Projects").Where(x => x.User_Id == user_id && x.Project_Id == project_id)
        //            .Take(count).OrderByDescending(x => x.CreatedDate).ToList();
        //    }
        //    catch (Exception ex)
        //    {
        //        result.exceptionMessage = ex.Message;
        //        Log.Error(ex.Message);
        //    }
        //    return result;
        //}
        //public ResultModelList<WorkDefinition> FilterSync(string user_id,string project_id,int count)
        //{
        //    return this.Filter(user_id, project_id, count);
        //}
        //public async Task<ResultModelList<WorkDefinition>>FilterAsync(string user_id,string project_id,int count)
        //{
        //    return await Task.Run(() => this.Filter(user_id, project_id, count));
        //}
        #endregion
    }
}
