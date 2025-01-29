using System.Numerics;
using Raylib_CsLo;

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
            var isEditorMode = false;

            Texture background = Raylib.LoadTexture($"{Directory.GetCurrentDirectory()}\\Assets\\bg.png");
            while (!Raylib.WindowShouldClose())
            {
                if (Raylib.IsKeyPressed(KeyboardKey.KEY_F1))
                {
                    isEditorMode = !isEditorMode;
                    update = isEditorMode;

                }
                Raylib.BeginDrawing();

                if (!isEditorMode)
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
                    if (Raylib.IsKeyDown(KeyboardKey.KEY_SPACE) && !update)
                    {
                        update = true;
                    }
                    if (update)
                    {
                        ball.Update();
                        player.Update();
                    }
                    Raylib.ClearBackground(Raylib.WHITE);
                    Raylib.DrawTexture(background, 0, 0, Raylib.WHITE);
                    if (!update)
                    {
                        Raylib.DrawText("Press Space To Start Game", 200, 300, 24, Raylib.WHITE);
                    }
                    player.Draw();
                    bricks.Draw();
                    ball.Draw();
                }
                else
                {
                    float mousePosX = Raylib.GetMousePosition().X;
                    float mousePosY = Raylib.GetMousePosition().Y;
                    for (int x = 0; x < screenWidth; x += 100)
                    {
                        Raylib.DrawLine(x, 0, x, screenHeight, Raylib.GRAY);
                    }
                    for (int y = 0; y < screenHeight; y += 20)
                    {
                        Raylib.DrawLine(0, y, screenWidth, y, Raylib.GRAY);
                    }
                    if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_BUTTON_LEFT))
                    {
                        bricks.Add((int)mousePosX, (int)mousePosY);
                        bricks.Draw();
                    }
                    if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_BUTTON_RIGHT))
                    {
                        bricks.Remove();
                    }

                    Raylib.DrawText("EDITOR MODE (Exit With F1)", 10, 10, 20, Raylib.RED);
                    Raylib.DrawText("Left Click: Place Bricks | Right Click: Delete Bricks", 10, 40, 20, Raylib.WHITE);

                }
                Raylib.EndDrawing();
                
            }
        }
    }
}