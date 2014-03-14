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
            using (var conn = CreateConnection())
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
    }
}