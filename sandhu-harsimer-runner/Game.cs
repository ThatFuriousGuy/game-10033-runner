// Include the namespaces (code libraries) you need below.
using sandhu_harsimer_runner.Objects;
using System;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;

// The namespace your code is in.
namespace MohawkGame2D
{

    // TODO:
    // Make area walls

    // Make player with controls -- DONE --
    
    // Make objects to stop player
    
    // Make end area


    public class Game
    {
        Player player = new Player();
        public void Setup()
        {
            Window.SetSize(800, 600);
            Window.SetTitle("Runner");
            // Calling setup so the player will be visible and run its own logic
            player.Setup();
        }

        public void Update()
        {
            Window.ClearBackground(Color.Black);
            player.Update();
            
        }
    }

}
