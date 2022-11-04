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
    public interface IRentService
    {
        IResult Add(Rent rent);
        IResult Update(Rent rent);
        IResult Delete(Rent rent);
        IDataResult<List<RentlDetailDTO>> GetRentDetails();
    }
}
