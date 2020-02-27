using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace tryout_gigamin1_nugi_mulya_nugraha.Models
{

    
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Username {get; set;}
        public string  Full_name { get; set; }
        public string  Email { get; set; }
        public string Phone_number { get; set; }
    }

    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Customer Cstmer_id {get; set;}
        public Driver Driver_id { get; set; }
        public List<Product> Order_detail {get; set;}
        
    }

    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Price { get; set; }
        public string Name  { get; set; }

    }

    public class Driver
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Full_name { get; set; }
        public string Phone_number {get; set;}
    }

}