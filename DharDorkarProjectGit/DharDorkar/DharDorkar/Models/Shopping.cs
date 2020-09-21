﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DharDorkar.Models
{
    public class Shippingdetail
    {
        public int ShippingDetailId { get; set; }
        [Required]
        public Nullable<int> MemberId { get; set; }
        [Required]
        public string Adress { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string District { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string ZipCode { get; set; }
        public Nullable<int> OrderId { get; set; }
        public Nullable<decimal> AmountPaid { get; set; }
        [Required]
        public string PaymentType { get; set; }
        [Required]
        public string NidNumber { get; set; }
        [Required]
        public Nullable<int> PhoneNumber { get; set; }
    }
}