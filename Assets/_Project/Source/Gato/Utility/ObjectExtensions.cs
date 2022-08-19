namespace Gato
{
    public static class ObjectExtensions
    {
        public static bool IsUnityNull(this object o)
        {
            if (o is UnityEngine.Object obj)
            {
                return obj == null;
            }

            return o == null;
        }
    }
}
