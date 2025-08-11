// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("sVrp5Mb4Ytabm1NUxf5ivyMqnsJ1B+EOLAFPXwMGZ8z6cfrMzhOk2xn2rVDLy3UP7QZ4kkE/l0cxWJrhdBlwWZmGUmSx+lgjiTQP4K63glGS3ei7iaQpGMmz4T8fkEMyTvUHKbjBIghV4s6vxEu9/yh5H/kjvTtTXSDwN0Q2aaq/wBuyRlH+7H6ptEkJVup6Tfi3szBSAceDnPAT4LPDpsVxLsTzPHOJVJNdIpKZGs+lvbPoDCfZiVqYd0d3GE4Pe9K3dmDuVAPv1YCbZ9t7auomNw9Co7sNmeRvYkbFy8T0RsXOxkbFxcRKBXQ5okUW9EbF5vTJws3uQoxCM8nFxcXBxMchP3e+roNx0YTqYc4352Q/56RSBX0petGeWxvA1cbHxcTF");
        private static int[] order = new int[] { 6,8,3,10,4,9,7,10,11,13,13,11,13,13,14 };
        private static int key = 196;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
