using Castle.Core.Resource;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfRentDal : EfEntityRepositoryBase<Rent, DbForRecapContext>, IRentDal
    {
        public List<RentlDetailDTO> GetRentDetails()
        {
            using (DbForRecapContext context = new DbForRecapContext())
            {
                var result = from ca in context.Cars
                             join b in context.Brands
                             on ca.BrandId equals b.Id
                             join re in context.Rents
                             on ca.Id equals re.CarId
                             join co in context.Colors
                             on ca.ColorId equals co.Id
                             from u in context.Users
                             join cu in context.Customers
                             on u.Id equals cu.UserId
                             from ren in context.Rents
                             join cus in context.Customers
                             on ren.CustomerId equals cus.Id
                             
                             select new RentlDetailDTO
                             {
                                 Id = re.Id,
                                 CustomerId = cus.Id,
                                 FirstName = u.FirstName,
                                 LastName = u.LastName,
                                 BrandName = b.BrandName,
                                 ModelName = ca.ModelName,
                                 RentDate = re.RentDate,
                                 ReturnDate = re.ReturnDate
                             };
                return result.ToList();

            }
        }
    }
}
