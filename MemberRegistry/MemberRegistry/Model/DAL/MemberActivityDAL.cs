using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace MemberRegistry.Model.DAL
{
    public class MemberActivityDAL : DALBase
    {
        // Hämtar alla aktiviteter i databasen.
        public IEnumerable<ActivityType> GetMemberActivitiesById(int memberId)
        {
            // Skapar och initierar ett anslutningsobjekt.
            using (SqlConnection conn = CreateConnection())
            {
                try
                {
                    // Skapar ett List-objekt med 100 platser.
                    var memberActivities = new List<ActivityType>(100);

                    // Skapar och initierar ett SqlCommand-objekt som används till att exekveras specifierad lagrad procedur.
                    SqlCommand cmd = new SqlCommand("appSchema.usp_GetMemberActivities", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Lägger till den paramter den lagrade proceduren kräver.
                    cmd.Parameters.AddWithValue("@MedID", memberId);

                    // Öppnar anslutningen till databasen.
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Så länge som det finns poster att läsa returnerar Read true och läsningen fortsätter.
                        while (reader.Read())
                        {
                            // Tar reda på vilket index de olika kolumnerna har.
                            int avgiftStatusIndex = reader.GetOrdinal("Avgiftstatus");
                            int aktTypIndex = reader.GetOrdinal("Akttyp");

                            // Returnerar referensen till de skapade MemberActivity-objektet.
                            memberActivities.Add(new ActivityType
                            {
                                MedID = memberId,
                                Avgiftstatus = reader.GetString(avgiftStatusIndex),
                                Akttyp = reader.GetString(aktTypIndex)
                            });
                        }
                    }

                    // Avallokerar minne som inte används och skickar tillbaks listan med medlemsaktiviteter.
                    memberActivities.TrimExcess();
                    return memberActivities;
                }
                catch
                {
                    // Kastar ett eget undantag om ett undantag kastas.
                    throw new ApplicationException("An error occured in the data access layer.");
                }
            }
        }

        // Hämtar alla aktiviteters medlemmar i databasen.
        public IEnumerable<ActivityType> GetActivityById(int activityID)
        {
            // Skapar och initierar ett anslutningsobjekt.
            using (SqlConnection conn = CreateConnection())
            {
                try
                {
                    // Skapar ett List-objekt med 100 platser.
                    var activityMembers = new List<ActivityType>(100);

                    // Skapar och initierar ett SqlCommand-objekt som används till att exekveras specifierad lagrad procedur.
                    SqlCommand cmd = new SqlCommand("appSchema.usp_GetActivityById", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Lägger till den paramter den lagrade proceduren kräver.
                    cmd.Parameters.AddWithValue("@AktID", activityID);

                    // Öppnar anslutningen till databasen.
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Så länge som det finns poster att läsa returnerar Read true och läsningen fortsätter.
                        while (reader.Read())
                        {
                            // Tar reda på vilket index de olika kolumnerna har.
                            int medAktIdIndex = reader.GetOrdinal("MedAktID");
                            int fNamnIndex = reader.GetOrdinal("Fnamn");
                            int eNamnIndex = reader.GetOrdinal("Enamn");

                            // Returnerar referensen till de skapade MemberActivity-objektet.
                            activityMembers.Add(new ActivityType
                            {
                                AktID = activityID,
                                MedAktID = reader.GetInt32(medAktIdIndex),
                                Fnamn = reader.GetString(fNamnIndex),
                                Enamn = reader.GetString(eNamnIndex)
                            });
                        }
                    }

                    // Avallokerar minne som inte används och skickar tillbaks listan med medlemsaktiviteter.
                    activityMembers.TrimExcess();
                    return activityMembers;
                }
                catch
                {
                    // Kastar ett eget undantag om ett undantag kastas.
                    throw new ApplicationException("An error occured in the data access layer.");
                }
            }
        }

        // Hämtar alla aktiviteters medlemmar i databasen.
        public void DeleteMemberActivityById(int memberActivityID)
        {
            // Skapar och initierar ett anslutningsobjekt.
            using (SqlConnection conn = CreateConnection())
            {
                try
                {
                    // Skapar och initierar ett SqlCommand-objekt som används till att exekveras specifierad lagrad procedur.
                    SqlCommand cmd = new SqlCommand("appSchema.usp_DeleteMemberActivityById", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Lägger till den paramter den lagrade proceduren kräver.
                    cmd.Parameters.AddWithValue("@MedAktID", memberActivityID);

                    // Öppnar anslutningen till databasen.
                    conn.Open();

                    // Den lagrade proceduren innehåller en DELETE-sats och returnerar inga poster varför metoden 
                    // ExecuteNonQuery används för att exekvera den lagrade proceduren.
                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    // Kastar ett eget undantag om ett undantag kastas.
                    throw new ApplicationException("An error occured in the data access layer.");
                }
            }
        }

        // Skapar en ny post i tabellen Medlem.
        public void InsertMemberActivity(int memberId, int activityId)
        {
            // Skapar och initierar ett anslutningsobjekt.
            using (SqlConnection conn = CreateConnection())
            {
                try
                {
                    // Skapar och initierar ett SqlCommand-objekt som används till att exekveras specifierad lagrad procedur.
                    SqlCommand cmd = new SqlCommand("appSchema.usp_AddMemberActivity", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Lägger till de paramterar den lagrade proceduren kräver.
                    cmd.Parameters.Add("@MedID", SqlDbType.Int).Value = memberId;
                    cmd.Parameters.Add("@AktID", SqlDbType.Int).Value = activityId;

                    // Hämtar data från den lagrade proceduren.
                    cmd.Parameters.Add("@MedAktID", SqlDbType.Int, 4).Direction = ParameterDirection.Output;

                    // Öppnar anslutningen till databasen.
                    conn.Open();

                    // Den lagrade proceduren innehåller en INSERT-sats och returnerar inga poster varför metoden 
                    // ExecuteNonQuery används för att exekvera den lagrade proceduren.
                    cmd.ExecuteNonQuery();

                    // Hämtar primärnyckelns värde för den nya posten och tilldelar MemberActivity-objektet värdet.
                    //memberActivity.MedAktID = (int)cmd.Parameters["@MedAktID"].Value;
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