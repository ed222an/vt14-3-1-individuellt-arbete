using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace MemberRegistry.Model.DAL
{
    public class MemberDAL : DALBase
    {

        #region CRUD-metoder

        // Hämtar alla medlemmar i databasen.
        public IEnumerable<Member> GetMembers()
        {
            // Skapar och initierar ett anslutningsobjekt.
            using (var conn = CreateConnection())
            {
                try
                {
                    // Skapar ett List-objekt med 100 platser.
                    var members = new List<Member>(100);

                    // Skapar och initierar ett SqlCommand-objekt som används till att exekveras specifierad lagrad procedur.
                    var cmd = new SqlCommand("appSchema.usp_GetMembers", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Öppnar anslutningen till databasen.
                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        // Tar reda på vilket index de olika kolumnerna har.
                        var medIdIndex = reader.GetOrdinal("MedID");
                        var fNamnIndex = reader.GetOrdinal("Fnamn");
                        var eNamnIndex = reader.GetOrdinal("Enamn");
                        var persNrIndex = reader.GetOrdinal("PersNR");
                        var addressIndex = reader.GetOrdinal("Address");
                        var postNrIndex = reader.GetOrdinal("PostNR");
                        var ortIndex = reader.GetOrdinal("Ort");

                        // Så länge som det finns poster att läsa returnerar Read true och läsningen fortsätter.
                        while (reader.Read())
                        {
                            // Hämtar ut datat för en post.
                            members.Add(new Member
                            {
                                MedID = reader.GetInt32(medIdIndex),
                                Fnamn = reader.GetString(fNamnIndex),
                                Enamn = reader.GetString(eNamnIndex),
                                PersNR = reader.GetString(persNrIndex),
                                Address = reader.GetString(addressIndex),
                                PostNR = reader.GetString(postNrIndex),
                                Ort = reader.GetString(ortIndex)
                            });
                        }
                    }

                    // Avallokerar minne som inte används och skickar tillbaks listan med medlemmar.
                    members.TrimExcess();
                    return members;
                }
                catch
                {
                    throw new ApplicationException("An error occured while getting members from the database.");
                }
            }
        }

        // Hämtar en medlems uppgifter.
        public Member GetMemberById(int memberId)
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
                            //int medIdIndex = reader.GetOrdinal("MedID");
                            int fNamnIndex = reader.GetOrdinal("Fnamn");
                            int eNamnIndex = reader.GetOrdinal("Enamn");
                            int persNrIndex = reader.GetOrdinal("PersNR");
                            int addressIndex = reader.GetOrdinal("Address");
                            int postNrIndex = reader.GetOrdinal("PostNR");
                            int ortIndex = reader.GetOrdinal("Ort");

                            // Returnerar referensen till de skapade Member-objektet.
                            return new Member
                            {
                                //MedID = reader.GetInt32(medIdIndex),
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

        // Skapar en ny post i tabellen Medlem.
        public void InsertMember(Member member)
        {
            // Skapar och initierar ett anslutningsobjekt.
            using (SqlConnection conn = CreateConnection())
            {
                try
                {
                    // Skapar och initierar ett SqlCommand-objekt som används till att exekveras specifierad lagrad procedur.
                    SqlCommand cmd = new SqlCommand("appSchema.usp_AddMember", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Lägger till de paramterar den lagrade proceduren kräver.
                    cmd.Parameters.Add("@Fnamn", SqlDbType.VarChar, 20).Value = member.Fnamn;
                    cmd.Parameters.Add("@Enamn", SqlDbType.VarChar, 20).Value = member.Enamn;
                    cmd.Parameters.Add("@PersNR", SqlDbType.VarChar, 11).Value = member.PersNR;
                    cmd.Parameters.Add("@Address", SqlDbType.VarChar, 30).Value = member.Address;
                    cmd.Parameters.Add("@PostNR", SqlDbType.VarChar, 6).Value = member.PostNR;
                    cmd.Parameters.Add("@Ort", SqlDbType.VarChar, 25).Value = member.Ort;

                    // Hämtar data från den lagrade proceduren.
                    cmd.Parameters.Add("@MedID", SqlDbType.Int, 4).Direction = ParameterDirection.Output;

                    // Öppnar anslutningen till databasen.
                    conn.Open();

                    // Den lagrade proceduren innehåller en INSERT-sats och returnerar inga poster varför metoden 
                    // ExecuteNonQuery används för att exekvera den lagrade proceduren.
                    cmd.ExecuteNonQuery();

                    // Hämtar primärnyckelns värde för den nya posten och tilldelar Member-objektet värdet.
                    member.MedID = (int)cmd.Parameters["@MedID"].Value;
                }
                catch
                {
                    // Kastar ett eget undantag om ett undantag kastas.
                    throw new ApplicationException("An error occured in the data access layer.");
                }
            }
        }

        // Uppdaterar en medlems uppgifter i tabellen Medlem.
        public void UpdateMember(Member member)
        {
            // Skapar och initierar ett anslutningsobjekt.
            using (SqlConnection conn = CreateConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("appSchema.usp_UpdateMember", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Lägger till de paramterar den lagrade proceduren kräver.
                    cmd.Parameters.Add("@Fnamn", SqlDbType.VarChar, 20).Value = member.Fnamn;
                    cmd.Parameters.Add("@Enamn", SqlDbType.VarChar, 20).Value = member.Enamn;
                    cmd.Parameters.Add("@PersNR", SqlDbType.VarChar, 11).Value = member.PersNR;
                    cmd.Parameters.Add("@Address", SqlDbType.VarChar, 30).Value = member.Address;
                    cmd.Parameters.Add("@PostNR", SqlDbType.VarChar, 6).Value = member.PostNR;
                    cmd.Parameters.Add("@Ort", SqlDbType.VarChar, 25).Value = member.Ort;

                    // Öppnar anslutningen till databasen.
                    conn.Open();

                    // Den lagrade proceduren innehåller en UPDATE-sats och returnerar inga poster varför metoden 
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

        // Tar bort en medlems uppgifter.
        public void DeleteMember(int memberId)
        {
            // Skapar och initierar ett anslutningsobjekt.
            using (SqlConnection conn = CreateConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("appSchema.usp_DeleteMember", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@MedID", SqlDbType.Int).Value = memberId;

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

        #endregion
    }
}