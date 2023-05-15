using System.Collections.Generic;

namespace GameVanilla.Game.Common
{
    /// <summary>
    /// Match detector that detects horizontal matches.
    /// </summary>
	public class KareMatchDetector : MatchDetector
    {
        /// <summary>
        /// Returns the list of detected matches.
        /// </summary>
        /// <param name="board">The game board.</param>
        /// <returns>The list of detected matches.</returns>
        public override List<Match> DetectMatches(GameBoard board)
        {
            var matches = new List<Match>();

            for (var j = 0; j < board.level.height; j++)
            {
                for (var i = 0; i < board.level.width;)
                {
                    var tile = board.GetTile(i, j);
                    if (tile != null && tile.GetComponent<Candy>() != null)
                    {
                        var color = tile.GetComponent<Candy>().color;
                        if (board.GetTile(i + 1, j) != null && board.GetTile(i + 1, j).GetComponent<Candy>() != null &&
                            board.GetTile(i + 1, j).GetComponent<Candy>().color == color && board.GetTile(i + 1, j).GetComponent<StripedCandy>() == null && board.GetTile(i + 1, j).GetComponent<WrappedCandy>() == null &&
                            board.GetTile(i + 2, j) != null && board.GetTile(i + 2, j).GetComponent<Candy>() != null &&
                            board.GetTile(i + 2, j).GetComponent<Candy>().color == color && board.GetTile(i + 2, j).GetComponent<StripedCandy>() == null && board.GetTile(i + 2, j).GetComponent<WrappedCandy>() == null &&
                            board.GetTile(i, j + 1) != null && board.GetTile(i, j + 1).GetComponent<Candy>() != null &&
                            board.GetTile(i, j + 1).GetComponent<Candy>().color == color && board.GetTile(i, j + 1).GetComponent<StripedCandy>() == null && board.GetTile(i, j + 1).GetComponent<WrappedCandy>() == null &&
                            board.GetTile(i, j + 2) != null && board.GetTile(i, j + 2).GetComponent<Candy>() != null &&
                            board.GetTile(i, j + 2).GetComponent<Candy>().color == color && board.GetTile(i, j + 2).GetComponent<StripedCandy>() == null && board.GetTile(i, j + 2).GetComponent<WrappedCandy>() == null)
                        {
                            var match = new Match();
                            match.type = MatchType.KareShaped;
                            match.AddTile(board.GetTile(i, j));
                            match.AddTile(board.GetTile(i + 1, j));
                            match.AddTile(board.GetTile(i + 2, j));
                            match.AddTile(board.GetTile(i, j + 1));
                            match.AddTile(board.GetTile(i, j + 2));
                            matches.Add(match);

                            var k = i + 3;
                            while (k < board.level.width && board.GetTile(k, j) != null &&
                                   board.GetTile(k, j).GetComponent<Candy>() != null &&
                                   board.GetTile(k, j).GetComponent<Candy>().color == color && board.GetTile(k, j).GetComponent<StripedCandy>() == null && board.GetTile(k, j).GetComponent<WrappedCandy>() == null)
                            {
                                match.AddTile(board.GetTile(k, j));
                                k += 1;
                            }

                            k = j + 3;
                            while (k < board.level.height && board.GetTile(i, k) != null &&
                                   board.GetTile(i, k).GetComponent<Candy>() != null &&
                                   board.GetTile(i, k).GetComponent<Candy>().color == color && board.GetTile(i, k).GetComponent<StripedCandy>() == null && board.GetTile(i, k).GetComponent<WrappedCandy>() == null)
                            {
                                match.AddTile(board.GetTile(i, k));
                                k += 1;
                            }
                        }

                        if (board.GetTile(i + 1, j) != null && board.GetTile(i + 1, j).GetComponent<Candy>() != null &&
                            board.GetTile(i + 1, j).GetComponent<Candy>().color == color && board.GetTile(i + 1, j).GetComponent<StripedCandy>() == null && board.GetTile(i + 1, j).GetComponent<WrappedCandy>() == null &&
                            board.GetTile(i + 2, j) != null && board.GetTile(i + 2, j).GetComponent<Candy>() != null &&
                            board.GetTile(i + 2, j).GetComponent<Candy>().color == color && board.GetTile(i + 2, j).GetComponent<StripedCandy>() == null && board.GetTile(i + 2, j).GetComponent<WrappedCandy>() == null &&
                            board.GetTile(i, j - 1) != null && board.GetTile(i, j - 1).GetComponent<Candy>() != null &&
                            board.GetTile(i, j - 1).GetComponent<Candy>().color == color && board.GetTile(i, j - 1).GetComponent<StripedCandy>() == null && board.GetTile(i, j - 1).GetComponent<WrappedCandy>() == null &&
                            board.GetTile(i, j - 2) != null && board.GetTile(i, j - 2).GetComponent<Candy>() != null &&
                            board.GetTile(i, j - 2).GetComponent<Candy>().color == color && board.GetTile(i, j - 2).GetComponent<StripedCandy>() == null && board.GetTile(i, j - 2).GetComponent<WrappedCandy>() == null)
                        {
                            var match = new Match();
                            match.type = MatchType.KareShaped;
                            match.AddTile(board.GetTile(i, j));
                            match.AddTile(board.GetTile(i + 1, j));
                            match.AddTile(board.GetTile(i + 2, j));
                            match.AddTile(board.GetTile(i, j - 1));
                            match.AddTile(board.GetTile(i, j - 2));
                            matches.Add(match);

                            var k = i + 3;
                            while (k < board.level.width && board.GetTile(k, j) != null &&
                                   board.GetTile(k, j).GetComponent<Candy>() != null &&
                                   board.GetTile(k, j).GetComponent<Candy>().color == color && board.GetTile(k, j).GetComponent<StripedCandy>() == null && board.GetTile(k, j).GetComponent<WrappedCandy>() == null)
                            {
                                match.AddTile(board.GetTile(k, j));
                                k += 1;
                            }

                            k = j - 3;
                            while (k >= 0 && board.GetTile(i, k) != null && board.GetTile(i, k).GetComponent<Candy>() != null &&
                                   board.GetTile(i, k).GetComponent<Candy>().color == color && board.GetTile(i, k).GetComponent<StripedCandy>() == null && board.GetTile(i, k).GetComponent<WrappedCandy>() == null)
                            {
                                match.AddTile(board.GetTile(i, k));
                                k -= 1;
                            }
                        }

                        if (board.GetTile(i - 1, j) != null && board.GetTile(i - 1, j).GetComponent<Candy>() != null &&
                            board.GetTile(i - 1, j).GetComponent<Candy>().color == color && board.GetTile(i - 1, j).GetComponent<StripedCandy>() == null && board.GetTile(i - 1, j).GetComponent<WrappedCandy>() == null &&
                            board.GetTile(i - 2, j) != null && board.GetTile(i - 2, j).GetComponent<Candy>() != null &&
                            board.GetTile(i - 2, j).GetComponent<Candy>().color == color && board.GetTile(i - 2, j).GetComponent<StripedCandy>() == null && board.GetTile(i - 2, j).GetComponent<WrappedCandy>() == null &&
                            board.GetTile(i, j + 1) != null && board.GetTile(i, j + 1).GetComponent<Candy>() != null &&
                            board.GetTile(i, j + 1).GetComponent<Candy>().color == color && board.GetTile(i, j + 1).GetComponent<StripedCandy>() == null && board.GetTile(i, j + 1).GetComponent<WrappedCandy>() == null &&
                            board.GetTile(i, j + 2) != null && board.GetTile(i, j + 2).GetComponent<Candy>() != null &&
                            board.GetTile(i, j + 2).GetComponent<Candy>().color == color && board.GetTile(i, j + 2).GetComponent<StripedCandy>() == null && board.GetTile(i, j + 2).GetComponent<WrappedCandy>() == null)
                        {
                            var match = new Match();
                            match.type = MatchType.KareShaped;
                            match.AddTile(board.GetTile(i, j));
                            match.AddTile(board.GetTile(i - 1, j));
                            match.AddTile(board.GetTile(i - 2, j));
                            match.AddTile(board.GetTile(i, j + 1));
                            match.AddTile(board.GetTile(i, j + 2));
                            matches.Add(match);

                            var k = i - 3;
                            while (k >= 0 && board.GetTile(k, j) != null && board.GetTile(k, j).GetComponent<Candy>() != null &&
                                   board.GetTile(k, j).GetComponent<Candy>().color == color && board.GetTile(k, j).GetComponent<StripedCandy>() == null && board.GetTile(k, j).GetComponent<WrappedCandy>() == null)
                            {
                                match.AddTile(board.GetTile(k, j));
                                k -= 1;
                            }

                            k = j + 3;
                            while (k < board.level.height && board.GetTile(i, k) != null &&
                                   board.GetTile(i, k).GetComponent<Candy>() != null &&
                                   board.GetTile(i, k).GetComponent<Candy>().color == color && board.GetTile(i, k).GetComponent<StripedCandy>() == null && board.GetTile(i, k).GetComponent<WrappedCandy>() == null)
                            {
                                match.AddTile(board.GetTile(i, k));
                                k += 1;
                            }
                        }

                        if (board.GetTile(i - 1, j) != null && board.GetTile(i - 1, j).GetComponent<Candy>() != null &&
                            board.GetTile(i - 1, j).GetComponent<Candy>().color == color && board.GetTile(i - 1, j).GetComponent<StripedCandy>() == null && board.GetTile(i - 1, j).GetComponent<WrappedCandy>() == null &&
                            board.GetTile(i - 2, j) != null && board.GetTile(i - 2, j).GetComponent<Candy>() != null &&
                            board.GetTile(i - 2, j).GetComponent<Candy>().color == color && board.GetTile(i - 2, j).GetComponent<StripedCandy>() == null && board.GetTile(i - 2, j).GetComponent<WrappedCandy>() == null &&
                            board.GetTile(i, j - 1) != null && board.GetTile(i, j - 1).GetComponent<Candy>() != null &&
                            board.GetTile(i, j - 1).GetComponent<Candy>().color == color && board.GetTile(i, j - 1).GetComponent<StripedCandy>() == null && board.GetTile(i, j - 1).GetComponent<WrappedCandy>() == null &&
                            board.GetTile(i, j - 2) != null && board.GetTile(i, j - 2).GetComponent<Candy>() != null &&
                            board.GetTile(i, j - 2).GetComponent<Candy>().color == color && board.GetTile(i, j - 2).GetComponent<StripedCandy>() == null && board.GetTile(i, j - 2).GetComponent<WrappedCandy>() == null)
                        {
                            var match = new Match();
                            match.type = MatchType.KareShaped;
                            match.AddTile(board.GetTile(i, j));
                            match.AddTile(board.GetTile(i - 1, j));
                            match.AddTile(board.GetTile(i - 2, j));
                            match.AddTile(board.GetTile(i, j - 1));
                            match.AddTile(board.GetTile(i, j - 2));
                            matches.Add(match);

                            var k = i - 3;
                            while (k >= 0 && board.GetTile(k, j) != null && board.GetTile(k, j).GetComponent<Candy>() != null &&
                                   board.GetTile(k, j).GetComponent<Candy>().color == color && board.GetTile(k, j).GetComponent<StripedCandy>() == null && board.GetTile(k, j).GetComponent<WrappedCandy>() == null)
                            {
                                match.AddTile(board.GetTile(k, j));
                                k -= 1;
                            }

                            k = j - 3;
                            while (k >= 0 && board.GetTile(i, k) != null && board.GetTile(i, k).GetComponent<Candy>() != null &&
                                   board.GetTile(i, k).GetComponent<Candy>().color == color && board.GetTile(i, k).GetComponent<StripedCandy>() == null && board.GetTile(i, k).GetComponent<WrappedCandy>() == null)
                            {
                                match.AddTile(board.GetTile(i, k));
                                k -= 1;
                            }
                        }
                    }

                    i += 1;
                }
            }

            return matches;
        }
    }
}
