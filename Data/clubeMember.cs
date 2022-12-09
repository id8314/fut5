using System;
namespace fut5.Data
{
    public class ClubeMember{
        public string email { get; set; }        
        public string clube { get; set; }
        public bool isMember { get; set; }
        public bool isActive { get; set; }

        public ClubeMember() { }
        public ClubeMember(String atletaEmail, String clubeName, bool member, bool active)
        {
            this.email = atletaEmail;
            this.clube = clubeName;
            this.isMember = member;
            this.isActive = isActive;
        }
    }
}