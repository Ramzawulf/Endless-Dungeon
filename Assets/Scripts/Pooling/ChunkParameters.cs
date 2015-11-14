namespace Assets.Scripts.Pooling
{
    public class ChunkParameters
    {
        public int Height;

        public static ChunkParameters Default
        {
            get
            {
                return new ChunkParameters
                {
                    Height = 3
                };
            }
        }
    }
}