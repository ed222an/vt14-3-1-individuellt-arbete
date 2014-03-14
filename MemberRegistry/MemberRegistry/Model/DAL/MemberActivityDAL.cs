using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace MemberRegistry.Model.DAL
{
    public class ActivityDAL : DALBase
    {
        // Hämtar alla aktiviteter i databasen.
        public MemberActivity GetMemberActivitiesById(int memberId)
        {
            // Skapar och initierar ett anslutningsobjekt.
            using (SqlConnection conn = CreateConnection())
            {
                try
                {
                    // Skapar och initierar ett SqlCommand-objekt som används till att exekveras specifierad lagrad procedur.
                    SqlCommand cmd = new SqlCommand("appSchema.usp_GetMember", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Lägger till den paramter den lagrade proceduren kräver.
                    cmd.Parameters.AddWithValue("@MedID", memberId);

                    // Öppnar anslutningen till databasen.
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Så länge som det finns poster att läsa returnerar Read true och läsningen fortsätter.
                        if (reader.Read())
                        {
                            // Tar reda på vilket index de olika kolumnerna har.
                            int fNamnIndex = reader.GetOrdinal("Fnamn");
                            int eNamnIndex = reader.GetOrdinal("Enamn");
                            int persNrIndex = reader.GetOrdinal("PersNR");
                            int addressIndex = reader.GetOrdinal("Address");
                            int postNrIndex = reader.GetOrdinal("PostNR");
                            int ortIndex = reader.GetOrdinal("Ort");

                            // Returnerar referensen till de skapade Member-objektet.
                            return new Member
                            {
                                MedID = memberId,
                                Fnamn = reader.GetString(fNamnIndex),
                                Enamn = reader.GetString(eNamnIndex),
                                PersNR = reader.GetString(persNrIndex),
                                Address = reader.GetString(addressIndex),
                                PostNR = reader.GetString(postNrIndex),
                                Ort = reader.GetString(ortIndex)
                            };
                        }
                    }

                    return null;
                }
                catch
                {
                    // Kastar ett eget undantag om ett undantag kastas.
                    throw new ApplicationException("An error occured in the data access layer.");
                }
            }
        }
    }
}