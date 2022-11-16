using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryCarDal : ICarDal
    {
        List<Car> _cars;

        public InMemoryCarDal()
        {
            _cars = new List<Car>
            {
                new Car{Id=1, BrandId=1, ColorId = 2, ModelYear = "2000",ModelName = "GT-R X (R34)",DailyPrice=100, Description= "Modified"},
                new Car{Id=2, BrandId=2, ColorId = 8, ModelYear = "1997",ModelName = "Supra 4", DailyPrice=100, Description= "Modified"},
                new Car{Id=3, BrandId=3, ColorId = 3, ModelYear = "2001",ModelName = "Corvette C5", DailyPrice=85, Description= "Stock"},
                new Car{Id=4, BrandId=4, ColorId = 1, ModelYear = "2001",ModelName = "E46 M3 GTR", DailyPrice=110, Description= "Modified"},
                new Car{Id=5, BrandId=5, ColorId = 4, ModelYear = "2000",ModelName = "Civic Sedan", DailyPrice=70, Description= "Stock"}

            };
        }

        public void Add(Car car)
        {
            _cars.Add(car);
        }

        public void Delete(Car car)
        {
            Car carToDelete = _cars.SingleOrDefault(c => c.Id == car.Id);
            _cars.Remove(carToDelete);
        }

        public Car Get(Expression<Func<Car, bool>> filter)
        {
            throw new NotImplementedException();
        }
        
        public List<Car> GetAll(Expression<Func<Car, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public List<Car> GetById(int id)
        {
            return _cars.Where(c => c.Id == id).ToList();
        }

        public List<CarDetailDTO> GetCarDetails()
        {
            throw new NotImplementedException();
        }

        public void Update(Car car)
        {
            Car carToUpdate = _cars.SingleOrDefault(c => c.Id == car.Id);
            carToUpdate.Id = car.Id;
            carToUpdate.BrandId = car.BrandId;
            carToUpdate.ModelYear = car.ModelYear;
            carToUpdate.DailyPrice = car.DailyPrice;
            carToUpdate.Description = car.Description;
            carToUpdate.ColorId = car.ColorId;

        }
    }
}

