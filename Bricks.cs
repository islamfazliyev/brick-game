using System.Numerics;
using Raylib_CsLo;

namespace Bricks
{

    public class Brick
    {

        public Rectangle Rect { get; private set; }
        public bool IsVisible { get; set; } = true;

        private Texture texture;
        private int x;
        private int y;
        private int width;
        private int height;

        public Brick(int x, int y, int width, int height, Texture texture)
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
                    new Rectangle(0, 0, texture.width, texture.height), // Full texture
                    Rect, // Destination rectangle
                    new Vector2(0, 0), // No rotation origin
                    0f, // No rotation
                    Raylib.WHITE // Default color
                );
            }
        }
    
    }
    public class Bricks
    {
        public List<Brick> BrickList { get; private set; } = new List<Brick>();
        private Texture brickTexture;

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

        public void Add(int x, int y)
        {
            int gridX = (x/50) * 50;
            int gridY = (y/50) * 50;

            bool exists = BrickList.Any(b => b.Rect.x == gridX && b.Rect.y == gridY);
            if (!exists)
            {
                BrickList.Add(new Brick(gridX, gridY, 100, 20, brickTexture));
            }
        }

        public void Remove()
        {
            var brickToRemove = BrickList.FirstOrDefault(b => Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(), b.Rect));
            if (brickToRemove != null)
            {
                BrickList.Remove(brickToRemove);
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