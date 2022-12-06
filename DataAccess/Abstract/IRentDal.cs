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
    public interface IRentDal : IEntityRepository<Rent>
    {
        List<RentlDetailDTO> GetRentDetails();
        List<RentlDetailDTO> GetRentDetailsByCarId(int carId);

    }
}
