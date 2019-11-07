using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Project.Entities
{
    public class ContactInfos
    {
        [DisplayName("Ülke")]
        public string Country { get; set; }

        [DisplayName("Şehir")]
        public string City { get; set; }

        [DisplayName("İlçe")]
        public string District { get; set; }

        [DisplayName("Açık Adres")]
        public string Address { get; set; }

        [StringLength(5)]
        [DisplayName("Posta Kodu")]
        public string PostCode { get; set; }

        [DisplayName("E-Mail"), Required(ErrorMessage = "Bu alan gerekli."), DataType(DataType.EmailAddress)]
        public string EMail { get; set; }

        [DisplayName("Cep Telefonu"), Required(ErrorMessage = "Bu alan gerekli."), 
            MinLength(11, ErrorMessage = "Girilen Cep Telefonu numarası 0 ile başlamalı ve 11 haneli olmalıdır."), 
            MaxLength(11, ErrorMessage = "Girilen Cep Telefonu numarası 0 ile başlamalı ve 11 haneli olmalıdır."), 
            DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

    }
}