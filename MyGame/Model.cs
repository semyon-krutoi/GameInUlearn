using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    internal class Model
    {
        #region
        public class Player
        {
            public int Scores { get; set; }
            public int Leaves { get; set; } = 3;

            public int Indexconveyer { get; set; }

            public void MoveIndexRight()
            {
                if (Indexconveyer < 3)
                    Indexconveyer++;
            }

            public void MoveIndexleft()
            {
                if (Indexconveyer > 0)
                    Indexconveyer--;
            }

            public void AddScore(int score)
            {
                Scores += score;
            }

            public void LeftLeaves()
            {
                if (Leaves > 0)
                    Leaves--;
            }
        }
        #endregion

        public enum ItemType
        {
            Fire,
            Water,
            Earth,
            Air,
            Bomb
        }

        public class GameItem
        {
            public double X { get; set; }
            public double Y { get; set; }
            public ItemType Type { get; set; }
            public int Target { get; set; }

            public GameItem(double x, double y, ItemType type, int target)
            {
                X = x;
                Y = y;
                Type = type;
                Target = target;
            }

            public void Move(double y)
            {
                Y += y;
            }
        }

        public class GameModel
        {
            public Player player { get; } = new Player();
            public List<GameItem> Items { get; } = new List<GameItem>();

            public bool IsGameOver => player.Leaves <= 0;

            public void Update(double deltaTime)
            {
                if (IsGameOver)
                    return;

                foreach (var item in Items)
                {
                    item.Move(deltaTime * 2.0);
                }

                for (int i = 0; i < Items.Count - 1; i++)
                {
                    if (Items[i].Y >= 10.0)
                    {
                        var tempItem = Items[i];

                        if (tempItem.Type == ItemType.Bomb)
                            player.LeftLeaves();

                        else if (tempItem.Target == player.Indexconveyer)
                            player.AddScore(10);

                        else
                            player.LeftLeaves();

                        Items.RemoveAt(i);

                    }
                }
            }

            public void AddItem(GameItem item)
            {
                Items.Add(item);
            }
        }
    }
    
}

