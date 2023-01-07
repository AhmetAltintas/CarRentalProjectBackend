﻿using Core.Entities.Concrete;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants
{
    public static class Messages
    {
        public static string CarAdded = "Araba eklendi.";
        public static string CarNameInvalid = "Araba ismi geçersiz.";
        public static string MaintenanceTime = "Sistem bakımda.";
        public static string CarsListed = "Arabalar listelendi.";
        public static string CarDeleted = "Araba silindi.";
        public static string CarUpdated = "Araba güncellendi.";

        public static string UserNameInvalid = "Kullanıcı adı geçersiz.";
        public static string UserAdded = "Kullanıcı eklendi.";
        public static string UserDeleted = "Kullanıcı silindi.";
        public static string UserUpdated = "Kullanıcı güncellendi.";
        public static string EmailIsAlreadyRegistered = "Bu e-posta adresine kayıtlı bir hesap zaten var.";

        public static string CustomerInvalid = "Müşteri geçersiz.";
        public static string CustomerAdded = "Müşteri eklendi.";
        public static string CustomerDeleted = "Müşteri silindi.";
        public static string CustomerUpdated = "Müşteri güncellendi.";

        public static string RentInvalid = "Kiralama geçersiz.";
        public static string RentAdded = "Kiralama eklendi.";
        public static string RentDeleted = "Kiralama silindi.";
        public static string RentUpdated = "Kiralama güncellendi.";

        public static string CarImageAdded = "Araba görseli eklendi.";
        public static string CarImageDeleted = "Araba görseli silindi. ";
        public static string CarImageUpdated = "Araba görseli güncellendi. ";

        public static string AuthorizationDenied = "Yetkiniz yok";

        public static string UserRegistered = "Kayıt oldu";
        public static string UserNotFound = "Kullanıcı bulunamadı.";
        public static string PasswordError = "Parola hatalı.";
        public static string SuccessfulLogin = "Başarılı giriş.";
        public static string UserAlreadyExists = "Kullanıcı zaten var.";
        public static string AccessTokenCreated = "Access token başarıyla oluşturuldu.";
        public static string EmailUpdated = "E-posta güncellendi.";
        public static string FirstAndLastNameUpdated = "Ad & soyad güncellendi.";

        public static string BrandAdded = "Marka eklendi.";
        public static string BrandDeleted = "Marka silindi.";
        public static string BrandUpdated = "Marka güncellendi.";


        public static string SuccessfullyListed = "Başarıyla listelendi.";

        public static string ColorAdded = "Renk eklendi.";
        public static string ColorDeleted = "Renk silindi.";
        public static string ColorUpdated = "Renk güncellendi.";

        public static string CarIsAlreadyExists = "Araba zaten var.";
        public static string CarDetailsListed = "Araba detayları listelendi.";


        public static string CarImageLimitExceeded = "Araba görsel limiti dolu.";
        public static string CarImageDoesNotFound = "Araç resmi bulunamadı.";


        public static string RentDateCannotBeBeforeToday = "Kiralama tarihi günümüzden önce olamaz.";
        public static string ReturnDateCannotBeLeftBlankAsThisCarWasAlsoRentedAtALaterDate = "Bu araç daha sonra bir tarihte kiralanmış olduğu için iade tarihi boş bırakılamaz";
        public static string ThisCarHasNotBeenReturned = "Araç henüz geri dönmedi";
        public static string ReturnDateCannotBeEarlierThanRentDate = "Dönüş tarihi kiralama tarihinden önce olamaz";
        public static string ThisCarIsAlreadyRentedInSelectedDateRange = "Araba bu tarihler arasında zaten kiralanmıştır";

        public static string PaymentSuccessful = "Ödeme başarılı.";
        public static string PaymentDenied = "Ödeme bilgileri reddedildi.";

        public static string CardNumberMustConsistOfLettersOnly = "Kart numarası sadece rakamlardan oluşmalıdır.";
        public static string LastTwoDigitsOfYearMustBeEntered = "Yılın son iki hanesini giriniz.";
        public static string CustomersFindeksScoreIsNotEnough = "Findeks skorunuz bu araç için yetersiz";
        public static string ThisCardIsAlreadyRegisteredForThisCustomer = "Bu kart zaten geçerli müşteriye kayıtlı.";


        public static string PasswordIsIncorrect = "Parola doğru değil.";
        public static string PasswordUpdated = "Parola güncellendi.";
        public static string PasswordsDoNotMatch = "Parolalar aynı değil.";
    }
}
