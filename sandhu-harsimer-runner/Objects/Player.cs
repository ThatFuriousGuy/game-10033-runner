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
        float speed = 5f;
        public bool game_won = false;

        public void Setup(float posx = 20f, float posy = 270f)
        {
            position = new Vector2(posx, posy);
        }
        public void Update(Walls[] walls)
        {
            draw_self();
            if (game_won == false) 
            {
                handle_input(walls);
            }
        }
        public void draw_self()
        {
            Draw.LineSize = 1;
            Draw.FillColor = Color.Red;
            Draw.Rectangle(position.X, position.Y, size.X, size.Y);
        }

        void handle_input(Walls[] walls)
        {
            // Figure out which way the player wants to go this frame
            float move_x = 0f;
            float move_y = 0f;

            if (Input.IsKeyboardKeyDown(KeyboardKey.W) || Input.IsKeyboardKeyDown(KeyboardKey.Up))
            {
                move_y -= speed;
            }
            if (Input.IsKeyboardKeyDown(KeyboardKey.S) || Input.IsKeyboardKeyDown(KeyboardKey.Down))
            {
                move_y += speed;
            }
            if (Input.IsKeyboardKeyDown(KeyboardKey.A) || Input.IsKeyboardKeyDown(KeyboardKey.Left))
            {
                move_x -= speed;
            }
            if (Input.IsKeyboardKeyDown(KeyboardKey.D) || Input.IsKeyboardKeyDown(KeyboardKey.Right))
            {
                move_x += speed;
            }

            // Move sideways
            position.X += move_x;
            if (hits_wall(walls))
            {
                position.X -= move_x;
            }

            // Move up/down
            position.Y += move_y;
            if (hits_wall(walls))
            {
                position.Y -= move_y;
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
        // Checks every wall to see if the player is inside any of them
        bool hits_wall(Walls[] walls)
        {
            for (int i = 0; i < walls.Length; i++)
            {
                if (walls[i].is_touching(position, size))
                {
                    return true;
                }
            }
            return false;
        }
        public Vector2 get_size()
        {
            return size;
        }
        public Vector2 get_position()
        {
            return position;
        }

        public void respawn(Vector2 spawn_point)
        {
            position = spawn_point;
        }

    }

}
