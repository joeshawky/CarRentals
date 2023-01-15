using Core.Entities;
using Core.Entities.Abstract;
using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Business.Abstract
{
    public interface IServiceBase<T> where T : class, IEntity, new()
    {
        IResult Update(T entity);
        IResult Delete(T entity);


    }
}
