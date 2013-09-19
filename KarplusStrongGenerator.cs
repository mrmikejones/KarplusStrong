using UnityEngine;
using System;
using System.Collections;

public class KarplusStrongGenerator : MonoBehaviour {
	public float frequency = 440;
	public float pickstrength = 1.0f;
	public float direction = 1.0f;
	
	private double increment;
	private double phase;
	private double sampling_frequency = 48000;
	
	private float[] buffer;
	float previous = 0.0f;
	private int bufferindex = 0;
	private int bufferlen = 0;
	
	OnePoleFilter pickDirectionFilter = new OnePoleFilter();
	OnePoleFilter pickStrengthFilter = new OnePoleFilter();
	OnePoleFilter dynamicsLowpassFilter = new OnePoleFilter();
	DelayLinearInterpolated pickPositionCombFilter = new DelayLinearInterpolated();
	OnePoleFilter lossFilter = new OnePoleFilter();
	
	// Use this for initialization
	void Start () {
		sampling_frequency = AudioSettings.outputSampleRate;
		
		bufferlen = (int)sampling_frequency / 80;
		buffer = new float[bufferlen];
		
		dynamicsLowpassFilter.SetPole(0.5);
		
		pickPositionCombFilter.SetMaximum(bufferlen);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnAudioFilterRead(float[] data, int channels)
	{
		for (var i = 0; i < data.Length; i = i + channels)
		{
			float currentsample = (float)lossFilter.Tick(GetBufferValue());
			
			for(var channel = 0; channel < channels; channel = channel + 1)
			{
				data[i + channel] = (float)currentsample;
			}
		}
	}
		
	float GetBufferValue()
	{
		float val = (0.5f * (buffer[bufferindex] + previous));
		buffer[bufferindex] = val;
		previous = val;
		
		bufferindex++;
		if(bufferindex >= bufferlen)
		{
			bufferindex = 0;
		}
		
		return val;
	}
		
	public void Trigger(float newFreq, float newPickStrength, float newPickDirction, float newPickPos)
	{
		frequency = newFreq;
		float p1 = (float)sampling_frequency/newFreq;
		
		bufferlen = Mathf.FloorToInt(p1 - 0.5f);
		
		double pluckPos = newPickPos;
		pickPositionCombFilter.SetDelay(0.5 * pluckPos * bufferlen);
		
		double g = Mathf.Exp(-1.0f/(frequency * 5.0f));   
  		double a = -0.8;
		lossFilter.SetCoefficients(g * (a + 1), a);
		
		pickstrength = newPickStrength;
		direction = newPickDirction;
			
		for(var i = 0; i < buffer.Length; i++)
		{
			buffer[i] =  1.0f - UnityEngine.Random.Range(0.0f, 20000.0f) / 10000.0f;
		}
		
		double D = newPickDirction < 0.0f ? 0.0009 : 0.9009;
		pickDirectionFilter.SetCoefficients(1.0f - D, -D);
		
		pickStrengthFilter.SetGain(pickstrength * 0.85);
		pickStrengthFilter.SetPole(0.999 - (pickstrength * 0.15)); 
		
		for(var i = 0; i < buffer.Length; i++)
		{
			buffer[i] = (float)pickDirectionFilter.Tick(pickStrengthFilter.Tick((double)buffer[i]));
			buffer[i] = buffer[i] - (float)pickPositionCombFilter.Tick(buffer[i]);
		}
	}
}
