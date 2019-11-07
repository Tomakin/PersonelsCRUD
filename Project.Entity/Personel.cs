using Project.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Project.Entities
{
    public class Personel : ContactInfos, IEntity
    {
        public int ID { get; set; }

        [DisplayName("Adı"), Required(ErrorMessage ="Bu alan gerekli."), MinLength(2), MaxLength(20, ErrorMessage = "Maximum 20 karakter girilebilir.")]
        public string Name { get; set; }
        
        [DisplayName("Soyadı"), Required(ErrorMessage = "Bu alan gerekli."), DataType(DataType.Text),
            MaxLength(20, ErrorMessage = "Maximum 20 karakter girilebilir.")]
        public string SurName { get; set; }

        [DisplayName("TC Kimlik Numarası"), Required(ErrorMessage = "Bu alan gerekli."), 
            MaxLength(11, ErrorMessage = "Girilen TC Kimlik No 11 haneli olmalıdır."), 
            MinLength(11, ErrorMessage = "Girilen TC Kimlik No 11 haneli olmalıdır."), 
            DataType(DataType.PhoneNumber), 
            RegularExpression("[0-9]{11}", ErrorMessage = "Lütfen rakamlardan oluşan geçerli bir TC Kimlik numarası giriniz.")]
        public string TCNo { get; set; }

        [DisplayName("Şirket Adı")]
        public string CompanyName { get; set; }

        [DisplayName("İletişim Türü")]
        public string ContactType { get; set; }
    }
}
