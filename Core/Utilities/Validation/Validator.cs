using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Validation
{
    public static class Validator
    {
        public static IResult Run(params IResult[] results)
        {
            foreach (var result in results)
            {
                if (result.Success is false)
                    return result;
            }

            return new SuccessResult();
        }
        //public static IDataResult<object> Run(params IDataResult<object>[] dataResults)
        //{
        //    foreach (var dataResult in dataResults)
        //    {
        //        if (dataResult.Success is false)
        //            return dataResult;
        //    }

        //    return new SuccessDataResult<object>(0);
        //}
    }

   
}
