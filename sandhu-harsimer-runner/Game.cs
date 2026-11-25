// Include the namespaces (code libraries) you need below.
using sandhu_harsimer_runner.Objects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;

// The namespace your code is in.
namespace MohawkGame2D
{

    // TODO:
    // Make area walls -- DONE --

    // Make checkpoints -- DONE --

    // Make player with controls -- DONE --
    
    // Make objects to stop player -- DONE --
    
    // Make end area -- DONE --


    public class Game
    {
        bool game_won = false;
        // A global speed for all the spikes
        float spike_speed = 120f;
        Spike[] spikes =
        {
            // Top left room
            new Spike(12, new Vector2[] { new(40, 40), new(40, 210), new(130, 210), new(130, 40) }, 0),
            new Spike(12, new Vector2[] { new(40, 40), new(40, 210), new(130, 210), new(130, 40) }, 2),

            // 1st vertical room
            new Spike(12, new Vector2[] { new(192, 110), new(192, 310) }, 0),
            new Spike(12, new Vector2[] { new(257, 110), new(257, 310) }, 1),

            // 2nd vertical room
            new Spike(12, new Vector2[] { new(315, 220), new(315, 480) }, 0),
            new Spike(12, new Vector2[] { new(345, 220), new(345, 480) }, 1),

            // Bottom room
            new Spike(12, 230, 425, 55, 0),
            new Spike(12, 230, 425, 55, MathF.PI),

            // Top right area
            new Spike(12, new Vector2[] { new(315, 40), new(315, 110) }, 1),
            new Spike(12, new Vector2[] { new(380, 40), new(380, 110) }, 0),
            new Spike(12, new Vector2[] { new(435, 40), new(435, 130) }, 1),
            new Spike(12, new Vector2[] { new(485, 40), new(485, 130) }, 0),

            // Next to goal
            new Spike(15, new Vector2[] { new(525, 200), new(400, 200) }, 0),
        };

        // The 3 checkpoints
        CheckPoint[] checkpoints = {new CheckPoint(), new CheckPoint(), new CheckPoint()};
        // Which checkpoint the player respawns at
        int current_checkpoint = 0;

        // Every wall position and size in the level (x, y, width, height)
        Walls[] walls =
        {
            // This was annoying to get each one right
            new Walls(0, 0, 570, 20),
            new Walls(0, 0, 20, 250),
            new Walls(0, 230, 170, 20),
            new Walls(0, 330, 100, 20),
            new Walls(80, 330, 20, 190),
            new Walls(80, 500, 300, 20),
            new Walls(360, 130, 20, 390),
            new Walls(150, 80, 150, 20),
            new Walls(150, 80, 20, 90),
            new Walls(150, 250, 20, 100),
            new Walls(150, 330, 150, 20),
            new Walls(280, 200, 20, 150),
            new Walls(215, 80, 20, 190),
            new Walls(280, 80, 20, 70),
            new Walls(280, 130, 100, 20),
            new Walls(360, 150, 140, 20),
            new Walls(570, 0, 20, 250),
            new Walls(480, 230, 20, 70),
            new Walls(480, 230, 90, 20),
            new Walls(360, 280, 140, 20),
        };

        Player player = new Player();
        EndGoal goal = new EndGoal();
        public void Setup()
        {
            Window.SetSize(800, 600);
            Window.SetTitle("Runner");
            // Calling setup so the player will be visible and run its own logic
            player.Setup();
            goal.Setup(380, 250, 100, 30);
            checkpoints[0].Setup(0, 250, 55, 80); // start box
            checkpoints[2].Setup(150, 20, 150, 60); // third box
            checkpoints[1].Setup(300, 150, 60, 50); // second box

            // So the player is spawned in the middle of the check point
            respawn_player();
        }

        public void Update()
        {
            Window.ClearBackground(Color.Black);

            for (int i = 0; i < walls.Length; i++)
            {
                walls[i].Update();
            }
            Handle_Points();
            after_win();
            goal.Update();
            player.Update(walls);
            handle_spikes();
        }

        void Handle_Points()
        {
            // Run code for every point that is out
            for (int i = 0; i < checkpoints.Length; i++)
            {
                // Calls each checkpoint if they can detect the player, to which they get set to active if so
                // It also does not let you claim already claimed checkpoints
                if (checkpoints[i].is_touching(player.get_position(), player.get_size()) && i > current_checkpoint)
                {
                    current_checkpoint = i;
                }

                checkpoints[i].Update(i == current_checkpoint);
            }
        }
        void handle_spikes()
        {
            for (int i = 0; i < spikes.Length; i++)
            {
                spikes[i].Update(spike_speed);

                if (spikes[i].is_touching(player.get_position(), player.get_size()))
                {
                    respawn_player();
                }
            }
        }

        void respawn_player()
        {
            player.respawn(checkpoints[current_checkpoint].get_position() + checkpoints[current_checkpoint].get_size() / 2f - player.get_size() / 2f);
        }
        void after_win()
        {
            if (game_won == false)
            {
                if (goal.is_touching(player.get_position(), player.get_size()))
                {
                    game_won = true;
                }
            }
            if (game_won)
            {
                player.game_won = game_won;
                string win_text = $"YOU WIN";
                string restart_text = $"Press Space to Restart";
                int size = 50;
                int size2 = 30;
                float text_width = win_text.Length * size * 0.6f;
                float text_width2 = restart_text.Length * size2 * 0.6f;

                float x = 650 - text_width / 2;
                float y = 290 - size / 2;
                float x2 = 600 - text_width2 / 2;
                float y2 = 450 - size2 / 2;

                Text.Color = Color.White;
                Text.Size = size;
                Text.Draw(win_text, x, y);
                Text.Size = size2;
                Text.Draw(restart_text, x2, y2);
                if (Input.IsKeyboardKeyDown(KeyboardKey.Space))
                {
                    reset_game();
                }
            }
        }
        void reset_game()
        {
            game_won = false;
            current_checkpoint = 0;
            player.game_won = game_won;
            respawn_player();
        }
    }

}
