using System.Collections;

public class OnePoleFilter
{
	double a1;
	double b0;
	double z1;
	double gain;
	
	public OnePoleFilter()
	{
		a1 = 0.0;
		b0 = 1.0;
		z1 = 0.0;
		gain = 1.0;
	}
	
	public double Tick(double val)
	{
		return z1 = (val * gain * b0) - (z1 * a1);
	}
	
	public void SetCoefficients(double p_b0, double p_a1)
	{
		b0 = p_b0;
		a1 = p_a1;
	}
	
	public void SetPole(double p_pole)
	{
		if(p_pole > 0.0)
			b0 = 1.0 - p_pole;
		else
			b0 = 1.0 + p_pole;
		
		a1 = -p_pole;
	}
	
	public void SetGain(double p_gain)
	{
		gain = p_gain;
	}
}
