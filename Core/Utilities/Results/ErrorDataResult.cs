using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{
    public class ErrorDataResult<T> : DataResult<T> where T : class, new()
    {

        public ErrorDataResult(string message)
            : base(null, false, message)
        {

        }

        public ErrorDataResult()
            : base(null, false)
        {

        }
        public ErrorDataResult(T data)
            : base(data, false)
        {

        }

        public ErrorDataResult(T data, string message)
            : base(data, false, message)
        {

        }
    }
}
