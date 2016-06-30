namespace edzacc
{
    public interface IParser
    {
        TokenTreeRoot Parse(string text);
    }
}