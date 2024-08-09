namespace SwapLib
{
    public class Swap<T>
    {
        static public void swap(ref T a, ref T b)
        {
            T temp = a;
            a = b;
            b = temp;
        }
    }
}
