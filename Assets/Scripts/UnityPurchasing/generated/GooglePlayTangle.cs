// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("XwKrp+9WIFdsP0yYbB27eotAPW4z/QqCp3zwB0ymCJdWNHnaZKQ9t2wH1Yzfk/iRoUcKIfDXylOkyXlV8UnFWpOLWRIbfWyr5WKQ3XvyHO6FW9io5uJMimXK+ZdCIUKSaFyav+idYxF64DXDrkTorIHSQS5St2PmgU8GUxRzg/g18jeV2EnxcagyvaZ+OxrSi9zXxRnmrhOSKGBAjeDuRQT7i+D99XDhRuszOwQspNhO4y9oTwXhePe73UVbuU4WBFPAW0FKsiBt31x/bVBbVHfbFduqUFxcXFhdXmpqL7BGjEtjV5Ox1Esscgo75+EE31xSXW3fXFdf31xcXY939EDXTR1FTyBs7x7AzpTdZO74x0IqBPLAlcL63CdAi2IQhl9eXF1c");
        private static int[] order = new int[] { 5,2,10,9,13,11,9,9,9,9,11,12,12,13,14 };
        private static int key = 93;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
