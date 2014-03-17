using System;
using System.Collections.Generic;
using MemberRegistry.Model.DAL;
using System.ComponentModel.DataAnnotations;
using MemberRegistry.App_Infrastructure;

namespace MemberRegistry.Model
{
    public class Service
    {
        // Privata fält
        private MemberDAL _memberDAL;
        private ActivityDAL _activityDAL;
        private MemberActivityDAL _memberActivityDAL;

        // Egenskaper som skapar DAL-klasser om det inte redan finns någora.
        private MemberDAL MemberDAL
        {
            get { return _memberDAL ?? (_memberDAL = new MemberDAL()); }
        }

        private ActivityDAL ActivityDAL
        {
            get { return _activityDAL ?? (_activityDAL = new ActivityDAL()); }
        }

        private MemberActivityDAL MemberActivityDAL
        {
            get { return _memberActivityDAL ?? (_memberActivityDAL = new MemberActivityDAL()); }
        }

        #region Member

        // Tar bort vald medlem ur databasen.
        public void DeleteMember(int memberId)
        {
            MemberDAL.DeleteMember(memberId);
        }

        // Sparar en medlems uppgifter i databasen.
        public void SaveMember(Member member)
        {
            // Uppfyller inte objektet affärsreglerna...
            ICollection<ValidationResult> validationResults;
            if (!member.Validate(out validationResults))
            {
                // Klarar inte objektet valideringen så kastas ett undantag, samt en referens till valideringssamlingen.
                var ex = new ValidationException("Objektet klarade inte valideringen.");
                ex.Data.Add("ValidationResults", validationResults);
                throw ex;
            }

            // Sparar contact-objektet. Är MedID 0 skapas en ny medlem...
            if (member.MedID == 0)
            {
                MemberDAL.InsertMember(member);
            }
            else //...annars uppdateras en befintlig.
            {
                MemberDAL.UpdateMember(member);
            }
        }

        // Hämtar en medlem med ett specifikt id från databasen.
        public Member GetMember(int memberId)
        {
            return MemberDAL.GetMemberById(memberId);
        }

        // Hämtar alla medlemmar ur databasen.
        public IEnumerable<Member> GetMembers()
        {
            return MemberDAL.GetMembers();
        }

        #endregion

        #region Activity

        // Hämtar alla aktiviteter ur databasen.
        public IEnumerable<Activity> GetActivities()
        {
            return ActivityDAL.GetActivities();
        }

        public IEnumerable<Activity> GetActivityById(int activityId)
        {
            return ActivityDAL.GetActivityById(activityId);
        }

        #endregion

        #region MemberActivities

        // Hämtar medlemsaktivitet från med ett specifikt id från databasen.
        public IEnumerable<ActivityType> GetMemberActivities(int memberId)
        {
            return MemberActivityDAL.GetMemberActivitiesById(memberId);
        }

        // Sparar en medlemsaktivitet uppgifter i databasen.
        public void SaveMemberActivity(MemberActivity memberActivity)
        {
            // Uppfyller inte objektet affärsreglerna...
            ICollection<ValidationResult> validationResults;
            if (!memberActivity.Validate(out validationResults))
            {
                // Klarar inte objektet valideringen så kastas ett undantag, samt en referens till valideringssamlingen.
                var ex = new ValidationException("Objektet klarade inte valideringen.");
                ex.Data.Add("ValidationResults", validationResults);
                throw ex;
            }

            // Lägger till medlemsaktiviteten i databasen.
            MemberActivityDAL.InsertMemberActivity(memberActivity);
        }

        #endregion
    }
}