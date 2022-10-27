/*Kampımızla beraber paralelde geliştireceğimiz bir projemiz daha olacak. Araba kiralama sistemi yazıyoruz.

Yepyeni bir proje oluşturunuz. Adı ReCapProject olacak. (Tekrar ve geliştirme projesi)

Entities, DataAccess, Business ve Console katmanlarını oluşturunuz.

Bir araba nesnesi oluşturunuz. "Car"

Özellik olarak : Id, BrandId, ColorId, ModelYear, DailyPrice, Description alanlarını ekleyiniz. (Brand = Marka)

InMemory formatta GetById, GetAll, Add, Update, Delete oprasyonlarını yazınız.

Consolda test ediniz.

Önemli: Copy - Paste yasak fakat kamp projesinden destek almak serbest.

Kodlarınızı Github'a aktarıp paylaşınız. İncelediğiniz arkadaşlarınıza yıldız vermeyi unutmayınız.*/

using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;

/*CarManager carManager = new CarManager(new InMemoryCarDal());

foreach (var car in carManager.GetCars())
{
    Console.WriteLine(car.BrandId + " " + car.Description);
}
*/


CarManager carManager = new CarManager(new EfCardal());

foreach (var car in carManager.GetCarsByColorId(1))
{
    Console.WriteLine(car.Description);
}




