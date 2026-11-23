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
    // Make area walls

    // Make checkpoints -- DONE --

    // Make player with controls -- DONE --
    
    // Make objects to stop player
    
    // Make end area


    public class Game
    {
        bool game_won = false;

        // The 3 checkpoints
        CheckPoint[] checkpoints = {new CheckPoint(), new CheckPoint(), new CheckPoint()};
        // Which checkpoint the player respawns at
        int current_checkpoint = 0;
        Player player = new Player();
        public void Setup()
        {
            Window.SetSize(800, 600);
            Window.SetTitle("Runner");
            // Calling setup so the player will be visible and run its own logic
            player.Setup();

            checkpoints[0].Setup(0, 250, 55, 80);   // start box
            checkpoints[2].Setup(150, 20, 150, 60);  // third box
            checkpoints[1].Setup(300, 150, 60, 50);  // second box

            // So the player is spawned in the middle of the check point
            player.respawn(checkpoints[current_checkpoint].get_position() + checkpoints[current_checkpoint].get_size() / 2f - player.get_size() / 2f);
        }

        public void Update()
        {
            Window.ClearBackground(Color.Black);
            Handle_Points();
            player.Update();
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
    }

}
