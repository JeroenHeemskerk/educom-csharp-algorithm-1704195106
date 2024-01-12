using System.Security.Cryptography.X509Certificates;
using System.Xml.Serialization;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using BornToMove.Business;
using BornToMove.OrganizerTest;
using Org;
using BornToMove.DAL;

namespace BornToMove
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //test if RotateSort works as it should

            /*
            RotateSortTests tests = new RotateSortTests();
            tests.testSortEmpty();
            tests.testSortOneElement();
            tests.testSortTwoElements();
            tests.testSortThreeEqual();
            tests.testSortUnsortedArray();
            tests.testSortUnsortedThreeEqual();
            */
            try {
                BuMove buMove = new BuMove();
                buMove.AddMovesIfEmpty();

                Console.WriteLine("Het is tijd dat u gaat bewegen!");
                Console.WriteLine("Wilt u een bewegingssuggestie of wilt u uit de lijst kiezen?");
                Console.WriteLine("Toets 1 voor een suggestie of 2 voor kiezen uit de lijst.");
                Move chosenMove;
                MoveRating chosenRating;
                int choice = validChoice(1, 2);
                bool newMove = false;
                if (choice == 1)
                {
                    Console.WriteLine("De volgende move is voor u gekozen:");
                    chosenRating = buMove.getRandomMove();
                    chosenMove = chosenRating.Move;
                }
                else
                {
                    Console.WriteLine("Kies een move uit de volgende lijst van moves of toets 0 om een nieuwe move te maken.");
                    IEnumerable<MoveRating> allRatings = buMove.getAllMoves();
                    RotateSort<MoveRating> sort = new RotateSort<MoveRating>();
                    List<MoveRating> sortedByRating = sort.Sort(allRatings.ToList(), new RatingConverter());
                    for (int indexer = 0; indexer != allRatings.Count(); indexer++) {
                        Console.WriteLine((indexer + 1) + sortedByRating[indexer].Move.name + ": " + sortedByRating[indexer].Move.description + ", sweatrate: " 
                            + sortedByRating[indexer].Move.sweatrate.ToString() + ", rating: " + sortedByRating.Select(Rating => Rating.Rating).ElementAt(indexer) 
                            + ", Vote:" + sortedByRating.Select(Rating => Rating.Vote).ElementAt(indexer));
                    }
                    choice = validChoice(0, sortedByRating.Select(rating => rating.Move).Count() + 1);
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
                            chosenMove = new Move(newName, newDescription, newSweatrate);
                            chosenRating = new MoveRating(chosenMove, 0, 0);
                            if (buMove.TryToMakeAMove(new Move(newName, newDescription, newSweatrate)))
                            {
                                break;
                            }
                        }
                        Console.WriteLine("U heeft de volgende nieuwe move gemaakt:");
                    }
                    else
                    {
                        chosenRating = sortedByRating.ElementAt(choice - 1);
                        Console.WriteLine("U heeft de volgende move gekozen:");
                    }
                }
                Console.WriteLine(chosenRating.Move.name + " - " + chosenRating.Move.description + ", Rating:" + chosenRating.Rating
                    + ", Vote:" + chosenRating.Vote);
                if (!newMove)
                {
                    Console.WriteLine("Als u de move heeft uitgevoerd, voer dan uw rating van de move in:");
                    int newRating = validChoice(1, 5);
                    Console.WriteLine("Voer nu de vote van de move in:");
                    int newVote = validChoice(1, 5);
                    buMove.TryToMakeRating(new MoveRating(chosenRating.Move, newRating, newVote));
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
