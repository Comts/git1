#if UNITY_ANDROID || UNITY_IPHONE || UNITY_STANDALONE_OSX || UNITY_TVOS
// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("MoADIDIPBAsohEqE9Q8DAwMHAgFecv8ox4dLEmRcGLp4emE9X9sgYRoULLEvovaQ598mKfoIBzXM2q2sSy0S3cJ/XVVTFbqzUH7SSJRllhPPptiWifciiCi3RLtFhIZuJNugcdJiVz34d8bBou9qAVO3woePo8FYHfJMmdRLLXtwXL7RHxQjGv+0jOuAAw0CMoADCACAAwMCvJ6r9eGpxJ1y53+0RMXtGwnVdD8LNliiCa+AI/XVW5iSAsT4WLmZIctsjVbpmj/i18brX5jmrMuKmD7312AxRXo3KV7Ir07OqbrdYVMyL/AVaiCLsSdSFJT1oSNUC5k4N6RZ13r6FPLcEERjNPv5uCDyA4H/NK4u5HDC6deoCFY1nInw/bv6IQABAwID");
        private static int[] order = new int[] { 0,11,13,3,10,5,12,11,13,12,11,11,12,13,14 };
        private static int key = 2;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
#endif
