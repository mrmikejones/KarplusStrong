using System;
using System.Collections;

public class DelayLinearInterpolated
{	
	double[] inputLine;
	
	int inPtr;
	int outPtr;
	double alpha;
	double omAlpha;
	double delay;
	
	
	public void SetMaximum(int delay)
	{
		inputLine = new double[delay + 1];
	}
	
	public void Empty()
	{
		Array.Clear(inputLine, 0, inputLine.Length);
	}
	
	public void SetDelay(double newdelay)
	{
		delay = newdelay;
		
		double fracOut = (double)inPtr - delay;
		while(fracOut < 0)
		{
			fracOut += inputLine.Length;
		}
		
		outPtr = (int)fracOut;
		alpha = fracOut - (double)outPtr;
		omAlpha = 1.0 - alpha;
	}
	
	public double Tick(double val)
	{
		inputLine[inPtr] = val;
		
		inPtr += 1;
		inPtr = inPtr % inputLine.Length;
		
		double outVal = inputLine[outPtr] * omAlpha;
		
		outPtr += 1;
		outPtr = outPtr % inputLine.Length;
		
		outVal += inputLine[outPtr] * alpha;
		
		return outVal;
	}
}
