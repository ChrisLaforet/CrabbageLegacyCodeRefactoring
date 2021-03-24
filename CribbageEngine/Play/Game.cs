using CribbageEngine.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CribbageEngine.Play
{
//Currently no plan to implement more than 2 player game 1-Player - 1-AI
//This will need to be interfaced with the WinForm in a MVC and/or event-handling way
    public class Game
    {
        public const int MAX_PLAYERS = 2;
        public const int MIN_PLAYERS = 2;


        private List<Player> _players = new List<Player>();

        public void RegisterPlayer(Player player)
		{
            if (_players.Count >= MAX_PLAYERS)
			{
                throw new TooManyPlayersException("Game already has maxed out its player limit");
			}
            _players.Add(player);
		}

        public IList<Player> Players
		{
            get
			{
                return _players;
			}
		}

        public Round Start()
		{
            if (_players.Count < MIN_PLAYERS)
			{
                throw new NotEnoughPlayersException("Game cannot start until there are enough players");
			}
            return new Round(this);
		}
    }
}
