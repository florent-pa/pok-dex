using ConsoleTables;
using System;
using System.Text.Json;

namespace ProgPokedexConsol
{
    class DlPk
    {
        private static Task[] tasks = new Task[8];
        public static List<Pokemon> listePokemon = new List<Pokemon>();
        public static int[,] listeGen()
        {
            return new int[,]
            {
                {1, 151, 1},
                {152, 251, 2},
                {252, 386, 3},
                {387, 493, 4},
                {494, 649, 5},
                {650, 721, 6},
                {722, 802, 7},
                {803, 898, 8}
            };
        }

        public static string[] listeType()
        {
            return new string[]
            {
                "Normal" , "Fire", "Water", "Grass","Electric", "Ice" , "Fighting", "Poison" ,"Ground", "Flying" ,"Psychic", "Bug" ,"Rock", "Ghost" ,"Dark", "Dragon" ,"Steel", "Fairy"     
            };
        }


        /// <summary>
        /// télécharge l'ensemble des pokémon présent
        /// </summary>
        public static void DlGeneration()
        {
            int[,] tabGen = listeGen();
            Console.WriteLine("Les Génération numéro 1 à 8 se chargent");
            //remplissage des lites
            tasks[0] = Task.Run(() => { GetJSON(tabGen[0,0], tabGen[0,1], tabGen[0,2]); });
            tasks[1] = Task.Run(() => { GetJSON(tabGen[1,0], tabGen[1,1], tabGen[1,2]); });
            tasks[2] = Task.Run(() => { GetJSON(tabGen[2,0], tabGen[2,1], tabGen[2,2]); });
            tasks[3] = Task.Run(() => { GetJSON(tabGen[3,0], tabGen[3,1], tabGen[3,2]); });
            tasks[4] = Task.Run(() => { GetJSON(tabGen[4,0], tabGen[4,1], tabGen[4,2]); });
            tasks[5] = Task.Run(() => { GetJSON(tabGen[5,0], tabGen[5,1], tabGen[5,2]); });
            tasks[6] = Task.Run(() => { GetJSON(tabGen[6,0], tabGen[6,1], tabGen[6,2]); });
            tasks[7] = Task.Run(() => { GetJSON(tabGen[7,0], tabGen[7,1], tabGen[7,2]); });

            Task.WaitAll(tasks);
            listePokemon.Sort((pkmn1, pkmn2) => { return pkmn1.id.CompareTo(pkmn2.id); });
            Thread.Sleep(2000); Console.Clear();
            Program.Choix();
        }

        public static void ListeType()
        {
            var table = new ConsoleTable("type", "type");
            table.AddRow("Normal", "Fire");
            table.AddRow("Water", "Grass");
            table.AddRow("Electric", "Ice");
            table.AddRow("Fighting", "Poison");
            table.AddRow("Ground", "Flying");
            table.AddRow("Psychic", "Bug");
            table.AddRow("Rock", "Ghost");
            table.AddRow("Dark", "Dragon");
            table.AddRow("Steel", "Fairy");

            table.Write();
        }

       
        public static void GetJSON(int borne_inf, int borne_sup, int gen)
        {
            int i;
            List<Pokemon> temp = new List<Pokemon>();

            using (System.Net.WebClient webClient=new System.Net.WebClient())
            {
                                
                for (i = borne_inf; i <= borne_sup; i++)
                {
                    string jsonStringPerPokemon = webClient.DownloadString("https://tmare.ndelpech.fr/tps/pokemons/" + i);
                    //On télécharge le pokémon i et on l'ajoute à la liste
                    temp.Add(JsonSerializer.Deserialize<Pokemon>(jsonStringPerPokemon));
                    //Console.WriteLine("\r" + pokemon.id);
                }
                listePokemon.AddRange(temp);               

            }

        }
    }
}
