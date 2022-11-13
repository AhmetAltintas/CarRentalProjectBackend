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
                             on ca.BrandId equals b.BrandId
                             join re in context.Rentals
                             on ca.CarId equals re.CarId
                             join co in context.Colors
                             on ca.ColorId equals co.ColorId
                             from u in context.Users
                             join cu in context.Customers
                             on u.UserId equals cu.UserId
                             select new RentlDetailDTO
                             {
                                 CarId = ca.CarId,
                                 BrandId = b.BrandId,
                                 ColorName = co.ColorName,
                                 BrandName = b.BrandName,
                                 ModelName = ca.ModelName,
                                 RentId = re.RentId,
                                 RentDate = re.RentDate,
                                 ReturnDate = re.ReturnDate,
                                 FirstName = u.FirstName,
                                 Lastname = u.LastName

                             };
                return result.ToList();

            }
        }
    }
}
