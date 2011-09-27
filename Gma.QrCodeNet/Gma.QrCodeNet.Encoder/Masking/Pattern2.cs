﻿namespace Gma.QrCodeNet.Encoding.Masking
{
    internal class Pattern2 : Pattern
{
        public override bool this[int i, int j]
        {
            get { return j % 2 == 0; }
        }

        public override MaskPatternType MaskPatternType
        {
            get { return MaskPatternType.Type2; }
        }
}
}
