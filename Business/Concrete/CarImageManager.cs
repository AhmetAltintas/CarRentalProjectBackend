using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Helpers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CarImageManager : ICarImageService
    {
        ICarImageDal _carImageDal;

        public CarImageManager(ICarImageDal carImageDal)
        {
            _carImageDal = carImageDal;
        }


        [CacheRemoveAspect("ICarImageService.Get")]
        [SecuredOperation("admin,customer")]
        [ValidationAspect(typeof(CarImageValidator))]
        public IResult Add(IFormFile image, CarImage carImage)
        {
            // Business Rules
            var ruleResult = BusinessRules.Run(CheckImageLimitExceeded(carImage.CarId));
            if (ruleResult != null)
            {
                return new ErrorResult(ruleResult.Message);
            }

            // Adding Image
            var imageResult = FileHelper.Add(image);
            carImage.ImagePath = imageResult.Message;
            if (!imageResult.Success)
            {
                return new ErrorResult(imageResult.Message);
            }
            _carImageDal.Add(carImage);
            return new SuccessResult(Messages.CarImageAdded);
        }


        [SecuredOperation("admin,customer")]
        [ValidationAspect(typeof(CarImageValidator))]
        public IResult Delete(CarImage carImage)
        {
            // Deleting Image
            var carToBeDeleted = _carImageDal.Get(c => c.Id == carImage.Id);
            if (carToBeDeleted == null)
            {
                return new ErrorResult(Messages.CarImageDoesNotFound);
            }
            FileHelper.Delete(carToBeDeleted.ImagePath);
            _carImageDal.Delete(carImage);
            return new SuccessResult(Messages.CarImageDeleted);
        }

        [SecuredOperation("admin,customer")]
        [ValidationAspect(typeof(CarImageValidator))]
        public IResult Update(IFormFile image, CarImage carImage)
        {
            // Updating Image
            var carToBeUpdated = _carImageDal.Get(c => c.Id == carImage.Id);
            if (carToBeUpdated == null)
            {
                return new ErrorResult(Messages.CarImageDoesNotFound);
            }
            var imageResult = FileHelper.Update(image, carToBeUpdated.ImagePath);
            carImage.ImagePath = imageResult.Message;
            if (!imageResult.Success)
            {
                return new ErrorResult(imageResult.Message);
            }
            _carImageDal.Update(carImage);
            return new SuccessResult(Messages.CarImageUpdated);
        }


        [CacheAspect]
        [PerformanceAspect(5)]
        public IDataResult<List<CarImage>> GetAll()
        {
            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll());
        }


        public IDataResult<List<CarImage>> GetCarImagesByCarId(int carId)
        {
            var data = _carImageDal.GetAll(cI => cI.CarId == carId);
            if (data.Count == 0)
            {
                data.Add(new CarImage
                {
                    CarId = carId,
                    ImagePath = "/Images/default.jpg"
                });
            }
            return new SuccessDataResult<List<CarImage>>(data);
        }


        public IDataResult<CarImage> GetById(int Id)
        {
            return new SuccessDataResult<CarImage>(_carImageDal.Get(cI => cI.Id == Id));
        }



        private IResult CheckIfCarImageLimit(int carId)
        {
            var result = _carImageDal.GetAll(c => c.CarId == carId).Count;
            if (result >= 5)
            {
                return new ErrorResult();
            }
            return new SuccessResult();
        }


        private IDataResult<List<CarImage>> GetDefaultImage(int carId)
        {

            List<CarImage> carImage = new List<CarImage>();
            carImage.Add(new CarImage { CarId = carId, Date = DateTime.Now, ImagePath = " DefaultImage.jpg " });
            return new SuccessDataResult<List<CarImage>>(carImage);
        }


        private IResult CheckCarImage(int carId)
        {
            var result = _carImageDal.GetAll(c => c.CarId == carId).Count;
            if (result > 0)
            {
                return new SuccessResult();
            }
            return new ErrorResult();
        }






        private IResult CheckImageLimitExceeded(int carId)
        {
            var carImagesOfTheCar = _carImageDal.GetAll(p => p.CarId == carId);
            if (carImagesOfTheCar.Count >= 5)
            {
                return new ErrorResult(Messages.CarImageLimitExceeded);
            }
            return new SuccessResult();
        }
    }
}
