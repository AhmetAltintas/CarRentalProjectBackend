using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        ICarDal _carDal;

        public CarManager(ICarDal carDal)
        {
            _carDal = carDal;
        }


        [CacheRemoveAspect("ICarService.Get")]
        //[SecuredOperation("admin,customer")]
        [ValidationAspect(typeof(CarValidator))]
        public IResult Add(Car car)
        {
            IResult result = BusinessRules.Run(CheckIfCarIdIsAlreadyExists(car.ModelName));

            if (result != null)
            {
                return result;
            }

            _carDal.Add(car);
            return new SuccessResult(Messages.CarAdded);
        }


        //[SecuredOperation("admin,customer")]
        [ValidationAspect(typeof(CarValidator))]
        public IResult Delete(Car car)
        {
            _carDal.Delete(car);
            return new SuccessResult(Messages.CarDeleted);
        }


        public IDataResult<Car> GetById(int id)
        {
            return new SuccessDataResult<Car>(_carDal.Get(c => c.Id == id));
        }


        [CacheAspect]
        public IDataResult<List<CarDetailDTO>> GetCarDetails()
        {
            if (DateTime.Now.Hour == 02)
            {
                return new ErrorDataResult<List<CarDetailDTO>>(Messages.MaintenanceTime);
            }

            return new SuccessDataResult<List<CarDetailDTO>>(_carDal.GetCarDetails());
        }

        public IDataResult<List<CarDetailDTO>> GetCarDetailsByBrandId(int brandId)
        {
            return new SuccessDataResult<List<CarDetailDTO>>(_carDal.GetCarDetailsByBrandId(brandId), Messages.CarDetailsListed);
        }

        public IDataResult<List<CarDetailDTO>> GetCarDetailsByColorId(int colorId)
        {
            return new SuccessDataResult<List<CarDetailDTO>>(_carDal.GetCarDetailsByColorId(colorId), Messages.CarDetailsListed);
        }

        public IDataResult<CarDetailDTO> GetCarDetailsById(int id)
        {
            return new SuccessDataResult<CarDetailDTO>(_carDal.GetCarDetailsById(id), Messages.CarDetailsListed);
        }

        [CacheAspect]
        [PerformanceAspect(5)]
        public IDataResult<List<Car>> GetCars()
        {
            if (DateTime.Now.Hour == 02)
            {
                return new ErrorDataResult<List<Car>>(Messages.MaintenanceTime);
            }
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(), Messages.CarsListed);
        }


        public IDataResult<List<Car>> GetCarsByBrandId(int brandId)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c => c.BrandId == brandId));
        }


        public IDataResult<List<Car>> GetCarsByColorId(int colorId)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c => c.ColorId == colorId));
        }


        //[SecuredOperation("admin,customer")]
        [ValidationAspect(typeof(CarValidator))]
        public IResult Update(Car car)
        {
            _carDal.Update(car);
            return new SuccessResult(Messages.CarUpdated);
        }







        private IResult CheckIfCarIdIsAlreadyExists(string modelName)
        {
            var result = _carDal.GetAll(c => c.ModelName == modelName).Any();
            if (result == true)
            {
                return new ErrorResult(Messages.CarIsAlreadyExists);
            }
            return new SuccessResult();
        }
    }
}
