﻿using System;
using System.Collections.Generic;
using MemberRegistry.Model.DAL;
using System.ComponentModel.DataAnnotations;
using MemberRegistry.App_Infrastructure;

namespace MemberRegistry.Model
{
    public class Service
    {
        #region Member

        // Privat fält
        private MemberDAL _memberDAL;

        // Egenskap som skapar en MemberDAL-klass om det inte redan finns någon.
        private MemberDAL MemberDAL
        {
            get { return _memberDAL ?? (_memberDAL = new MemberDAL()); }
        }

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
    }
}