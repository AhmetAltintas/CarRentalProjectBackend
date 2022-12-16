using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IPaymentService
    {
        IDataResult<List<Payment>> GetAll();
        IResult Pay(Payment payment);
        IResult Add(Payment payment);
        IResult Update(Payment payment);
        IResult Delete(Payment payment);

    }
}
