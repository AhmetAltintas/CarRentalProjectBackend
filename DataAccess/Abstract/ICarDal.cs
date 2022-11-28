using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface ICarDal : IEntityRepository<Car>
    {
        List<CarDetailDTO> GetCarDetails();
        CarDetailDTO GetCarDetailsById(int id);
        List<CarDetailDTO> GetCarDetailsByColorId(int colorId);
        List<CarDetailDTO> GetCarDetailsByBrandId(int brandId);
    }
}
