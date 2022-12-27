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
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class RentManager : IRentService
    {
        IRentDal _rentDal;

        public RentManager(IRentDal rentDal)
        {
            _rentDal = rentDal;
        }


        [CacheRemoveAspect("ICarService.Get")]
        [SecuredOperation("admin,customer,user")]
        [ValidationAspect(typeof(RentValidator))]
        public IResult Add(Rent rent)
        {
            var result = RulesForAdding(rent);

            if (!result.Success)
            {
                return result;
            }
            _rentDal.Add(rent);
            return new SuccessResult(Messages.RentAdded);
        }


        [SecuredOperation("admin")]
        public IResult Delete(Rent rent)
        {
            _rentDal.Delete(rent);
            return new SuccessResult(Messages.RentDeleted);
        }


        //[PerformanceAspect(5)]
        [CacheAspect]
        public IDataResult<List<RentlDetailDTO>> GetRentDetails()
        {
            return new SuccessDataResult<List<RentlDetailDTO>>(_rentDal.GetRentDetails());
        }


        [SecuredOperation("admin")]
        [ValidationAspect(typeof(RentValidator))]
        [ValidationAspect(typeof(RentValidator))]
        public IResult Update(Rent rent)
        {
            _rentDal.Update(rent);
            return new SuccessResult(Messages.RentUpdated);
        }


        public IDataResult<Rent> GetById(int id)
        {
            return new SuccessDataResult<Rent>(_rentDal.Get(r => r.Id == id));
        }


        public IDataResult<List<RentlDetailDTO>> GetRentDetailsByCarId(int carId)
        {
            return new SuccessDataResult<List<RentlDetailDTO>>(_rentDal.GetRentDetailsByCarId(carId));
        }




        public IResult RulesForAdding(Rent rent)
        {
            var result = BusinessRules.Run(
                CheckIfRentDateIsBeforeToday(rent.RentDate),
                CheckIfReturnDateIsBeforeThanRentDate(rent.ReturnDate, rent.RentDate),
                CheckIfThisCarIsAlreadyRentedInSelectedDateRange(rent),
                CheckIfThisCarIsRentedAtALaterDateWhileReturnDateIsNull(rent),
                CheckIfThisCarHasBeenReturned(rent));
            if (result != null)
            {
                return result;
            }
            return new SuccessResult("Ödeme sayfasına yönlendiriliyorsunuz.");
        }





        private IResult CheckIfRentDateIsBeforeToday(DateTime rentDate)
        {
            if (rentDate.Date < DateTime.Now.Date)
            {
                return new ErrorResult(Messages.RentDateCannotBeBeforeToday);
            }
            return new SuccessResult();
        }

        private IResult CheckIfThisCarIsRentedAtALaterDateWhileReturnDateIsNull(Rent rent)
        {
            var result = _rentDal.Get(r =>
            r.CarId == rent.CarId
            && rent.ReturnDate == null
            && r.RentDate.Date > rent.RentDate.Date
            );

            if (result != null)
            {
                return new ErrorResult(Messages.ReturnDateCannotBeLeftBlankAsThisCarWasAlsoRentedAtALaterDate);
            }
            return new SuccessResult();
        }

        private IResult CheckIfThisCarHasBeenReturned(Rent rent)
        {
            var result = _rentDal.Get(r => r.CarId == rent.CarId && r.ReturnDate == null);

            if (result != null)
            {
                if (rent.ReturnDate==null || rent.ReturnDate>result.RentDate)
                {
                    return new ErrorResult(Messages.ThisCarHasNotBeenReturned);
                }
            }
            return new SuccessResult();
        }

        private IResult CheckIfReturnDateIsBeforeThanRentDate(DateTime? returnDate, DateTime rentDate)
        {
            if (returnDate != null)
            {
                if (returnDate < rentDate)
                {
                    return new ErrorResult(Messages.ReturnDateCannotBeEarlierThanRentDate);
                }
            }
            return new SuccessResult();
        }

        private IResult CheckIfThisCarIsAlreadyRentedInSelectedDateRange(Rent rent)
        {
            var result = _rentDal.Get(r =>
            r.CarId == rent.CarId
            && (r.RentDate.Date == rent.RentDate.Date
            || (r.RentDate.Date < rent.RentDate.Date
            && (r.ReturnDate == null
            || ((DateTime)r.ReturnDate).Date > rent.RentDate.Date))));

            if (result != null)
            {
                return new ErrorResult(Messages.ThisCarIsAlreadyRentedInSelectedDateRange);
            }
            return new SuccessResult();
        }





    }
}
