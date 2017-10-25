using System;
using System.Collections.Generic;
namespace BrightIdeas.Models
{
    public class User
    {
        public int UserId{get;set;}
        public string Name {get;set;}
        public string Alias {get;set;}
        public string Email {get;set;}
        public string Password {get;set;}
        public DateTime create_at{get;set;}
        public List<Subscribtion> subscribtions {get;set;}
        public List<Idea> ideas{get;set;}
        public User()
        {
            create_at=DateTime.Now;
            subscribtions=new List<Subscribtion>();
            ideas=new List<Idea>();
        }
    }
}