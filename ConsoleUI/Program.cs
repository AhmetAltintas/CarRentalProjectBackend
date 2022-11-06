using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using System.Net.Http.Headers;


//GetCarDetailsTest();

GetRentDetailsTest();












static void GetCarDetailsTest()
{
    CarManager carManager = new CarManager(new EfCardal());

    var result = carManager.GetCarDetails();

    if (result.Success)
    {
        foreach (var car in carManager.GetCarDetails().Data)
        {
            Console.WriteLine(car.BrandName + car.ModelName + " / " + car.ColorName + " / " + car.DailyPrice);
        }
    }
    else
    {
        Console.WriteLine(result.Message);
    }
}

static void GetRentDetailsTest()
{
    RentManager rentManager = new RentManager(new EfRentDal());
    var result = rentManager.GetRentDetails();
    foreach (var rent in result.Data)
    {
        Console.WriteLine(rent.RentId + " numaralı kiralama başlangıç tarihi :  " + rent.RentDate + ", bitiş tarihi : " + rent.ReturnDate);
    }
}