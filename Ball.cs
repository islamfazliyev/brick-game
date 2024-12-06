using Raylib_cs;


namespace Ball
{
    public class Ball
    {
        //int speed;
        public int width = 10, height = 10;
        public int speedX = 5, speedY = 5;
        public int defaultSpeeds = 5;
        Texture2D texture = Raylib.LoadTexture($"{Directory.GetCurrentDirectory()}\\Assets\\Ball.png");
        public int x = Raylib.GetScreenWidth() / 2, y = Raylib.GetScreenHeight() / 2;
        
        public void Draw()
        {
            
            texture.Width = width;
            texture.Height = height;
            Raylib.DrawTexture(texture, x, y, Color.White);
        }

        public void Update()
        {
            x += speedX;
            y += speedY;
            if (y + height >= Raylib.GetScreenHeight())
            {
                ResetBall();
            }
            if (y - height <= 0)
            {
                speedY *= -1;
            }
            if (x + width >= Raylib.GetScreenWidth() || x - width <= 0)
            {
                speedX *= -1;
            }
            
        }

        public void ResetBall()
        {
            x = Raylib.GetScreenWidth() / 2;
            y = Raylib.GetScreenHeight() / 2;

            int[] speedChoices = { -1, 1 };
            Random random = new Random();

            speedX *= speedChoices[random.Next(0, 2)];
            speedY *= speedChoices[random.Next(0, 2)];
            
        }
    }
}