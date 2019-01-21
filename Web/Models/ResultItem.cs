using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class ResultItemList<T>
    {
        public string exceptionMessage { get; set; }
        public List<T> data { get; set; }

        public ResultItemList()
        {
            data = new List<T>();
        }
    }
    public class ResultItemSingle<T>
    {
        public string exceptionMessage { get; set; }
        public T data { get; set; }
    }
}