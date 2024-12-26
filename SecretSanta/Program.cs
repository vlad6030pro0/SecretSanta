using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecretSanta
{
	internal class Program
	{
		public static List<Player> playerslist = new List<Player>();
		public static Dictionary<string, string> playersresult = new Dictionary<string, string>();
		static void Main(string[] args)
		{
            playerslist = GetPlayers();
			PrintPlayers();

			for (int i = 0; i < playerslist.Count; i++)
			{
				AddReceiver();
			}

			PrintResult();

			Console.ReadLine();
        }
		public static List<Player> GetPlayers()
		{
			StreamReader reader = new StreamReader("playerslist.txt");
			string[] playerslisttxt = reader.ReadToEnd().Split('\n');
			var players = new List<Player>();
			for (int i = 0; i < playerslisttxt.Length; i++)
			{
				Player player = new Player() 
				{
					Id = i,
					Name = playerslisttxt[i]
				};
				players.Add(player);
			}

			return players;
		}
		public static Player GetPlayer(int id)
		{
			var player = playerslist[id];
			return player;
		}
		public static void DeletePlayer(Player playerfordelete)
		{
			var player = playerslist.Where(x => x.Id == playerfordelete.Id).FirstOrDefault();
			if (player != null)
			{
				playerslist.RemoveAt(player.Id);
			}
			else
			{
                Console.WriteLine($"Player named \"{playerfordelete.Name}\" was not found!");
            }
		}
		public static void AddReceiver()
		{
			Random rand = new Random(123);

			int senderId = rand.Next(0, playerslist.Count);
			int receiverId = rand.Next(0, playerslist.Count);

			var sender = GetPlayer(senderId);
			var receiver = GetPlayer(receiverId);

			while(sender.Receiver != null)
			{
                Console.WriteLine("SENDER WHILE");
                senderId = rand.Next(0, playerslist.Count);
				sender = GetPlayer(senderId);
			}

			while (senderId == receiverId || receiver.Sender != null) 
			{
				if(sender.Sender == receiver.Receiver && sender.Sender != null && receiver.Receiver != null)
				{
					continue;
				}
                Console.WriteLine("RECEIVER WHILE");
                receiverId = rand.Next(0, playerslist.Count);
				receiver = GetPlayer(receiverId);
			}

			playersresult.Add(sender.Name, receiver.Name);
			receiver.Sender = sender;
			sender.Receiver = receiver;

			int a = 0;
		}
		public static void PrintResult()
		{
			Console.WriteLine("\n----------\nResult list:");
			foreach (var player in playersresult)
			{
                Console.WriteLine($"{player.Key} gives {player.Value}");
            }
			Console.WriteLine("----------");
		}
		public static void PrintPlayers()
		{
			if (playerslist != null)
			{
				Console.WriteLine("\n----------\nPlayers list:");
				foreach (var player in playerslist)
				{
					Console.WriteLine(player.Name);
				}
				Console.WriteLine("----------");
			}
			else
			{
				Console.WriteLine("Players list is null!");
			}
		}
	}

	public class Player
	{
        public int Id { get; set; }
        public string Name { get; set; }
		public Player Receiver { get; set; } = null;
		public Player Sender { get; set; } = null;
    }
}