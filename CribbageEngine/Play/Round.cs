using CribbageEngine.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CribbageEngine.Play
{
	public class Round
	{
		public const int INITIAL_DEAL_CARD_COUNT = 6;
		public const int PER_PLAYER_CRIB_CARD_COUNT = 2;

		private readonly Deck _deck = new Deck();
		private readonly IList<Card> _crib = new List<Card>();
		private readonly IList<RoundPlayer> _players = new List<RoundPlayer>();
		private int _nextPlayerIndex;

		public Round(Game game)
		{
			this.Game = game;
			this._deck.Shuffle();
			foreach (Player player in game.Players)
			{
				_players.Add(new RoundPlayer(this, player));
			}
		}

		public RoundPlayer GetDealer()
		{
			foreach (RoundPlayer player in _players)
			{
				if (player.Player.IsDealer)
				{
					return player;
				}
			}
			throw new InvalidStateException("Round does not have a Dealer");
		}

		public RoundPlayer NextPlayer => _players[_nextPlayerIndex];

		public bool IsStarted { get; private set; }

		public bool IsFinished { get; private set; }

		private void AssertIsNotStarted()
		{
			if (IsStarted)
			{
				throw new OperationNotPermittedException("Round has already started");
			}
		}

		private void AssertIsStarted()
		{
			if (!IsStarted)
			{
				throw new OperationNotPermittedException("Round has not started");
			}
		}

		public bool IsPlayStarted
		{
			get
			{
				return StarterCard != null;
			}
		}

		private void AssertPlayIsNotStarted()
		{
			if (IsPlayStarted)
			{
				throw new OperationNotPermittedException("Round play has already started");
			}
		}

		private void AssertPlayIsStarted()
		{
			if (!IsPlayStarted)
			{
				throw new OperationNotPermittedException("Round play has not started");
			}
		}

		public Card StarterCard { get; private set; }

		public Game Game { get; private set; }

		private void BankCribCards(Card[] cards)
		{
			AssertIsStarted();
			AssertPlayIsNotStarted();
			if (cards == null || cards.Length != PER_PLAYER_CRIB_CARD_COUNT)
			{
				throw new InvalidCribCardCountException("Invalid number of cards to bank into crib");
			}
			foreach (Card card in cards)
			{
				_crib.Add(card);
			}
		}
		private void BankCribCards(IList<Card> cards)
		{
			this.BankCribCards(cards.ToArray());
		}

		public IList<Card> Crib
		{
			get
			{
				return _crib;
			}
		}

		public void Start()
		{
			AssertIsNotStarted();
			AssertPlayIsNotStarted();
			IList<Player> players = Game.Players;
			for (int count = 0; count < INITIAL_DEAL_CARD_COUNT; count++)
			{
				foreach (Player player in Game.Players)
				{
					player.AddCard(_deck.Draw());
				}
			}
			this.IsStarted = true;
		}

		public void StartPlay()
		{
			AssertIsStarted();
			AssertPlayIsNotStarted();

			PrepareCrib();
			CutDeckAndGetStarterCard();
			FindFirstPlayer();
			PlayRound();
		}

		private void PrepareCrib()
		{
			foreach (Player player in Game.Players)
			{
				BankCribCards(player.BankCribCards());
			}

			if (_crib.Count != Game.Players.Count * PER_PLAYER_CRIB_CARD_COUNT)
			{
				throw new CribNotProvidedException("Crib cards have not been banked by all players");
			}
		}

		private void CutDeckAndGetStarterCard()
		{
			this.StarterCard = _deck.GetStarterCard();
			if (this.StarterCard.Face == Card.FaceType.Jack)
			{
				this.GetDealer().AddScore(PlayScore.ScoreType.HisHeels, Evaluation.HeelsValue);
			}
		}

		private void FindFirstPlayer()
		{
			if (_players[0].Player.IsDealer)
			{
				_nextPlayerIndex = 1;
			} 
			else if (_players.Count > 2 && _players[1].Player.IsDealer)
			{
				_nextPlayerIndex = 2;
			}
			else
			{
				_nextPlayerIndex = 0;
			}
		}

		private void PlayRound() 
		{ 
			while (PlayersHaveCards() && PlayerIsNotWinner())
			{
				PlaySession();
			}
			IsFinished = true;
		}

		private bool PlayersHaveCards()
		{
			foreach (RoundPlayer player in _players)
			{
				if (player.HasCards())
				{
					return true;
				}
			}
			return false;
		}

		private bool PlayerIsNotWinner()
		{
			foreach (RoundPlayer player in _players)
			{
				if (player.Score >= Evaluation.GAME_WINNING_SCORE)
				{
					return false;
				}
			}
			return true;
		}

		private void RotatePlayer()
		{
			_nextPlayerIndex = (_nextPlayerIndex + 1) % _players.Count;
		}

		internal void RackScore(RoundPlayer player, PlayScore newScore)
		{
			Game.RackScore(player, newScore);
		}

		private void PlaySession()
		{
			AssertPlayIsStarted();

			List<Card> sessionCards = new List<Card>();
			int sessionScore = 0;

			RoundPlayer currentPlayer = NextPlayer;
			RotatePlayer();
			bool gotPass = false;
			while (PlayersHaveCards())
			{
				bool playLegal = true;
				IPlayResponse response = currentPlayer.Play(sessionCards.ToArray());
				if (response is Card)
				{
					Card card = response as Card;
					if (sessionScore + card.Value > PlayScore.MAX_PLAY_SCORE)
					{
						// event - illegal card
						playLegal = false;
					}
					else
					{
						sessionCards.Add(card);
						sessionScore += card.Value;
						PlayScore[] scores = Evaluation.EvaluatePlayHand(sessionCards.ToArray());
						if (scores.Length > 0)
						{
							currentPlayer.AddScores(scores);
						}
						gotPass = false;
					}
				}
				else if (gotPass)		// TODO: Logic will have to change for 3 players eventually
				{
					break;
				}
				else
				{
					gotPass = true;
				}
				if (playLegal)
				{
					currentPlayer = NextPlayer;
					RotatePlayer();
				}
			}
		}
	}
}
