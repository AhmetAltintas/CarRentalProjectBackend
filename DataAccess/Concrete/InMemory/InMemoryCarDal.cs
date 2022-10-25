using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
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
                new Car{Id=1, BrandId=1, ColorId = 1, ModelYear = "2000", DailyPrice=25250, Description= "GT-R X (R34)"},
                new Car{Id=2, BrandId=2, ColorId = 8, ModelYear = "1997", DailyPrice=23500, Description= "Supra 4"},
                new Car{Id=3, BrandId=3, ColorId = 3, ModelYear = "2001", DailyPrice=20150, Description= "Corvette C5"},
                new Car{Id=4, BrandId=4, ColorId = 6, ModelYear = "2001", DailyPrice=27300, Description= "E46 M3 GTR"},
                new Car{Id=5, BrandId=5, ColorId = 4, ModelYear = "2000", DailyPrice=17750, Description= "Civic Sedan"}

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

        public List<Car> GetAll()
        {
            return _cars;
        }

        public List<Car> GetById(int id)
        {
            return _cars.Where(c => c.Id == id).ToList();
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

