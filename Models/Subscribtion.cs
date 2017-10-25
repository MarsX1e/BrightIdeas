using System;
using System.Collections.Generic;
namespace BrightIdeas.Models
{
    public class Subscribtion
    {
        public int SubscribtionId{get;set;}
        public int UserId{get;set;}
        public int IdeaId{get;set;}
        public DateTime create_at{get;set;}
        public User User{get;set;}
        public Idea Idea {get;set;}
        public Subscribtion()
        {
            create_at=DateTime.Now;
        }
    }
}