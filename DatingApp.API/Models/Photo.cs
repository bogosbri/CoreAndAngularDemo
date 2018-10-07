using System;

namespace DatingApp.API.Models
{
    public class Photo
    {
        public int Id { get; set;}

        public string Url { get; set;}

        public string Description { get; set;}

        public DateTime DateAdded { get; set; }

        public bool IsMain { get; set;}

    // we added these two to get entity framework to build the schema the way we want. Instead of a restricted deleted we will
    //get a cascading delete. If a user is deleted then the photos will also be deleted
        public User User { get; set;}

        public int UserId{ get; set;}

    }
}