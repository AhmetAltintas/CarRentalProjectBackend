using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using System.Net.Http.Headers;


//GetCarDetailsTest();

UserManager userManager = new UserManager(new EfUserDal());
User user1 = new User { UserId = 1, FirstName = "Ahmet", LastName = "ALTINTAŞ", Email = "ahmtaltnts35@gmail.com", Password = "12345abc" };
userManager.Add(user1);

CustomerManager customerManager = new CustomerManager(new EfCustomerDal());
Customer customer1 = new Customer { CustomerId = 1, UserId = 1, CompanyName = "ALTNTS" };
customerManager.Add(customer1);

RentManager rentManager = new RentManager(new EfRentDal());
Rent rent1 = new Rent {RentId = 1, CarId = 1, CustomerId = 1, RentDate = new DateTime(4,11,2022), ReturnDate = new DateTime(6,11,2022)};
rentManager.Add(rent1);
Rent rent2 = new Rent { RentId = 2, CarId = 2, CustomerId = 1, RentDate = new DateTime(4, 11, 2022), ReturnDate = new DateTime(6, 11, 2022) };
rentManager.Add(rent2);


var result = rentManager.GetRentDetails();
foreach (var rent in result.Data)
{
    Console.WriteLine(rent.RentId + " numaralı kiralama başlangıç tarihi :  " + rent.RentDate + ", bitiş tarihi : " + rent.ReturnDate);
}














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

