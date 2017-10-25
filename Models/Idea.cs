using System;
using System.Collections.Generic;
namespace BrightIdeas.Models
{
    public class Idea
    {
        public int IdeaId{get;set;}
        public string IdeaText {get;set;}
        public DateTime create_at{get;set;}
        public int UserId{get;set;}
        public List<Subscribtion> subscribtions {get;set;}
        public User user{get;set;}
        public Idea()
        {
            create_at=DateTime.Now;
            subscribtions=new List<Subscribtion>();
        }
    }
}