#if UNITY_ANDROID || UNITY_IPHONE || UNITY_STANDALONE_OSX || UNITY_TVOS
// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("af+YefmejepWZAUYxyJdF7yGEGVpRcgf8LB8JVNrL41PTVYKaOwXVgW3NBcFODM8H7N9s8I4NDQ0MDU2fBol6vVIamJkIo2EZ0nlf6NSoST4ke+hvsAVvx+Ac4xys7FZE+yXRuVVYArPQPH2ldhdNmSA9bC4lPZvKsV7ruN8GkxHa4nmKCMULciDu9wtIxuGGJXBp9DoER7NPzAC++2amxTC4myvpTXzz2+Orhb8W7ph3q0ItzQ6NQW3ND83tzQ0NYupnMLWnvNUA8zOjxfFNLbIA5kZ00f13uCfP9Xg8dxor9Gb/L2vCcDgVwZyTQAeqkXQSINz8tosPuJDCDwBb5U+mLcjo8KWFGM8rg8Ak27gTc0jxesnc2ECq77HyozNFjc2NDU0");
        private static int[] order = new int[] { 4,11,4,3,10,5,12,13,12,11,12,13,12,13,14 };
        private static int key = 53;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
#endif
