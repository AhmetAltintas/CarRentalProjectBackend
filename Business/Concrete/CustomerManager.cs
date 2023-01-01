using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CustomerManager : ICustomerService
    {
        ICustomerDal _customerDal;

        public CustomerManager(ICustomerDal customerDal)
        {
            _customerDal = customerDal;
        }


        [ValidationAspect(typeof(CustomerValidator))]
        public IResult Add(Customer customer)
        {
            if (customer.UserId == null && customer.CompanyName.Length <= 2 )
            {
                return new ErrorResult(Messages.CustomerInvalid);
            }
            _customerDal.Add(customer);
            return new SuccessResult(Messages.CustomerAdded);
        }


        public IResult Delete(Customer customer)
        {
            _customerDal.Delete(customer);
            return new SuccessResult(Messages.CustomerDeleted);
        }


        [ValidationAspect(typeof(CustomerValidator))]
        public IResult Update(Customer customer)
        {
            _customerDal.Update(customer);
            return new SuccessResult(Messages.CustomerUpdated);
        }


        public IDataResult<List<Customer>> GetAll()
        {
            return new SuccessDataResult<List<Customer>>(_customerDal.GetAll());
        }


        public IDataResult<Customer> GetById(int id)
        {
            var result = _customerDal.Get(c=> c.Id == id);
            return new SuccessDataResult<Customer>(result);
        }
    }
}
