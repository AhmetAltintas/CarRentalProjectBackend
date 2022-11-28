using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICarService
    {
        IDataResult<List<Car>> GetCars();
        IDataResult<Car> GetById(int id);
        IDataResult<List<Car>> GetCarsByBrandId(int brandId);
        IDataResult<List<Car>> GetCarsByColorId(int colorId);

        IDataResult<List<CarDetailDTO>> GetCarDetails();
        IDataResult<CarDetailDTO> GetCarDetailsById(int id);
        IDataResult<List<CarDetailDTO>> GetCarDetailsByBrandId(int brandId);
        IDataResult<List<CarDetailDTO>> GetCarDetailsByColorId(int colorId);


        IResult Add(Car car);
        IResult Update(Car car);
        IResult Delete(Car car);
    }
}

