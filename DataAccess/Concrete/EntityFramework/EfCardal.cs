using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCardal : EfEntityRepositoryBase<Car, DbForRecapContext>, ICarDal
    {
        public List<CarDetailDTO> GetCarDetails()
        {
            using (DbForRecapContext context = new DbForRecapContext())
            {
                var result = from ca in context.Cars
                             join br in context.Brands
                             on ca.BrandId equals br.Id
                             join co in context.Colors
                             on ca.ColorId equals co.Id
                             select new CarDetailDTO
                             {
                                 BrandName = br.BrandName,
                                 ModelName = ca.ModelName,
                                 ColorName = co.ColorName,
                                 DailyPrice = ca.DailyPrice,

                             };
                return result.ToList();
                             
            }
        }
    }
}
