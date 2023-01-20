﻿using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class PaymentManager : IPaymentService
    {
        IPaymentDal _paymentDal;

        public PaymentManager(IPaymentDal paymentDal)
        {
            _paymentDal = paymentDal;
        }


        public IResult Add(Payment payment)
        {
            _paymentDal.Add(payment);
            return new SuccessResult();
        }

        public IResult CheckIfThisCardIsAlreadySavedForThisCustomer(Payment payment)
        {
            var result = _paymentDal.Get(p => p.CustomerId == payment.CustomerId && p.CardNumber == payment.CardNumber);
            if (result != null) return new ErrorResult(Messages.ThisCardIsAlreadyRegisteredForThisCustomer);

            return new SuccessResult();
        }

        public IResult Delete(Payment payment)
        {
            _paymentDal.Delete(payment);
            return new SuccessResult();
        }


        public IDataResult<List<Payment>> GetAll()
        {
            return new SuccessDataResult<List<Payment>>(_paymentDal.GetAll());
        }

        public IDataResult<List<Payment>> GetAllByCustomerId(int customerId)
        {
            var result = _paymentDal.GetAll(p => p.CustomerId == customerId);
            return new SuccessDataResult<List<Payment>>(result);
        }

        public IDataResult<Payment> GetById(int id)
        {
            var result = _paymentDal.Get(p => p.Id == id);
            return new SuccessDataResult<Payment>(result);
        }

        [TransactionScopeAspect]
        [ValidationAspect(typeof(PaymentValidator))]
        public IResult Pay(Payment payment)
        {
            var result = _paymentDal.Get(p =>
            p.FullName == payment.FullName
            && p.CardNumber == payment.CardNumber
            && p.ExpiryMonth == payment.ExpiryMonth
            && p.ExpiryYear == payment.ExpiryYear
            && p.CVV == payment.CVV
            );

            if (result != null)
            {
                return new SuccessResult(Messages.PaymentSuccessful);
            }
            return new ErrorResult(Messages.PaymentDenied);
        }


        public IResult Update(Payment payment)
        {
            _paymentDal.Update(payment);
            return new SuccessResult();
        }
    }
}
