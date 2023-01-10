using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using System.Net.Http.Headers;


//GetCarDetailsTest();











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