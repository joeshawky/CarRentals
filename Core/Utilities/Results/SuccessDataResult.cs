using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{
    public class SuccessDataResult<T> : DataResult<T> where T : class, new()
    {
        public SuccessDataResult()
            :base(null, true, string.Empty)
        {

        }
        public SuccessDataResult(string message)
            :base(null, true, message)
        {

        }
        public SuccessDataResult(T data)
            : base(data, true)
        {

        }

        public SuccessDataResult(T data, string message)
            : base(data, true, message)
        {

        }
    }
}
