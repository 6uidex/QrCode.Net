﻿using System;
using Gma.QrCodeNet.Encoding.Positioning;

namespace Gma.QrCodeNet.Encoding.EncodingRegion
{
	internal static class Codeword
	{
		
		
		internal static void TryEmbedCodewords(this TriStateMatrix tsMatrix, BitList codewords)
		{
			int sWidth = tsMatrix.Width;
			int codewordsSize = codewords.Count;
			
			int bitIndex = 0;
			bool directionUp = true;
			
			int x = sWidth - 1;
			int y = sWidth - 1;
			
			while( x > 0 )
			{
				//Skip vertical timing pattern
				if(x == 6)
					x -= 1;
				while( y >= 0 && y < sWidth)
				{
					for(int xOffset = 0; xOffset < 2; xOffset++)
					{
						int xPos = x - xOffset;
						if(tsMatrix.MStatus(xPos, y) != MatrixStatus.None)
						{
							continue;
						}
						else
						{
							bool bit;
							if(bitIndex < codewordsSize)
							{
								bit = codewords[bitIndex];
								bitIndex++;
							}
							else
								bit = false;
							
							tsMatrix[xPos, y, MatrixStatus.Data] = bit;
								
						}
					}
					y = NextY(y, directionUp);
					
				}
				directionUp = ChangeDirection(directionUp);
				y = NextY(y, directionUp);
				x -= 2;
			}
			
			if(bitIndex != codewordsSize)
				throw new Exception(string.Format("Not all bits from codewords consumed by matrix: {0} / {1}", bitIndex, codewordsSize));
		}
		
		internal static int NextY(int y, bool directionUp)
		{
			return directionUp ? y - 1 : y + 1;
		}
		
		internal static bool ChangeDirection(bool directionUp)
		{
			return directionUp ? false : true;
		}
		
	}
}