using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Comparer;
using Core.Utilities.Results;
using Core.Utilities.Validation;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ColorManager : IColorService
    {
        private readonly IColorDal _colorDal;

        public ColorManager(IColorDal colorDal)
        {
            _colorDal = colorDal;
        }


        [ValidationAspect(typeof(ColorValidator))]
        public IResult Add(Color color)
        {
            var result = Validator.Run(ColorNameExists(color.Name));

            if (result.Success)
            {
                return new ErrorResult(Messages.BrandNameAlreadyExists);
            }

            _colorDal.Add(color);
            return new SuccessResult(Messages.ColorAdded);
        }

        public IResult Delete(Color color)
        {
            var result = Validator.Run(ColorExists(color));

            if (result.Success == false)
                return result;

            _colorDal.Delete(color);
            return new SuccessResult(Messages.ColorDeleted);
        }

        public IDataResult<List<Color>> GetAll(Func<Color, bool>? filter = null)
        {
            return new SuccessDataResult<List<Color>>(_colorDal.GetAll(filter));
        }

        public IDataResult<Color> GetById(int colorId)
        {
            var result = Validator.Run(ColorExistsById(colorId));
            if (result.Success == false)
                return new ErrorDataResult<Color>(result.Message);

            var color = _colorDal.Get(c => c.Id == colorId);

            return new SuccessDataResult<Color>(color);
        }

        public IResult Update(Color color)
        {
            var result = Validator.Run(ColorExistsById(color.Id));

            if (result.Success == false)
                return result;

            _colorDal.Update(color);
            return new SuccessResult(Messages.ColorUpdated);
        }

        private IResult ColorExistsById(int colorId)
        {
            var color = _colorDal.Get(c => c.Id == colorId);

            return color == null ? new ErrorResult(Messages.ColorNotFound) : new SuccessResult();
        }
        private IResult ColorNameExists(string colorName)
        {
            var colorResult = _colorDal.GetAll(c=> c.Name.ToLower() == colorName.ToLower()).Any();
            if (colorResult)
            {
                return new SuccessResult(Messages.ColorNameAlreadyExists);
            }

            return new ErrorResult();
        }
        private IResult ColorExists(Color color)
        {
            var colorSrc = _colorDal.Get(c => c.Id == color.Id);
            if (colorSrc == null)
                return new ErrorResult(Messages.ColorNotFound);

            var result = ObjectsComparer.CompareByValues(color, colorSrc);

            return result ? new SuccessResult() : new ErrorResult(Messages.ColorNotFound);
        }
    }
}
