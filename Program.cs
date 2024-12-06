using System.Numerics;
using Raylib_cs;

namespace Game
{
    class Game
    {
        const int screenWidth = 600;
        const int screenHeight = 600;
        private static void Main()
        {
            bool update = false;
            Raylib.InitWindow(screenWidth, screenHeight, "s");
            Raylib.SetTargetFPS(60);

            var player = new Player.Player();
            var bricks = new Bricks.Bricks();
            var ball = new Ball.Ball();
            Texture2D background = Raylib.LoadTexture($"{Directory.GetCurrentDirectory()}\\Assets\\bg.png");
            while (!Raylib.WindowShouldClose())
            {
                if (Raylib.CheckCollisionCircleRec(new Vector2(ball.x, ball.y), ball.width, new Rectangle(player.x, player.y, player.width, player.height)))
                {
                    ball.speedY += -1;
                }
                foreach (var brick in bricks.BrickList)
                {
                    if (brick.IsVisible && Raylib.CheckCollisionCircleRec(new Vector2(ball.x, ball.y), ball.width, brick.Rect))
                    {
                        ball.speedY *= -1; // Reverse ball direction
                        brick.IsVisible = false; // Hide the brick
                    }
                }
                if (Raylib.IsKeyDown(KeyboardKey.Space) && !update)
                {
                    update = true;
                }
                if (update)
                {
                    ball.Update();
                    player.Update();
                }
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.White);
                Raylib.DrawTexture(background, 0, 0, Color.White);
                if (!update)
                {
                    Raylib.DrawText("Press Space To Start Game", 200, 300, 24, Color.White);
                }
                player.Draw();
                bricks.Draw();
                ball.Draw();
                Raylib.EndDrawing();
            }
        }
    }
}