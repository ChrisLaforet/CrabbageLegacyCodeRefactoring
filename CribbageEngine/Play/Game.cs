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

        private IScoreBoard _scoreBoard;

        public Game() { }

        public Game(IScoreBoard scoreBoard)
		{
            this._scoreBoard = scoreBoard;
		}

        public void RegisterPlayer(Player player)
		{
            if (_players.Count >= MAX_PLAYERS)
			{
                throw new TooManyPlayersException("Game already has maxed out its player limit");
			}

            if (player.IsDealer && Dealer != null)
			{
                throw new TooManyDealersException("Game already has a dealer");
			}

            if (player.IsDealer)
			{
                Dealer = player;
			}
            else
			{
                _players.Add(player);
            }
        }

        public Player Dealer { get; private set; }

        public IList<Player> Players
		{
            get
			{
                return _players;
			}
		}

        internal void RackScore(RoundPlayer player, PlayScore newScore)
		{
            if (_scoreBoard != null)
            {
                _scoreBoard.RackScore(player, newScore);
            }
		}

        public Round StartRound()
		{
            if (Dealer == null && _players.Count > 0)
			{
                Dealer = _players.Last();
                Dealer.IsDealer = true;
            }
            else
			{
                _players.Add(Dealer);
			}
            if (_players.Count < MIN_PLAYERS)
            {
                throw new NotEnoughPlayersException("Game cannot start until there are enough players");
            }
            return new Round(this);
		}
    }
}
