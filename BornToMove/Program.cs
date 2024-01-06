using System.Security.Cryptography.X509Certificates;
using System.Xml.Serialization;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using BornToMove.Business;

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
            int choice = validChoice(1, 2);
            bool newMove = false;
            try
            {
                BuMove buMove = new BuMove();
                if (choice == 1)
                {
                    Console.WriteLine("De volgende move is voor u gekozen:");
                    chosenMove = buMove.getRandomMove();
                }
                else
                {
                    Console.WriteLine("Kies een move uit de volgende lijst van moves of toets 0 om een nieuwe move te maken.");
                    List<Move> allMoves = buMove.getAllMoves();
                    foreach (Move move in allMoves) {
                        Console.WriteLine(move.name + ": " + move.description + ": " + move.sweatrate.ToString());
                    }
                    choice = validChoice(0, allMoves.Count());
                    if (choice == 0)
                    {
                        newMove = true;
                        while (true)
                        {
                            Console.WriteLine("Voer de naam in van de nieuwe move:");
                            bool exists = false;
                            string? newName;
                            while (true)
                            {
                                exists = false;
                                newName = Console.ReadLine();
                                if (newName.IsNullOrEmpty())
                                {
                                    Console.WriteLine("Voer alstublieft een naam in.");
                                }
                                else
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
                            if (buMove.TryToMakeAMove(new Move(newName, newDescription, newSweatrate)))
                            {
                                break;
                            }
                        }
                        chosenMove = buMove.getLastMove();
                        Console.WriteLine("U heeft de volgende nieuwe move gemaakt:");
                    }
                    else
                    {
                        chosenMove = buMove.getMoveById(choice);
                        Console.WriteLine("U heeft de volgende move gekozen:");
                    }
                }
                Console.WriteLine(chosenMove.name + " - " + chosenMove.description);
                if (!newMove)
                {
                    Console.WriteLine("Als u de move heeft uitgevoerd, voer dan uw beoordeling van de move in:");
                    int rating = validChoice(1, 5);
                    Console.WriteLine("Voer nu de sweatrate van de move in:");
                    int newSweat = validChoice(1, 5);
                    chosenMove.sweatrate = newSweat;
                    buMove.tryToUpdateMove(chosenMove.name, chosenMove);
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
