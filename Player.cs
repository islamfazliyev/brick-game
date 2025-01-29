using Raylib_cs;

namespace Player
{
    public class Player
    {
        public int speed = 7;
        public int width = 100, height = 20;
        public int x = 400, y = 560;
        Texture2D texture = Raylib.LoadTexture($"{Directory.GetCurrentDirectory()}\\Assets\\Player.png");

        public void Draw()
        {
            texture.Width = width;
            texture.Height = height;
            Raylib.DrawTexture(texture, x, y, Color.White);
        }
        public void Update()
        {
            if (Raylib.IsKeyDown(KeyboardKey.A))
            {
                x -= speed;
            }
            if (Raylib.IsKeyDown(KeyboardKey.D))
            {
                x += speed;
            }
            if (x <= 0)
            {
                x = 0;
            }
            if (x + width >= Raylib.GetScreenWidth())
            {
                x = Raylib.GetScreenWidth() - 100;
            }
        }
    }
}