using MohawkGame2D;
using System;
using System.Numerics;

namespace sandhu_harsimer_runner.Objects
{
    internal class CheckPoint
    {
        Vector2 position = new Vector2();
        Vector2 size = new Vector2();

        public void Setup(float x, float y, float width, float height)
        {
            position = new Vector2(x, y);
            size = new Vector2(width, height);
        }

        // active is when the player has claimed this checkpoint
        public void Update(bool active)
        {
            draw_self(active);
        }

        void draw_self(bool active)
        {
            if (active)
            {
                Draw.FillColor = Color.Green;
            }
            else
            {
                Draw.FillColor = Color.Gray;
            }
            Draw.Rectangle(position.X, position.Y, size.X, size.Y);
        }

        // Checks if the player's square overlaps this checkpoint
        public bool is_touching(Vector2 player_pos, Vector2 player_size)
        {
            return player_pos.X < position.X + size.X && player_pos.X + player_size.X > position.X && player_pos.Y < position.Y + size.Y && player_pos.Y + player_size.Y > position.Y;
        }

        public Vector2 get_position()
        {
            return position;
        }
        public Vector2 get_size()
        {
            return size;
        }
    }
}