// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("dU8aAf1B4fBwvK2V2DkhlwN+9fiDbDfKUVHvlXec4gjbpQ3dq8IAe++de5S2m9XFmZz9VmDrYFZUiT5BCEdyIRM+s4JTKXulhQrZqNRvnbMiW7iSz3hUNV7RJ2Wy44VjuSehye6D6sMDHMj+K2DCuROulXo0LRjLlr1DE8AC7d3tgtSV4Ugt7Pp0zpmTzHDg12ItKarIm10ZBmqJeilZPG7cX3xuU1hXdNgW2KlTX19fW15du6XtJDQZ60secPtUrX3+pX0+yJ/Humqt3qzzMCVagSjcy2R25DMu0yvAc35cYvhMAQHJzl9k+CW5sARY3F9RXm7cX1Rc3F9fXtCf7qM434xf67ReaabpE84Jx7gIA4BVPycpcuez4EsEwYFaT1xdX15f");
        private static int[] order = new int[] { 5,3,8,4,9,10,13,8,10,12,10,13,12,13,14 };
        private static int key = 94;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
