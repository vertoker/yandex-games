namespace Maze.Data
{
    public interface IPacker
    {
        public void UnPack(byte[] data);
        public byte[] Pack();
    }
}