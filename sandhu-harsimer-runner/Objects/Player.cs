using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using MohawkGame2D;

namespace sandhu_harsimer_runner.Objects
{
    internal class Player
    {
        Vector2 position = new Vector2();
        Vector2 size = new Vector2(30f, 30f);
        float speed = 7f;

        public void Setup(float posx = 20f, float posy = 270f)
        {
            position = new Vector2(posx, posy);
        }
        public void Update()
        {
            draw_self();
            handle_input();
        }
        public void draw_self()
        {
            Draw.LineSize = 1;
            Draw.FillColor = Color.Red;
            Draw.Rectangle(position.X, position.Y, size.X, size.Y);
        }

        void handle_input()
        {
            if (Input.IsKeyboardKeyDown(KeyboardKey.W) || (Input.IsKeyboardKeyDown(KeyboardKey.Up)))
            {
                // Player wants to go down
                position.Y -= speed;
            }
            if (Input.IsKeyboardKeyDown(KeyboardKey.S) || (Input.IsKeyboardKeyDown(KeyboardKey.Down)))
            {
                // Player wants to go up
                position.Y += speed;
            }
            if (Input.IsKeyboardKeyDown(KeyboardKey.A) || (Input.IsKeyboardKeyDown(KeyboardKey.Left)))
            {
                // Player wants to go left
                position.X -= speed;
            }
            if (Input.IsKeyboardKeyDown(KeyboardKey.D) || (Input.IsKeyboardKeyDown(KeyboardKey.Right)))
            {
                // Player wants to go right
                position.X += speed;
            }
            update_pos_clamp(size.X, size.Y);
        }
        void update_pos_clamp(float width, float height)
        {
            if (position.X > 800f - width)
            {
                position.X = 800f - width;
            }
            if (position.X < 0f)
            {
                position.X = 0f;
            }
            if (position.Y > 600f - height)
            {
                position.Y = 600f - height;
            }
            if (position.Y < 0f)
            {
                position.Y = 0f;
            }
        }

    }

}
