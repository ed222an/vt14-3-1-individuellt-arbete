using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace MemberRegistry.Model.DAL
{
    public class ActivityDAL : DALBase
    {
        // Hämtar alla aktiviteter i databasen.
        public IEnumerable<Activity> GetActivities()
        {
            // Skapar och initierar ett anslutningsobjekt.
            using (SqlConnection conn = CreateConnection())
            {
                try
                {
                    // Skapar ett List-objekt med 100 platser.
                    var activities = new List<Activity>(100);

                    // Skapar och initierar ett SqlCommand-objekt som används till att exekveras specifierad lagrad procedur.
                    var cmd = new SqlCommand("appSchema.usp_GetActivities", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Öppnar anslutningen till databasen.
                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        // Tar reda på vilket index de olika kolumnerna har.
                        var aktIdIndex = reader.GetOrdinal("AktID");
                        var akttypIndex = reader.GetOrdinal("Akttyp");

                        // Så länge som det finns poster att läsa returnerar Read true och läsningen fortsätter.
                        while (reader.Read())
                        {
                            // Hämtar ut datat för en post.
                            activities.Add(new Activity
                            {
                                AktID = reader.GetInt32(aktIdIndex),
                                Akttyp = reader.GetString(akttypIndex),
                            });
                        }
                    }

                    // Avallokerar minne som inte används och skickar tillbaks listan med aktiviteter.
                    activities.TrimExcess();
                    return activities;
                }
                catch
                {
                    throw new ApplicationException("An error occured while getting members from the database.");
                }
            }
        }

        // Hämtar en medlems uppgifter.
        public IEnumerable<Activity> GetActivityById(int activityId)
        {
            // Skapar och initierar ett anslutningsobjekt.
            using (SqlConnection conn = CreateConnection())
            {
                try
                {
                    // Skapar ett List-objekt med 100 platser.
                    var activities = new List<Activity>(100);

                    // Skapar och initierar ett SqlCommand-objekt som används till att exekveras specifierad lagrad procedur.
                    SqlCommand cmd = new SqlCommand("appSchema.usp_GetActivityById", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Lägger till den paramter den lagrade proceduren kräver.
                    cmd.Parameters.AddWithValue("@AktID", activityId);

                    // Öppnar anslutningen till databasen.
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Så länge som det finns poster att läsa returnerar Read true och läsningen fortsätter.
                        if (reader.Read())
                        {
                            // Tar reda på vilket index de olika kolumnerna har.
                            int aktTypIndex = reader.GetOrdinal("Akttyp");

                            // Returnerar referensen till de skapade Aktivitets-objektet.
                            activities.Add(new Activity
                            {
                                AktID = activityId,
                                Akttyp = reader.GetString(aktTypIndex)
                            });
                        }
                    }

                    // Avallokerar minne som inte används och skickar tillbaks listan med aktiviteter.
                    activities.TrimExcess();
                    return activities;
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