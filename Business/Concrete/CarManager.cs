using Business.Abstract;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        public void Add(Car car)
        {
            if (car.Description.Length >= 2)
            {
                if (car.DailyPrice > 0)
                    _carDal.Add(car);
                else
                    Console.WriteLine("Günlük Kiralama Bedeli O TL den fazla olmalıdır");
            }
            else
            {
                Console.WriteLine("Araba açıklaması 2 karakterden küçük olamaz");
            }
        }

        public void Delete(Car car)
        {
            _carDal.Delete(car);
        }

        public List<Car> GetCars()
        {
            return _carDal.GetAll();
        }

        public List<Car> GetCarsByBrandId(int id)
        {
            return _carDal.GetAll(p=>p.BrandId == id);
        }

        public List<Car> GetCarsByColorId(int id)
        {
            return _carDal.GetAll(p =>p.ColorId == id);
        }

        public void Update(Car car)
        {
            _carDal.Update(car);
        }
    }
}
