using System.Numerics;
using Raylib_cs;

namespace Bricks
{

    public class Brick
    {

        public Rectangle Rect { get; private set; }
        public bool IsVisible { get; set; } = true;

        private Texture2D texture;
        private int x;
        private int y;
        private int width;
        private int height;

        public Brick(int x, int y, int width, int height, Texture2D texture)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.texture = texture;

            // Define the rectangle for collision and positioning
            Rect = new Rectangle(x, y, width, height);
        }

        public void Draw()
        {
            if (IsVisible)
            {
                // Scale the texture to fit the brick
                Raylib.DrawTexturePro(
                    texture,
                    new Rectangle(0, 0, texture.Width, texture.Height), // Full texture
                    Rect, // Destination rectangle
                    new Vector2(0, 0), // No rotation origin
                    0f, // No rotation
                    Color.White // Default color
                );
            }
        }
    
    }
    public class Bricks
    {
        public List<Brick> BrickList { get; private set; } = new List<Brick>();
        private Texture2D brickTexture;

        public Bricks()
        {
            // Load the texture once for all bricks
            brickTexture = Raylib.LoadTexture($"{Directory.GetCurrentDirectory()}\\Assets\\Brick.png");

            int brickWidth = 100;
            int brickHeight = 20;
            int xSpacing = 10;
            int ySpacing = 10;

            // Position bricks in rows and columns
            for (int x = 30; x < 600 - brickWidth; x += brickWidth + xSpacing)
            {
                for (int y = 10; y < 200; y += brickHeight + ySpacing)
                {
                    
                    BrickList.Add(new Brick(x, y, brickWidth, brickHeight, brickTexture));
                }
            }
        }

        public void Draw()
        {
            foreach (var brick in BrickList)
            {
                brick.Draw();
            }
        }
    }
}