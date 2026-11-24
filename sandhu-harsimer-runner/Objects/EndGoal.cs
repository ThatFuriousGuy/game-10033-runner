using MohawkGame2D;
using System;
using System.Numerics;

namespace sandhu_harsimer_runner.Objects
{
    internal class EndGoal
    {
        Vector2 position = new Vector2();
        Vector2 size = new Vector2();

        public void Setup(float x, float y, float width, float height)
        {
            position = new Vector2(x, y);
            size = new Vector2(width, height);
        }

        // If the player reaches here, they win
        public void Update()
        {
            draw_self();
        }

        void draw_self()
        {
            Draw.FillColor = Color.Blue;
            Draw.Rectangle(position.X, position.Y, size.X, size.Y);
        }
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
