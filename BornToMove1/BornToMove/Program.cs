using System.Security.Cryptography.X509Certificates;
using System.Xml.Serialization;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;

namespace BornToMove
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Het is tijd dat u gaat bewegen!");
            Console.WriteLine("Wilt u een bewegingssuggestie of wilt u uit de lijst kiezen?");
            Console.WriteLine("Toets 1 voor een suggestie of 2 voor kiezen uit de lijst.");
            Move chosenMove;
            List<Move> moves = new List<Move>();
            Random rand = new Random(); 
            int choice = validChoice(1, 2);
            try
            {
                SqlConnectionStringBuilder builder = connectToDB();
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    bool newMove = false;
                    if (choice == 1)
                    {
                        Console.WriteLine("De volgende move is voor u gekozen:");
                        String sql = "SELECT Id, name, description, sweatrate FROM dbo.move";
                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                            connection.Open();
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    moves.Add(new Move(
                                        Convert.ToInt16(reader["Id"]),
                                        Convert.ToString(reader["name"]),
                                        Convert.ToString(reader["description"]),
                                        Convert.ToInt16(reader["sweatrate"])));
                                }
                                reader.Close();
                            }
                        }
                        chosenMove = moves[rand.Next(0, moves.Count - 1)];
                    }
                    else
                    {
                        Console.WriteLine("Kies een move uit de volgende lijst van moves of toets 0 om een nieuwe move te maken.");
                        String sql = "SELECT Id, name, description, sweatrate FROM dbo.move";
                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                            connection.Open();
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    moves.Add(new Move(
                                        Convert.ToInt16(reader["Id"]),
                                        Convert.ToString(reader["name"]),
                                        Convert.ToString(reader["description"]),
                                        Convert.ToInt16(reader["sweatrate"])));
                                    Console.WriteLine(reader["Id"].ToString() + " " + reader["name"].ToString() + ": " + reader["description"].ToString());
                                }
                                reader.Close();
                            }
                        }
                        choice = validChoice(0, moves.Count());
                        if (choice == 0)
                        {
                            newMove = true;
                            Console.WriteLine("Voer de naam in van de nieuwe move:");
                            bool exists = false;
                            string? newName;
                            while (true)
                            {
                                exists = false;
                                newName = Console.ReadLine();
                                foreach (Move thisMove in moves)
                                {
                                    if (thisMove.name == newName)
                                    {
                                        exists = true;
                                        break;
                                    }
                                }
                                if (exists)
                                {
                                    Console.WriteLine("Deze naam bestaat al. Voer een andere naam in.");
                                }
                                else if (newName.IsNullOrEmpty())
                                {
                                    Console.WriteLine("Voer alstublieft een naam in.");
                                } else
                                {
                                    break;
                                }
                            }
                            Console.WriteLine("Voer een omschrijving in van de nieuwe move:");
                            String? newDescription;
                            while (true)
                            {
                                newDescription = Console.ReadLine();
                                if (newDescription.IsNullOrEmpty())
                                {
                                    Console.WriteLine("Voer alstublieft een omschrijving in.");
                                }
                                {
                                    break;
                                }
                            }
                            Console.WriteLine("Voer een sweatrate tussen 1 en 5 in van de nieuwe move:");
                            int newSweatrate = validChoice(1, 5);
                            moves.Add(new Move(moves.Count, newName, newDescription, newSweatrate));
                            chosenMove = moves[moves.Count - 1];
                            Console.WriteLine("U heeft de volgende nieuwe move gemaakt:");
                            sql = "INSERT INTO dbo.move (name, description, sweatrate) VALUES ('" + chosenMove.name + "', '" + 
                                chosenMove.description + "', '" + chosenMove.sweatrate + "')";
                            using (SqlCommand command = new SqlCommand(sql, connection))
                            {
                                SqlDataReader reader = command.ExecuteReader();
                                String? status = Convert.ToString(reader);
                            }
                        }
                        else
                        {
                            chosenMove = moves[choice - 1];
                            Console.WriteLine("U heeft de volgende move gekozen:");
                        }
                    }
                    Console.WriteLine(chosenMove.name + " - " + chosenMove.description);
                    if (!newMove)
                    {
                        Console.WriteLine("Als u de move heeft uitgevoerd, voer dan uw beoordeling van de move in:");
                        int rating = validChoice(1, 5);
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
                Console.WriteLine("probleem");
            }
            Console.ReadLine();
        }

        static int validChoice(int lower, int higher)
        {
            int[] optionList = Enumerable.Range(lower, higher).ToArray();
            int choice;
            while (true) {
                choice = Convert.ToInt32(Console.ReadLine());
                if (optionList.Contains(choice))
                {
                    return choice;
                }
                else
                {
                     Console.WriteLine("Ongeldige keuze, probeer opnieuw.");
                }
            }

        }

        static SqlConnectionStringBuilder connectToDB()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = "(localdb)\\mssqllocaldb";
            builder.TrustServerCertificate = true;
            builder.InitialCatalog = "born2move";
            return builder;

        }
    }
}
