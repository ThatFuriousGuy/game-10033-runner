using MohawkGame2D;
using System;
using System.Numerics;

namespace sandhu_harsimer_runner.Objects
{
    internal class Walls
    {
        Vector2 position = new Vector2();
        Vector2 size = new Vector2();

        // Lets me set wall sizes for various spots on the "map"
        public Walls(float x, float y, float width, float height)
        {
            position = new Vector2(x, y);
            size = new Vector2(width, height);
        }

        public void Update()
        {
            draw_self();
        }

        void draw_self()
        {
            Draw.LineSize = 0;
            Draw.FillColor = Color.OffWhite;
            Draw.Rectangle(position.X, position.Y, size.X, size.Y);
        }

        // Same overlap check as the checkpoints
        public bool is_touching(Vector2 player_pos, Vector2 player_size)
        {
            return player_pos.X < position.X + size.X && player_pos.X + player_size.X > position.X
                && player_pos.Y < position.Y + size.Y && player_pos.Y + player_size.Y > position.Y;
        }
    }
}